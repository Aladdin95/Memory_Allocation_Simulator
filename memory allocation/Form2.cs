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
            Program.waiting.Clear();
            Program.allocated_info.Clear();
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
            generate_colors();
            if(Program.max_size == 0){
                MessageBox.Show("No thing to Draw!");
                return;
            }
            //do some thing
           // MessageBox.Show("I'm drawing "+ Program.output_with_holes.Count.ToString()+" items");
            
            //clear
            draw_area.Controls.Clear();
            de_allocators.Clear();
            
            //draw
            int min = 33, max = 300;
            draw_area.RowCount = Program.output_with_holes.Count;
            draw_area.RowStyles.Clear();
            Entry p ;
            int height;
            Color color;
            for (int i = 0; i < Program.output_with_holes.Count; ++i)
            {
                p = Program.output_with_holes[i];
                height = min + p.size * (max - min) / Program.max_size;
                color = (p.id == -1) ? Color.White : colors[p.id - 1];
                draw_area.RowStyles.Add(new RowStyle(SizeType.Absolute, height));

                //addresses
                draw_area.Controls.Add(new Label(){Text = "Address: "+p.start.ToString()
                , Size = new Size(120,height)
                }, 0, i);

                //processes
                Label item = new Label();
                item.Size = new Size(item.Size.Width, height);
                //
                item.Text = (p.id==-1)? "hole!": "Process"+p.id.ToString();
                item.Text += "\nSize= " + p.size.ToString();
                //
                item.TextAlign = ContentAlignment.MiddleCenter;
                item.BackColor = color;
                if (p.id == -1)
                {
                    item.BorderStyle = BorderStyle.FixedSingle;
                }
                draw_area.Controls.Add(item, 1, i);

                //de-allocate
                if (p.id != -1)
                {
                    de_allocators.Add(new Button() {Size = new Size(60,60),
                        BackColor = Color.Salmon,
                    });
                    string del1 = "✖";
                    de_allocators.Last().Text = del1 + p.id.ToString();
                    de_allocators.Last().Click += new EventHandler((sender,e)=>de_allocate(sender,e,p.id));

                    de_allocators.Last().Anchor = AnchorStyles.Top;
                    de_allocators.Last().Anchor = AnchorStyles.Left;
                    draw_area.Controls.Add(de_allocators.Last(), 2, i);
                }
          
            }
        }

        private void de_allocate(object sender, EventArgs e, int id)
        {
            //problem : id is static, need to create much vars in memory
            //soln1, play on sender
                string sender_info = sender.ToString();
                int id2, test; string id2_s="";
                for (int i = 0; i < sender_info.Count(); ++i)
                {
                    if(sender_info[i].ToString()!=" " && Int32.TryParse(sender_info[i].ToString(),out test))
                    {
                        id2_s += sender_info[i].ToString();
                    }
                }
                id2 = Int32.Parse(id2_s);
                //MessageBox.Show(id2.ToString());
            // end of soln 1

            Program.DeAllocate(id2);
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
