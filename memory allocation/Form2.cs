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
            int p_size_int = Int32.Parse(p_size.Text);
            string t = (method.Text=="best fit")? "best_fit" : "first_fit";
            //preparing input:
            Program.type = t;
            Program.nprocesses++;
            //Entry entry = new Entry(Program.nprocesses,p_size_int);
            Program.Allocate(new Entry(Program.nprocesses,p_size_int));
            //draw();
        }
    }
}
