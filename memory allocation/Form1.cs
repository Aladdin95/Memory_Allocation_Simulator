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
    public partial class Form1 : Form
    {
       
        List<TextBox> hole_sizes = new List<TextBox>();
        List<TextBox> hole_adds = new List<TextBox>();

        //int n_holes_int;
        //List<int> hole_sizes_int = new List<int>();
        //List<int> hole_adds_int = new List<int>();
        public Form1()
        {
            InitializeComponent();
        }

        private void n_holes_TextChanged(object sender, EventArgs e)
        {
            if (n_holes.Text == "" || n_holes.Text == "0") return;
            Program.nholes = Int32.Parse(n_holes.Text);
            //clear
            panel.Controls.Clear();
            hole_adds.Clear();
            hole_sizes.Clear();
            button1.Visible = false;
            //
            panel.RowCount = Program.nholes + 1;
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            panel.Controls.Add(new Label() { Text = "hole size" }, 0, 0);
            panel.Controls.Add(new Label() { Text = "hole address" }, 1, 0);
            for (int i = 1; i <= Program.nholes; ++i)
            {
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
                hole_sizes.Add(new TextBox());
                hole_adds.Add(new TextBox());
                panel.Controls.Add(hole_sizes[i-1], 0, i);
                panel.Controls.Add(hole_adds[i - 1], 1, i);
            }

            button1.Location = new Point(button1.Location.X, 
                hole_adds.Last().Location.Y + 50 + panel.Location.Y
                );
            button1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //clear
            Program.holes_info.Clear();
           
            //filling
            try
            {
                fill_data();
                //let form2 work
                Form2 m2 = new Form2();
                m2.ShowDialog();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void fill_data()
        {
            //filling
            for (int i = 0; i < Program.nholes ; ++i)
            {
                if (hole_sizes[i].Text == "")
                {
                    Exception er = new Exception("please fill all fields");
                    throw (er);
                }
                if (hole_adds[i].Text == "")
                {
                    Exception er = new Exception("please fill all fields");
                    throw (er);
                }
                Program.holes_info.Add(new Entry(-1,
                                            Int32.Parse(hole_adds[i].Text),
                                            Int32.Parse(hole_sizes[i].Text))
                                      );
            }
        }
    }
}
