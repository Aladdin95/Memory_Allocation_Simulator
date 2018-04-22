using System;
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
        public static int max_size = 0;
        public static string type; //="first_fit";
        public static List<Entry> holes_info = new List<Entry>();

        public static List<Entry> allocated_info = new List<Entry>(nprocesses);
        public static List<Entry> waiting = new List<Entry>(nprocesses);
        public static List<Entry> output_with_holes = new List<Entry>(nprocesses+holes_info.Count());



        static void sort(ref List<Entry> x, string s)
        {
            int i;
            int j;
            if (s == "start")
            {
                for (i = 0; i < x.Count - 1; i++)
                    for (j = i + 1; j < x.Count; j++)
                        if (x[j].start < x[i].start)
                        {
                            Entry temp = new Entry(x[i]);
                            x[i] = x[j];
                            x[j] = temp;
                        }
            }
            else if (s == "size")
                for (i = 0; i < x.Count - 1; i++)
                    for (j = i + 1; j < x.Count; j++)
                        if (x[j].size < x[i].size)
                        {
                            Entry temp = new Entry(x[i]);
                            x[i] = x[j];
                            x[j] = temp;
                        }
        }

        static void Compact()
        {

        }

        /*
        static void Allocate()
        {
            holes_info = new List<Entry>(input_holes);
            allocated_info = new List<Entry>(input_processes.Count);
            waiting = new List<Entry>(input_processes.Count);
            List<int> temp = new List<int>(input_processes);
            temp.Reverse();

            if (type == "first_fit")
                sort(ref holes_info, "start");

            else if (type == "best_fit")
                sort(ref holes_info, "size");

            else return;

            while (temp.Count > 0)
            {
                int i = 0;
                int n = holes_info.Count;
                for (i = 0; i < n; i++)
                {
                    if (temp.Last() < holes_info[i].size)
                    {
                        allocated_info.Add(new Entry(input_processes.Count - temp.Count + 1, holes_info[i].start, temp.Last()));
                        holes_info[i].start = allocated_info.Last().end + 1;
                        holes_info[i].size -= allocated_info.Last().size;
                        temp.RemoveAt(temp.Count - 1);
                        break;
                    }
                    else if (temp.Last() == holes_info[i].size)
                    {
                        allocated_info.Add(new Entry(input_processes.Count - temp.Count + 1, holes_info[i].start, temp.Last()));
                        holes_info.RemoveAt(i);
                        temp.RemoveAt(temp.Count - 1);
                        break;
                    }
                }

                if (i == n)
                {
                    waiting.Add(new Entry(input_processes.Count - temp.Count + 1, temp.Last()));
                    temp.RemoveAt(temp.Count - 1);
                }
            }
            sort(ref allocated_info, "start");
        }*/

        public static void Allocate(ref List<Entry> input_processes)
        {
            if (type == "first_fit")
                sort(ref holes_info, "start");

            else if (type == "best_fit")
                sort(ref holes_info, "size");

            else return;

            for (int j = 0; j < input_processes.Count && holes_info.Count > 0; j++)
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
                waiting.Add(new Entry(process.id, process.size));
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
                    if (holes_info[holes_index].size > max_size) max_size = holes_info[holes_index].size;
                    output_with_holes.Add(new Entry(holes_info[holes_index]));
                    holes_index++;
                }
                else
                {
                    if (allocated_info[allocated_index].size > max_size) max_size = allocated_info[allocated_index].size;
                    output_with_holes.Add(new Entry(allocated_info[allocated_index]));
                    allocated_index++;
                }
            }

            while (holes_index < holes_info.Count)
            {
                if (holes_info[holes_index].size > max_size) max_size = holes_info[holes_index].size;
                output_with_holes.Add(new Entry(holes_info[holes_index]));
                holes_index++;
            }
        }

        public static void merge_input_holes()
        {
            int index=0;
            sort(ref holes_info, "start");
            List<Entry> temp = new List<Entry>(holes_info);
            for (int i = 0; i < temp.Count-1; ++i)
            {
                if (temp[i].end + 1 == temp[i + 1].start)
                {
                    holes_info[index].end = temp[i + 1].end;
                    holes_info[index].size += temp[i + 1].size;
                }
                else 
                    index++;
            }
            holes_info.RemoveRange(index + 1, holes_info.Count - index - 1);
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