namespace TimeTableProj
{
    partial class NewClassroom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewClassroom));
            this.NumberLabel = new System.Windows.Forms.Label();
            this.cabinetNumber = new System.Windows.Forms.TextBox();
            this.SubjectLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AddCabinet = new System.Windows.Forms.Button();
            this.subjectSelect = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // NumberLabel
            // 
            this.NumberLabel.AutoSize = true;
            this.NumberLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumberLabel.Location = new System.Drawing.Point(32, 26);
            this.NumberLabel.Name = "NumberLabel";
            this.NumberLabel.Size = new System.Drawing.Size(152, 21);
            this.NumberLabel.TabIndex = 0;
            this.NumberLabel.Text = "Номер на кабинета:";
            // 
            // cabinetNumber
            // 
            this.cabinetNumber.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cabinetNumber.Location = new System.Drawing.Point(207, 26);
            this.cabinetNumber.Name = "cabinetNumber";
            this.cabinetNumber.Size = new System.Drawing.Size(121, 23);
            this.cabinetNumber.TabIndex = 1;
            // 
            // SubjectLabel
            // 
            this.SubjectLabel.AutoSize = true;
            this.SubjectLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SubjectLabel.Location = new System.Drawing.Point(32, 82);
            this.SubjectLabel.Name = "SubjectLabel";
            this.SubjectLabel.Size = new System.Drawing.Size(170, 21);
            this.SubjectLabel.TabIndex = 2;
            this.SubjectLabel.Text = "Специфичен предмет:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.AddCabinet);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 348);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(363, 52);
            this.panel1.TabIndex = 4;
            // 
            // AddCabinet
            // 
            this.AddCabinet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.AddCabinet.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AddCabinet.Location = new System.Drawing.Point(264, 11);
            this.AddCabinet.Name = "AddCabinet";
            this.AddCabinet.Size = new System.Drawing.Size(87, 29);
            this.AddCabinet.TabIndex = 5;
            this.AddCabinet.Text = "Добави";
            this.AddCabinet.UseVisualStyleBackColor = false;
            this.AddCabinet.Click += new System.EventHandler(this.AddCabinet_Click);
            // 
            // subjectSelect
            // 
            this.subjectSelect.FormattingEnabled = true;
            this.subjectSelect.Location = new System.Drawing.Point(208, 84);
            this.subjectSelect.Name = "subjectSelect";
            this.subjectSelect.Size = new System.Drawing.Size(121, 23);
            this.subjectSelect.TabIndex = 5;
            // 
            // NewClassroom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 400);
            this.Controls.Add(this.subjectSelect);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.SubjectLabel);
            this.Controls.Add(this.cabinetNumber);
            this.Controls.Add(this.NumberLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewClassroom";
            this.Text = "Нов Кабинет";
            this.Load += new System.EventHandler(this.NewClassroom_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NumberLabel;
        private System.Windows.Forms.TextBox cabinetNumber;
        private System.Windows.Forms.Label SubjectLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button AddCabinet;
        private System.Windows.Forms.ComboBox subjectSelect;
    }
}