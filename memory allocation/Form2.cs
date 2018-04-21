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
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (p_size.Text == "" || method.Text == "")
            {
                MessageBox.Show("Fill all possible data");
                return;
            }
            int p_size_int = Int32.Parse(p_size.Text); p_size.Text = "";
            string t = (method.Text=="best fit")? "best_fit" : "first_fit";
            //preparing input:
            Program.type = t;
            Program.nprocesses++;
            //Entry entry = new Entry(id, size);
            Program.Allocate(new Entry(Program.nprocesses,p_size_int));
            draw();
        }
        private void draw()
        {
            Program.instert_holes();
            //do some thing
            MessageBox.Show("I'm drawing "+ Program.output_with_holes.Count.ToString()+" items");
            //clear
            draw_area.Controls.Clear();
            
            //draw
            int last_address = Program.output_with_holes.Last().end;

            draw_area.RowCount = Program.output_with_holes.Count;
            draw_area.RowStyles.Clear();
            //draw_area.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
          
            for (int i = 0; i < Program.output_with_holes.Count; ++i)
            {
                draw_area.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
                Label item = new Label();
                item.Text = Program.output_with_holes[i].id.ToString();
                draw_area.Controls.Add(item, 0, i);
            }
        }
    }
}
