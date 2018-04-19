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
        int n_holes_int;
        List<TextBox> hole_sizes = new List<TextBox>();
        List<TextBox> hole_adds = new List<TextBox>();
        public Form1()
        {
            InitializeComponent();
        }

        private void n_holes_TextChanged(object sender, EventArgs e)
        {
            if (n_holes.Text == "") return;
            n_holes_int = Int32.Parse(n_holes.Text);
            //clear
            panel.Controls.Clear();
            hole_adds.Clear();
            hole_sizes.Clear();
            button1.Visible = false;
            //
            panel.RowCount = n_holes_int + 1;
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            panel.Controls.Add(new Label() { Text = "hole size" }, 0, 0);
            panel.Controls.Add(new Label() { Text = "hole address" }, 1, 0);
            for (int i = 1; i <= n_holes_int; ++i)
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
    }
}
