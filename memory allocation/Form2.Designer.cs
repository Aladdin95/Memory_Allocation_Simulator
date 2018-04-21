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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "process size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(224, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "method";
            // 
            // p_size
            // 
            this.p_size.Location = new System.Drawing.Point(53, 37);
            this.p_size.Name = "p_size";
            this.p_size.Size = new System.Drawing.Size(100, 20);
            this.p_size.TabIndex = 2;
            // 
            // method
            // 
            this.method.FormattingEnabled = true;
            this.method.Items.AddRange(new object[] {
            "best fit",
            "first fit"});
            this.method.Location = new System.Drawing.Point(227, 37);
            this.method.Name = "method";
            this.method.Size = new System.Drawing.Size(121, 21);
            this.method.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(405, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 37);
            this.button1.TabIndex = 4;
            this.button1.Text = "Allocate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // draw_area
            // 
            this.draw_area.AutoSize = true;
            this.draw_area.ColumnCount = 2;
            this.draw_area.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.84875F));
            this.draw_area.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.15124F));
            this.draw_area.Location = new System.Drawing.Point(112, 97);
            this.draw_area.Name = "draw_area";
            this.draw_area.RowCount = 1;
            this.draw_area.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.draw_area.Size = new System.Drawing.Size(384, 177);
            this.draw_area.TabIndex = 5;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(544, 286);
            this.Controls.Add(this.draw_area);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.method);
            this.Controls.Add(this.p_size);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
    }
}