using Model.DTOs;

namespace TimeTableProj
{
    partial class NewClass
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewClass));
            this.label1 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.HoursView = new System.Windows.Forms.DataGridView();
            this.Lesson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimesPerWeek = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hourBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.AddClass = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.HoursView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hourBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Име:";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(78, 19);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(100, 23);
            this.name.TabIndex = 1;
            // 
            // HoursView
            // 
            this.HoursView.AllowUserToAddRows = false;
            this.HoursView.AllowUserToDeleteRows = false;
            this.HoursView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.HoursView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.HoursView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HoursView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Lesson,
            this.TimesPerWeek});
            this.HoursView.Location = new System.Drawing.Point(12, 50);
            this.HoursView.Name = "HoursView";
            this.HoursView.RowTemplate.Height = 25;
            this.HoursView.Size = new System.Drawing.Size(263, 278);
            this.HoursView.TabIndex = 3;
            // 
            // Lesson
            // 
            this.Lesson.HeaderText = "Предмет";
            this.Lesson.Name = "Lesson";
            this.Lesson.ReadOnly = true;
            // 
            // TimesPerWeek
            // 
            this.TimesPerWeek.HeaderText = "Часове на седмица";
            this.TimesPerWeek.Name = "TimesPerWeek";
            this.TimesPerWeek.Width = 120;
            // 
            // hourBindingSource
            // 
            this.hourBindingSource.DataSource = typeof(SubjectDTO);
            // 
            // AddClass
            // 
            this.AddClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.AddClass.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AddClass.Location = new System.Drawing.Point(188, 346);
            this.AddClass.Name = "AddClass";
            this.AddClass.Size = new System.Drawing.Size(87, 29);
            this.AddClass.TabIndex = 4;
            this.AddClass.Text = "Добави";
            this.AddClass.UseVisualStyleBackColor = false;
            this.AddClass.Click += new System.EventHandler(this.AddClass_Click);
            // 
            // NewClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 386);
            this.Controls.Add(this.AddClass);
            this.Controls.Add(this.HoursView);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewClass";
            this.Text = "Нов клас";
            this.Load += new System.EventHandler(this.NewClass_Load);
            ((System.ComponentModel.ISupportInitialize)(this.HoursView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hourBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.DataGridView HoursView;
        private System.Windows.Forms.BindingSource hourBindingSource;
        private System.Windows.Forms.Button AddClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lesson;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimesPerWeek;
    }
}