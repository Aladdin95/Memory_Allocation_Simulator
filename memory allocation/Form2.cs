using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace memory_allocation
{
    public partial class Form2 : Form
    {
        List<Color> colors = new List<Color>();
        List <Button> de_allocators = new List<Button>();
        public Form2()
        {
            InitializeComponent();
            //reset all values as holes info might change
            Program.nprocesses = 0;
            Program.output_with_holes.Clear();
            Program.output_with_reserved.Clear();
            Program.waiting.Clear();
            Program.allocated_info.Clear();
            draw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int p_size_int;
            if (!Int32.TryParse(p_size.Text, out p_size_int) || p_size_int < 1)
            {
                MessageBox.Show("Fill \"process size\" with a positive integer", "ERROR");
                return;
            }
            string t;
            if (method.Text == "best fit") t = "best_fit";
            else
            {
                t = "first_fit";
                method.Text = "first fit";
            }
            p_size.Text = "";
            //preparing input:
            Program.type = t;
            Program.nprocesses++;
            Program.Allocate(new Entry(Program.nprocesses,p_size_int));
            draw();
        }
        private void draw()
        {
            Program.instert_holes();
            Program.insert_reserved();
            Program.setMaxSize();
            generate_colors();
            if(Program.max_size == 0){
                MessageBox.Show("No thing to Draw!");
                return;
            }
            //do some thing
           // MessageBox.Show("I'm drawing "+ Program.output_with_reserved.Count.ToString()+" items");
            
            //clear
            draw_area.Controls.Clear();
            de_allocators.Clear();
            
            //draw
            int min = 30, max = 150;
            int full = 350;
            draw_area.RowCount = Program.output_with_reserved.Count;
            draw_area.RowStyles.Clear();
            draw_area.ColumnStyles.Clear();
            int height;
            Color color;
            for (int i = 0; i < Program.output_with_reserved.Count; ++i)
            {
                Entry p = Program.output_with_reserved[i];
                //height = min + p.size * (max - min) / Program.max_size;
                height = min + (p.size * (full-min) / Program.memory_size);
                color = (p.id == -1) ? Color.White : (p.id == Program.reserved_id) ? 
                    Color.Gray : colors[p.id - 1];
                draw_area.RowStyles.Add(new RowStyle(SizeType.Absolute, height));

                //addresses
                draw_area.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
                draw_area.Controls.Add(new Label(){Text = "Address: "+p.start.ToString()
                , Size = new Size(200,height),
                TextAlign = ContentAlignment.TopRight,
                }, 0, i);

                //processes
                draw_area.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
                Label item = new Label();
                item.Size = new Size(200, height);
                //
                item.Text = (p.id==-1)? "hole!,": (p.id == Program.reserved_id) ? "Reserved,": "P"+p.id.ToString()+",";
                item.Text += " Size= " + p.size.ToString();
                //
                item.TextAlign = ContentAlignment.MiddleCenter;
                item.BackColor = color;
                if (p.id == -1 || p.id == Program.reserved_id)
                {
                    item.BorderStyle = BorderStyle.FixedSingle;
                }
                draw_area.Controls.Add(item, 1, i);

                //de-allocate
                draw_area.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
                if (p.id != -1)
                {
                    de_allocators.Add(new Button() {Size = new Size(60,40),
                        BackColor = Color.Salmon,
                    });
                    string del1 = "✖";
                    de_allocators.Last().Text = (p.id == Program.reserved_id)? del1 : 
                        del1 + p.id.ToString();

                    de_allocators.Last().Click += (sender, e) => { de_allocate(sender, e, p); };

                    de_allocators.Last().Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                    draw_area.Controls.Add(de_allocators.Last(), 2, i);
                }
          
            }
        }

        private void de_allocate(object sender, EventArgs e, Entry entry)
        {
            if (entry.id == Program.reserved_id)
                Program.reserved_DeAllocate(entry.start);
                //very fucking smart
            else
                Program.DeAllocate(entry.id);
            draw();
        }

        private void generate_colors()
        {
            int r, g, b;
            Random m = new Random();
            for (int i = colors.Count; i < Program.nprocesses; ++i)
            {
                r = m.Next(70, 220);
                g = m.Next(70, 250);
                b = m.Next(70, 250);
                colors.Add(Color.FromArgb(r, g, b));
            }
        }
    }

}
