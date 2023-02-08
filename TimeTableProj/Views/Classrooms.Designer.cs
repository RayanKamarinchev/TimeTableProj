namespace TimeTableProj
{
    partial class classrooms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(classrooms));
            this.sp = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numberOrderBtn = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.addClassrooom = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // sp
            // 
            this.sp.AutoScroll = true;
            this.sp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sp.Location = new System.Drawing.Point(44, 0);
            this.sp.Name = "sp";
            this.sp.Size = new System.Drawing.Size(706, 399);
            this.sp.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(44, 399);
            this.panel3.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(750, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(50, 399);
            this.panel2.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numberOrderBtn);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 399);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 51);
            this.panel1.TabIndex = 5;
            // 
            // numberOrderBtn
            // 
            this.numberOrderBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.numberOrderBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.numberOrderBtn.Location = new System.Drawing.Point(12, 6);
            this.numberOrderBtn.Name = "numberOrderBtn";
            this.numberOrderBtn.Size = new System.Drawing.Size(179, 36);
            this.numberOrderBtn.TabIndex = 3;
            this.numberOrderBtn.Text = "Подреди номерата";
            this.numberOrderBtn.UseVisualStyleBackColor = false;
            this.numberOrderBtn.Click += new System.EventHandler(this.numberOrderBtn_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.addClassrooom);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(637, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(163, 51);
            this.panel4.TabIndex = 1;
            // 
            // addClassrooom
            // 
            this.addClassrooom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.addClassrooom.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.addClassrooom.Location = new System.Drawing.Point(8, 6);
            this.addClassrooom.Name = "addClassrooom";
            this.addClassrooom.Size = new System.Drawing.Size(145, 36);
            this.addClassrooom.TabIndex = 0;
            this.addClassrooom.Text = "Добави Стая";
            this.addClassrooom.UseVisualStyleBackColor = false;
            this.addClassrooom.Click += new System.EventHandler(this.addClassrooom_Click);
            // 
            // classrooms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.sp);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "classrooms";
            this.Text = "Стаи";
            this.Activated += new System.EventHandler(this.Classes_Activated);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel sp;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button addClassrooom;
        private System.Windows.Forms.Button numberOrderBtn;
    }
}