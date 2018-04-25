namespace memory_allocation
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.p_size = new System.Windows.Forms.TextBox();
            this.method = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.draw_area = new System.Windows.Forms.TableLayoutPanel();
            this.wait_panel = new System.Windows.Forms.TableLayoutPanel();
            this.memory = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "process size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(411, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "method";
            // 
            // p_size
            // 
            this.p_size.Location = new System.Drawing.Point(97, 54);
            this.p_size.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.p_size.Name = "p_size";
            this.p_size.Size = new System.Drawing.Size(180, 27);
            this.p_size.TabIndex = 2;
            // 
            // method
            // 
            this.method.FormattingEnabled = true;
            this.method.Items.AddRange(new object[] {
            "best fit",
            "first fit"});
            this.method.Location = new System.Drawing.Point(416, 54);
            this.method.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.method.Name = "method";
            this.method.Size = new System.Drawing.Size(219, 27);
            this.method.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightBlue;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(743, 29);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 54);
            this.button1.TabIndex = 4;
            this.button1.Text = "Allocate";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // draw_area
            // 
            this.draw_area.AutoSize = true;
            this.draw_area.ColumnCount = 3;
            this.draw_area.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.85714F));
            this.draw_area.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.14286F));
            this.draw_area.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 264F));
            this.draw_area.Location = new System.Drawing.Point(203, 174);
            this.draw_area.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.draw_area.Name = "draw_area";
            this.draw_area.RowCount = 1;
            this.draw_area.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.draw_area.Size = new System.Drawing.Size(514, 259);
            this.draw_area.TabIndex = 5;
            // 
            // wait_panel
            // 
            this.wait_panel.AutoSize = true;
            this.wait_panel.ColumnCount = 1;
            this.wait_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.wait_panel.Location = new System.Drawing.Point(783, 154);
            this.wait_panel.Name = "wait_panel";
            this.wait_panel.RowCount = 1;
            this.wait_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.wait_panel.Size = new System.Drawing.Size(200, 100);
            this.wait_panel.TabIndex = 6;
            // 
            // memory
            // 
            this.memory.AutoSize = true;
            this.memory.Location = new System.Drawing.Point(406, 142);
            this.memory.Name = "memory";
            this.memory.Size = new System.Drawing.Size(77, 19);
            this.memory.TabIndex = 7;
            this.memory.Text = "Memory";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(995, 582);
            this.Controls.Add(this.memory);
            this.Controls.Add(this.wait_panel);
            this.Controls.Add(this.draw_area);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.method);
            this.Controls.Add(this.p_size);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Century Schoolbook", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.Name = "Form2";
            this.Text = "processes handler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox p_size;
        private System.Windows.Forms.ComboBox method;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel draw_area;
        private System.Windows.Forms.TableLayoutPanel wait_panel;
        private System.Windows.Forms.Label memory;
    }
}