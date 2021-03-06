﻿using System;
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
            if (!Int32.TryParse(n_holes.Text, out Program.nholes) || Program.nholes < 1)
            {
                panel.Controls.Clear();
                hole_adds.Clear();
                hole_sizes.Clear();
                button1.Visible = false;
                if (n_holes.Text != "")
                {
                    MessageBox.Show("My dear, Enter a positive integer number");
                    n_holes.Text = "";
                }
                return;
            }
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
            panel.Controls.Add(new Label() { Text = "hole size",Size=new Size(120,20) }, 0, 0);
            panel.Controls.Add(new Label() { Text = "hole address", Size = new Size(120, 20) }, 1, 0);
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
            int test;
            for (int i = 0; i < Program.nholes ; ++i)
            {
                
                if (hole_sizes[i].Text == "")
                {
                    Exception er = new Exception("please fill all fields");
                    throw (er);
                }
                if (! Int32.TryParse(hole_sizes[i].Text, out test))
                {
                    Exception er = new Exception("\"hole size\" must be a positive integer number");
                    throw (er);
                }
                if (test <= 0)
                {
                    Exception er = new Exception("\"hole size\" must be a positive number");
                    throw (er);
                }
                if (hole_adds[i].Text == "")
                {
                    Exception er = new Exception("please fill all fields");
                    throw (er);
                }
                if (!Int32.TryParse(hole_adds[i].Text, out test))
                {
                    Exception er = new Exception("\"hole address\" must be zero or positive number");
                    throw (er);
                }
                
                Program.holes_info.Add(new Entry(-1,
                                            Int32.Parse(hole_adds[i].Text),
                                            Int32.Parse(hole_sizes[i].Text))
                                      );
            }
            if (!Program.merge_input_holes()) 
            {
                Exception er = new Exception("overlapping between holes is not allowed");
                throw (er);
            }
            if (!Int32.TryParse(m_size.Text, out Program.memory_size) || Program.memory_size < 1)
            {
                Exception er = new Exception("Enter a positive integer number in the memory size slot");
                throw (er);
            }
            if (Program.holes_info.Last().end > Program.memory_size-1)
            {
                Exception er = new Exception("memory size should be greater than or equal the end of last hole");
                throw (er);
            }
        }
    }
}
