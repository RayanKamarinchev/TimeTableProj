using Model.DTOs;

namespace TimeTableProj
{
    partial class NewHour
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewHour));
            this.hourBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.teacherBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.hourBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.HourTable = new System.Windows.Forms.DataGridView();
            this.Lesson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teacherBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.hourBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teacherBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hourBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HourTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teacherBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // hourBindingSource
            // 
            this.hourBindingSource.DataSource = typeof(Model.DTOs.SubjectDTO);
            // 
            // teacherBindingSource
            // 
            this.teacherBindingSource.DataMember = "Teacher";
            this.teacherBindingSource.DataSource = this.hourBindingSource;
            // 
            // hourBindingSource1
            // 
            this.hourBindingSource1.DataSource = typeof(Model.DTOs.SubjectDTO);
            // 
            // HourTable
            // 
            this.HourTable.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.HourTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.HourTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HourTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Lesson,
            this.Type,
            this.Id});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.HourTable.DefaultCellStyle = dataGridViewCellStyle2;
            this.HourTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.HourTable.Location = new System.Drawing.Point(20, 13);
            this.HourTable.Name = "HourTable";
            this.HourTable.RowTemplate.Height = 25;
            this.HourTable.Size = new System.Drawing.Size(461, 379);
            this.HourTable.TabIndex = 0;
            // 
            // Lesson
            // 
            this.Lesson.HeaderText = "Предмети";
            this.Lesson.Name = "Lesson";
            this.Lesson.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Lesson.Width = 200;
            // 
            // Type
            // 
            this.Type.HeaderText = "Тип Предмет";
            this.Type.Items.AddRange(new object[] {
            "Обикновен",
            "Компютърен",
            "Биология",
            "Химия",
            "Физика",
            "НаОткрито"});
            this.Type.Name = "Type";
            this.Type.Width = 200;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // teacherBindingSource1
            // 
            this.teacherBindingSource1.DataMember = "Teacher";
            this.teacherBindingSource1.DataSource = this.hourBindingSource;
            // 
            // NewHour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(508, 404);
            this.Controls.Add(this.HourTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewHour";
            this.Text = "Нов Предмет";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewHour_FormClosing);
            this.Load += new System.EventHandler(this.NewHour_Load);
            ((System.ComponentModel.ISupportInitialize)(this.hourBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teacherBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hourBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HourTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teacherBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource hourBindingSource;
        private System.Windows.Forms.BindingSource teacherBindingSource;
        private System.Windows.Forms.BindingSource hourBindingSource1;
        private System.Windows.Forms.DataGridView HourTable;
        private System.Windows.Forms.BindingSource teacherBindingSource1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lesson;
        private System.Windows.Forms.DataGridViewComboBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
    }
}