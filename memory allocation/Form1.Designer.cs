namespace memory_allocation
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.n_holes = new System.Windows.Forms.TextBox();
            this.panel = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.m_size = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Name = "label1";
            // 
            // n_holes
            // 
            resources.ApplyResources(this.n_holes, "n_holes");
            this.n_holes.Name = "n_holes";
            this.n_holes.TextChanged += new System.EventHandler(this.n_holes_TextChanged);
            // 
            // panel
            // 
            resources.ApplyResources(this.panel, "panel");
            this.panel.Name = "panel";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DeepSkyBlue;
            resources.ApplyResources(this.button1, "button1");
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_size
            // 
            resources.ApplyResources(this.m_size, "m_size");
            this.m_size.Name = "m_size";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Name = "label2";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.Controls.Add(this.m_size);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.n_holes);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox n_holes;
        private System.Windows.Forms.TableLayoutPanel panel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox m_size;
        private System.Windows.Forms.Label label2;
    }
}