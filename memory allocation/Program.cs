﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace memory_allocation
{

    public class Program
    {
        public static int nholes; // = 5;
        public static int nprocesses = 0; // = 5;
        public static int hole_id = -1;
        public static int reserved_id = 0;
        public static int max_size = 0;
        public static string type; //="first_fit";
        public static int memory_size;
        public static List<Entry> holes_info = new List<Entry>();

        public static List<Entry> allocated_info = new List<Entry>(nprocesses);
        public static List<Entry> waiting = new List<Entry>(nprocesses);
        public static List<Entry> output_with_holes = new List<Entry>(nprocesses+holes_info.Count());
        public static List<Entry> output_with_reserved=new List<Entry>();

        public static int waiting_size(){
            int waiting_size = 0;
            for (int i = 0; i < waiting.Count; ++i)
            {
                waiting_size += waiting[i].size;
            }
            return waiting_size;
        }

        static void sort(ref List<Entry> x, string s)
        {
            bool swapped;

            if (s == "start")
                do
                {
                    swapped = false;
                    for (int i = 0; i < x.Count - 1; ++i) 
                    {
                        if (x[i].start > x[i + 1].start)
                        {
                            Entry temp = new Entry(x[i]);
                            x[i] = x[i + 1];
                            x[i + 1] = temp;
                            swapped = true;
                        }
                    }
                } while (swapped);

            else if (s == "size")
                do
                {
                    swapped = false;
                    for (int i = 0; i < x.Count - 1; ++i) 
                    {
                        if (x[i].size > x[i + 1].size)
                        {
                            Entry temp = new Entry(x[i]);
                            x[i] = x[i + 1];
                            x[i + 1] = temp;
                            swapped = true;
                        }
                    }
                } while (swapped);
        }

        static private void Compact()
        {
            int i = 0;
            Entry hole = new Entry(-1, 0);

            instert_holes();
            insert_reserved();
            allocated_info.Clear();
            holes_info.Clear();
            hole.end = memory_size - 1;

            while (i < output_with_reserved.Count)
            {
                Entry p = output_with_reserved[i];

                if (p.id == -1)
                {
                    hole.size += p.size;
                    output_with_reserved[i].end = p.start - 1;
                    i++;
                    while (i < output_with_reserved.Count && output_with_reserved[i].id != -1)
                    {
                        output_with_reserved[i].start = output_with_reserved[i - 1].end + 1;
                        output_with_reserved[i].end = output_with_reserved[i].start + output_with_reserved[i].size - 1;

                        if (output_with_reserved[i].id != 0)
                            allocated_info.Add(new Entry(output_with_reserved[i]));
                        i++;
                    }
                    if (i < output_with_reserved.Count)
                    {
                        output_with_reserved[i].start = output_with_reserved[i - 1].end + 1;
                        output_with_reserved[i].end = output_with_reserved[i].start + output_with_reserved[i].size - 1;
                    }
                }
                else 
                {
                    if (p.id != 0)
                        allocated_info.Add(new Entry(output_with_reserved[i]));
                    i++;
                }
            }
            holes_info.Add(new Entry(hole));
            holes_info.Last().start = memory_size - hole.size;
        }

        public static void Allocate(ref List<Entry> input_processes)
        {
            if (type == "first_fit")
                sort(ref holes_info, "start");

            else if (type == "best_fit")
                sort(ref holes_info, "size");

            else return;

            int j = 0;
            while (j < input_processes.Count && holes_info.Count > 0)
            {
                int i = 0;
                int n = holes_info.Count;
                for (i = 0; i < n; i++)
                {
                    if (input_processes[j].size < holes_info[i].size)
                    {
                        allocated_info.Add(new Entry(input_processes[j].id, holes_info[i].start, input_processes[j].size));
                        holes_info[i].start = allocated_info.Last().end + 1;
                        holes_info[i].size -= allocated_info.Last().size;
                        input_processes.RemoveAt(j);
                        break;
                    }
                    else if (input_processes[j].size == holes_info[i].size)
                    {
                        allocated_info.Add(new Entry(input_processes[j].id, holes_info[i].start, input_processes[j].size));
                        holes_info.RemoveAt(i);
                        input_processes.RemoveAt(j);
                        break;
                    }
                }
                if (i == n)
                {
                    if(handle_compaction(input_processes[j]))
                            input_processes.RemoveAt(j);
                    else
                        j++;
                }
            }
            sort(ref allocated_info, "start");
        }

        public static void Allocate(Entry process)
        {
            if (type == "first_fit")
                sort(ref holes_info, "start");

            else if (type == "best_fit")
                sort(ref holes_info, "size");

            else return;

            int n = holes_info.Count;
            int i = 0;
            for (i = 0; i < n; i++)
            {
                if (process.size < holes_info[i].size)
                {
                    allocated_info.Add(new Entry(process.id, holes_info[i].start,process.size));
                    holes_info[i].start = allocated_info.Last().end + 1;
                    holes_info[i].size -= allocated_info.Last().size;
                    break;
                }
                else if (process.size == holes_info[i].size)
                {
                    allocated_info.Add(new Entry(process.id, holes_info[i].start,process.size));
                    holes_info.RemoveAt(i);
                    break;
                }
            }
            if (i == n)
            {
                bool compacted = handle_compaction(process);
                if(!compacted)
                    waiting.Add(new Entry(process.id, process.size));
            }
            else
                sort(ref allocated_info, "start");
        }

        public static void DeAllocate(int id)
        {
            int allocated_info_index;
            int holes_info_index;

            if (type == "best_fit") sort(ref holes_info, "start");

            for (allocated_info_index = 0; allocated_info_index < allocated_info.Count; allocated_info_index++)
                if (allocated_info[allocated_info_index].id == id) break;
            if (allocated_info_index == allocated_info.Count) return;

            for (holes_info_index = 0; holes_info_index < holes_info.Count; holes_info_index++)
                if (allocated_info[allocated_info_index].end < holes_info[holes_info_index].start) break;

            if  (holes_info_index > 0 && holes_info_index < holes_info.Count && 
                (allocated_info[allocated_info_index].start == holes_info[holes_info_index - 1].end + 1) &&
                (allocated_info[allocated_info_index].end == holes_info[holes_info_index].start - 1))
            {
                holes_info[holes_info_index - 1].size += allocated_info[allocated_info_index].size + holes_info[holes_info_index].size;
                holes_info[holes_info_index - 1].end = holes_info[holes_info_index].end;
                holes_info.RemoveAt(holes_info_index);
            }
            else if (holes_info_index < holes_info.Count && 
                 allocated_info[allocated_info_index].end == holes_info[holes_info_index].start - 1)
            {
                holes_info[holes_info_index].size += allocated_info[allocated_info_index].size;
                holes_info[holes_info_index].start = allocated_info[allocated_info_index].start;
            }
            else if (holes_info_index > 0 && allocated_info[allocated_info_index].start == holes_info[holes_info_index - 1].end + 1)
            {
                holes_info[holes_info_index - 1].size += allocated_info[allocated_info_index].size;
                holes_info[holes_info_index - 1].end = allocated_info[allocated_info_index].end;
            }
            else
            {
                holes_info.Insert(holes_info_index, allocated_info[allocated_info_index]);
                holes_info[holes_info_index].id = hole_id;
            }
            allocated_info.RemoveAt(allocated_info_index);
            if (type == "best_fit") sort(ref holes_info, "size");

            Allocate(ref waiting);
        }

        public static void instert_holes()
        {
            int holes_index = 0, allocated_index = 0;
            output_with_holes = new List<Entry>(holes_info.Count + allocated_info.Count);

            if (type == "best_fit") sort(ref holes_info, "start");

            while (allocated_index < allocated_info.Count)
            {
                if (holes_index < holes_info.Count && holes_info[holes_index].start < allocated_info[allocated_index].start)
                {
                    
                    output_with_holes.Add(new Entry(holes_info[holes_index]));
                    holes_index++;
                }
                else
                {
                    
                    output_with_holes.Add(new Entry(allocated_info[allocated_index]));
                    allocated_index++;
                }
            }

            while (holes_index < holes_info.Count)
            {
               
                output_with_holes.Add(new Entry(holes_info[holes_index]));
                holes_index++;
            }
        }

        public static bool merge_input_holes()
        {
            int index = 0;
            sort(ref holes_info, "start");
            List<Entry> temp = new List<Entry>(holes_info);
            for (int i = 0; i < temp.Count - 1; ++i)
            {
                if (temp[i].end >= temp[i + 1].start) return false;
                if (temp[i].end + 1 == temp[i + 1].start)
                {
                    holes_info[index].end = temp[i + 1].end;
                    holes_info[index].size += temp[i + 1].size;
                }
                else
                    holes_info[++index] = temp[i + 1];
            }
            holes_info.RemoveRange(index + 1, holes_info.Count - index - 1);
            return true;
        }

        public static void insert_reserved()
        {
            int n = output_with_holes.Count;
            if (output_with_holes[0].start > 0) ++n;
            if (output_with_holes.Last().end < memory_size-1) ++n;
            for (int i = 1; i < output_with_holes.Count; ++i)
                if (output_with_holes[i].start > output_with_holes[i-1].end+1) ++n;

            output_with_reserved = new List<Entry>(n);

            if (output_with_holes[0].start > 0)
                output_with_reserved.Add(new Entry(reserved_id, 0, output_with_holes[0].start));

            output_with_reserved.Add(output_with_holes[0]);

            for (int i = 1; i < output_with_holes.Count; ++i)
            {
                if ((output_with_holes[i].start > output_with_holes[i-1].end+1))
                    output_with_reserved.Add(new Entry(reserved_id, output_with_holes[i - 1].end + 1, output_with_holes[i].start - output_with_holes[i - 1].end - 1));
                output_with_reserved.Add(output_with_holes[i]);
            }
            if (output_with_holes.Last().end < memory_size-1)
                output_with_reserved.Add(new Entry(reserved_id, output_with_holes.Last().end + 1, memory_size - output_with_holes.Last().end - 1));
        }

        public static void reserved_DeAllocate(int start)
        {
            int reserved_index;

            for (reserved_index = 0; reserved_index < output_with_reserved.Count; reserved_index++)
                if (output_with_reserved[reserved_index].start == start) break;
            if (reserved_index == output_with_reserved.Count) return;

            allocated_info.Add(new Entry(-531, start, output_with_reserved[reserved_index].size));  //-531 magic value :"D 
            DeAllocate(-531);
        }

        public static void setMaxSize()
        {
            //clear
            max_size = 0;
        /*-----------      doing 2 things        ---------
             * maxSize of single record
             * total size
         */
            for (int i = 0; i < output_with_reserved.Count(); i++)
            {
                if (output_with_reserved[i].size > max_size)
                    max_size = output_with_reserved[i].size;
            }

        }
   
        private static bool canCompact(int size)
        {
            int available = 0;
            for (int i = 0; i < holes_info.Count; ++i)
            {
                available += holes_info[i].size;
                if (size <= available) return true;
            }
            return false;
        }
     
        private static bool handle_compaction(Entry p)
        { //hint, I don't push any thing to the waiting list
            bool canCompact = Program.canCompact(p.size);
            if (!canCompact)
            {
                //MessageBox.Show("no free space to allocate process " + p.id +
                //  "\nit will be allocated whenever it's possible :)", "Notice");
                return false;
            }
            else
            {
                DialogResult userResp = MessageBox.Show("using compact can allocate P"+p.id+",\n"
                    + "Do you want to apply compact?"
                    ,
                    "No enough space",
                    MessageBoxButtons.YesNo
                    ,
                    MessageBoxIcon.Question
                    );
                if (userResp.ToString() == "No")
                {
                   // MessageBox.Show("p" + p.id + " will be in the waiting queue" +
                  //"\nit will be allocated whenever it's possible :)", "OK");
                    return false;
                }
                else //compact
                {
                    //use
                    Compact();
                    Allocate(p);
                    return true;
                }
            }
        }

        static void Main(string[] args)
        {
            //let form1 work
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            /* -------------------------------- back end testing here ------------------------------------------*/

    //        type = "first_fit";

    //        holes_info.Add(new Entry(hole_id, 0, 100));     // hole_id   start   size
    //        holes_info.Add(new Entry(hole_id, 200, 500));
    //        holes_info.Add(new Entry(hole_id, 800, 200));
    //        holes_info.Add(new Entry(hole_id, 1100, 300));
    //        holes_info.Add(new Entry(hole_id, 1500, 600));

    //        /*
    //        input_processes.Add(212);
    //        input_processes.Add(417);
    //        input_processes.Add(112);
    //        input_processes.Add(426);
    //         */
            
    //        /*
    //        Console.WriteLine("--------input processes--------");
    //        foreach (int i in input_processes)
    //        {
    //            Console.WriteLine(i);
    //        }
    //        */

    //        Console.WriteLine();
    //        Console.WriteLine("--------input holes--------");
    //        foreach (Entry i in holes_info)
    //        {
    //            Console.Write(i.id);
    //            Console.Write("  ");
    //            Console.Write(i.size);
    //            Console.Write("  ");
    //            Console.Write(i.start);
    //            Console.Write("  ");
    //            Console.WriteLine(i.end);
    //        }

    //        Allocate(new Entry(1, 212));   // id  size
    //        Allocate(new Entry(2, 417));
    //        Allocate(new Entry(3, 112));
    //        Allocate(new Entry(4, 426));

    //        Console.WriteLine();
    //        Console.WriteLine("--------allocated processes--------");
    //        foreach (Entry i in allocated_info)
    //        {
    //            Console.Write(i.id);
    //            Console.Write("  ");
    //            Console.Write(i.size);
    //            Console.Write("  ");
    //            Console.Write(i.start);
    //            Console.Write("  ");
    //            Console.WriteLine(i.end);
    //        }

    //        Console.WriteLine();
    //        Console.WriteLine("--------holes--------");
    //        foreach (Entry i in holes_info)
    //        {
    //            Console.Write(i.id);
    //            Console.Write("  ");
    //            Console.Write(i.size);
    //            Console.Write("  ");
    //            Console.Write(i.start);
    //            Console.Write("  ");
    //            Console.WriteLine(i.end);
    //        }

    //        instert_holes();

    //        Console.WriteLine();
    //        Console.WriteLine("--------output--------");
    //        foreach (Entry i in output_with_holes)
    //        {
    //            Console.Write(i.id);
    //            Console.Write("  ");
    //            Console.Write(i.size);
    //            Console.Write("  ");
    //            Console.Write(i.start);
    //            Console.Write("  ");
    //            Console.WriteLine(i.end);
    //        }


    //        //DeAllocate(3);
    //        //DeAllocate(1);
    //        //DeAllocate(4);
            
            
    //        DeAllocate(2);       // id
    //        instert_holes();

    //        Console.WriteLine();
    //        Console.WriteLine("--------output--------");
    //        foreach (Entry i in output_with_holes)
    //        {
    //            Console.Write(i.id);
    //            Console.Write("  ");
    //            Console.Write(i.size);
    //            Console.Write("  ");
    //            Console.Write(i.start);
    //            Console.Write("  ");
    //            Console.WriteLine(i.end);
    //        }
            
            /* ---------------------- End of backend testing --------------------------------------------------- */
        }


    } //of program class

    public class Entry
    {
        public int id { get; set; }
        public int start { get; set; }
        public int size { get; set; }
        public int end { get; set; }

        public Entry(int id, int start, int size)
        { this.id = id; this.start = start; this.size = size; this.end = start + size - 1; }

        public Entry(int id, int size)
        { this.id = id; this.size = size; }

        public Entry(Entry E)
        {
            id = E.id; start = E.start; size = E.size; end = E.end;
        }
        static public bool operator <(Entry x, Entry y) { return (x.size < y.size); }
        static public bool operator >(Entry x, Entry y) { return (x.size > y.size); }
    };
}