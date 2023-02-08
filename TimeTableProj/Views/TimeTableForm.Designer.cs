using System.Drawing;
using System.IO;
using Model.DTOs;

namespace TimeTableProj
{
    partial class TimeTableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeTableForm));
            this.timeTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.scroll = new System.Windows.Forms.Panel();
            this.Content = new System.Windows.Forms.Panel();
            this.TimeTableField = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.Hours = new System.Windows.Forms.Button();
            this.Classes = new System.Windows.Forms.Button();
            this.Teachers = new System.Windows.Forms.Button();
            this.Menu = new System.Windows.Forms.Panel();
            this.cabinetsView = new System.Windows.Forms.Button();
            this.classView = new System.Windows.Forms.Button();
            this.teacherView = new System.Windows.Forms.Button();
            this.viewAll = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.line = new System.Windows.Forms.Label();
            this.save = new System.Windows.Forms.Button();
            this.ExcelClassrooms = new System.Windows.Forms.Button();
            this.TeacersExcel = new System.Windows.Forms.Button();
            this.ExcelExport = new System.Windows.Forms.Button();
            this.generate = new System.Windows.Forms.Button();
            this.design = new System.Windows.Forms.Button();
            this.Classrooms = new System.Windows.Forms.Button();
            this.subjectInfoHover = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.timeTableBindingSource)).BeginInit();
            this.scroll.SuspendLayout();
            this.Content.SuspendLayout();
            this.Menu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timeTableBindingSource
            // 
            this.timeTableBindingSource.DataSource = typeof(Model.DTOs.TimeTable);
            // 
            // scroll
            // 
            this.scroll.Controls.Add(this.Content);
            this.scroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scroll.Location = new System.Drawing.Point(0, 86);
            this.scroll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.scroll.Name = "scroll";
            this.scroll.Size = new System.Drawing.Size(1117, 462);
            this.scroll.TabIndex = 4;
            // 
            // Content
            // 
            this.Content.BackColor = System.Drawing.Color.White;
            this.Content.Controls.Add(this.TimeTableField);
            this.Content.Controls.Add(this.label10);
            this.Content.Controls.Add(this.label19);
            this.Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Content.Location = new System.Drawing.Point(0, 0);
            this.Content.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Content.Name = "Content";
            this.Content.Size = new System.Drawing.Size(1117, 462);
            this.Content.TabIndex = 7;
            // 
            // TimeTableField
            // 
            this.TimeTableField.BackColor = System.Drawing.Color.White;
            this.TimeTableField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TimeTableField.Location = new System.Drawing.Point(1, 1);
            this.TimeTableField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TimeTableField.Name = "TimeTableField";
            this.TimeTableField.Size = new System.Drawing.Size(1116, 461);
            this.TimeTableField.TabIndex = 29;
            this.TimeTableField.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TimeTableField_Scroll);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Black;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Location = new System.Drawing.Point(1, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1116, 1);
            this.label10.TabIndex = 28;
            this.label10.Text = "label10";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Black;
            this.label19.Dock = System.Windows.Forms.DockStyle.Left;
            this.label19.Location = new System.Drawing.Point(0, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 462);
            this.label19.TabIndex = 18;
            this.label19.Text = "label19";
            // 
            // Hours
            // 
            this.Hours.BackColor = System.Drawing.Color.White;
            this.Hours.Dock = System.Windows.Forms.DockStyle.Left;
            this.Hours.Image = ((System.Drawing.Image)(resources.GetObject("Hours.Image")));
            this.Hours.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Hours.Location = new System.Drawing.Point(0, 0);
            this.Hours.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Hours.Name = "Hours";
            this.Hours.Size = new System.Drawing.Size(78, 86);
            this.Hours.TabIndex = 0;
            this.Hours.Text = "Предмети";
            this.Hours.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Hours.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Hours.UseVisualStyleBackColor = false;
            this.Hours.Click += new System.EventHandler(this.Hours_Click);
            // 
            // Classes
            // 
            this.Classes.BackColor = System.Drawing.Color.White;
            this.Classes.Dock = System.Windows.Forms.DockStyle.Left;
            this.Classes.Image = ((System.Drawing.Image)(resources.GetObject("Classes.Image")));
            this.Classes.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Classes.Location = new System.Drawing.Point(78, 0);
            this.Classes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Classes.Name = "Classes";
            this.Classes.Size = new System.Drawing.Size(78, 86);
            this.Classes.TabIndex = 1;
            this.Classes.Text = "Класове";
            this.Classes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Classes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Classes.UseVisualStyleBackColor = false;
            this.Classes.Click += new System.EventHandler(this.Classes_Click);
            // 
            // Teachers
            // 
            this.Teachers.Dock = System.Windows.Forms.DockStyle.Left;
            this.Teachers.Image = ((System.Drawing.Image)(resources.GetObject("Teachers.Image")));
            this.Teachers.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Teachers.Location = new System.Drawing.Point(156, 0);
            this.Teachers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Teachers.Name = "Teachers";
            this.Teachers.Size = new System.Drawing.Size(78, 86);
            this.Teachers.TabIndex = 2;
            this.Teachers.Text = "Учители";
            this.Teachers.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Teachers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Teachers.UseVisualStyleBackColor = true;
            this.Teachers.Click += new System.EventHandler(this.Teachers_Click);
            // 
            // Menu
            // 
            this.Menu.Controls.Add(this.cabinetsView);
            this.Menu.Controls.Add(this.classView);
            this.Menu.Controls.Add(this.teacherView);
            this.Menu.Controls.Add(this.viewAll);
            this.Menu.Controls.Add(this.panel1);
            this.Menu.Controls.Add(this.save);
            this.Menu.Controls.Add(this.ExcelClassrooms);
            this.Menu.Controls.Add(this.TeacersExcel);
            this.Menu.Controls.Add(this.ExcelExport);
            this.Menu.Controls.Add(this.generate);
            this.Menu.Controls.Add(this.design);
            this.Menu.Controls.Add(this.Classrooms);
            this.Menu.Controls.Add(this.Teachers);
            this.Menu.Controls.Add(this.Classes);
            this.Menu.Controls.Add(this.Hours);
            this.Menu.Dock = System.Windows.Forms.DockStyle.Top;
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(1117, 86);
            this.Menu.TabIndex = 0;
            // 
            // cabinetsView
            // 
            this.cabinetsView.BackColor = System.Drawing.Color.White;
            this.cabinetsView.Dock = System.Windows.Forms.DockStyle.Left;
            this.cabinetsView.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cabinetsView.Image = ((System.Drawing.Image)(resources.GetObject("cabinetsView.Image")));
            this.cabinetsView.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cabinetsView.Location = new System.Drawing.Point(1025, 0);
            this.cabinetsView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cabinetsView.Name = "cabinetsView";
            this.cabinetsView.Size = new System.Drawing.Size(78, 86);
            this.cabinetsView.TabIndex = 48;
            this.cabinetsView.Text = "Виж по кабинети";
            this.cabinetsView.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cabinetsView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cabinetsView.UseVisualStyleBackColor = false;
            this.cabinetsView.Click += new System.EventHandler(this.cabinetsView_Click);
            // 
            // classView
            // 
            this.classView.Dock = System.Windows.Forms.DockStyle.Left;
            this.classView.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.classView.Image = ((System.Drawing.Image)(resources.GetObject("classView.Image")));
            this.classView.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.classView.Location = new System.Drawing.Point(947, 0);
            this.classView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.classView.Name = "classView";
            this.classView.Size = new System.Drawing.Size(78, 86);
            this.classView.TabIndex = 47;
            this.classView.Text = "Виж по класове";
            this.classView.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.classView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.classView.UseVisualStyleBackColor = true;
            this.classView.Click += new System.EventHandler(this.classView_Click);
            // 
            // teacherView
            // 
            this.teacherView.Dock = System.Windows.Forms.DockStyle.Left;
            this.teacherView.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.teacherView.Image = ((System.Drawing.Image)(resources.GetObject("teacherView.Image")));
            this.teacherView.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.teacherView.Location = new System.Drawing.Point(869, 0);
            this.teacherView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.teacherView.Name = "teacherView";
            this.teacherView.Size = new System.Drawing.Size(78, 86);
            this.teacherView.TabIndex = 46;
            this.teacherView.Text = "Виж като учител";
            this.teacherView.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.teacherView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.teacherView.UseVisualStyleBackColor = true;
            this.teacherView.Click += new System.EventHandler(this.teacherView_Click);
            // 
            // viewAll
            // 
            this.viewAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.viewAll.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.viewAll.Image = ((System.Drawing.Image)(resources.GetObject("viewAll.Image")));
            this.viewAll.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.viewAll.Location = new System.Drawing.Point(791, 0);
            this.viewAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.viewAll.Name = "viewAll";
            this.viewAll.Size = new System.Drawing.Size(78, 86);
            this.viewAll.TabIndex = 45;
            this.viewAll.Text = "Виж всички";
            this.viewAll.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.viewAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.viewAll.UseVisualStyleBackColor = true;
            this.viewAll.Click += new System.EventHandler(this.viewAll_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.line);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(780, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(11, 86);
            this.panel1.TabIndex = 44;
            // 
            // line
            // 
            this.line.BackColor = System.Drawing.Color.DimGray;
            this.line.Location = new System.Drawing.Point(6, 2);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(1, 63);
            this.line.TabIndex = 8;
            // 
            // save
            // 
            this.save.Dock = System.Windows.Forms.DockStyle.Left;
            this.save.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.save.Image = ((System.Drawing.Image)(resources.GetObject("save.Image")));
            this.save.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.save.Location = new System.Drawing.Point(702, 0);
            this.save.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(78, 86);
            this.save.TabIndex = 43;
            this.save.Text = "Запази";
            this.save.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // ExcelClassrooms
            // 
            this.ExcelClassrooms.Dock = System.Windows.Forms.DockStyle.Left;
            this.ExcelClassrooms.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ExcelClassrooms.Image = ((System.Drawing.Image)(resources.GetObject("ExcelClassrooms.Image")));
            this.ExcelClassrooms.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ExcelClassrooms.Location = new System.Drawing.Point(624, 0);
            this.ExcelClassrooms.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ExcelClassrooms.Name = "ExcelClassrooms";
            this.ExcelClassrooms.Size = new System.Drawing.Size(78, 86);
            this.ExcelClassrooms.TabIndex = 37;
            this.ExcelClassrooms.Text = "Експорт Кабинети";
            this.ExcelClassrooms.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ExcelClassrooms.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ExcelClassrooms.UseVisualStyleBackColor = true;
            this.ExcelClassrooms.Click += new System.EventHandler(this.ExcelClassrooms_Click);
            // 
            // TeacersExcel
            // 
            this.TeacersExcel.Dock = System.Windows.Forms.DockStyle.Left;
            this.TeacersExcel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TeacersExcel.Image = ((System.Drawing.Image)(resources.GetObject("TeacersExcel.Image")));
            this.TeacersExcel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.TeacersExcel.Location = new System.Drawing.Point(546, 0);
            this.TeacersExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TeacersExcel.Name = "TeacersExcel";
            this.TeacersExcel.Size = new System.Drawing.Size(78, 86);
            this.TeacersExcel.TabIndex = 31;
            this.TeacersExcel.Text = "Експорт Учители";
            this.TeacersExcel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TeacersExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TeacersExcel.UseVisualStyleBackColor = true;
            this.TeacersExcel.Click += new System.EventHandler(this.TeachersExcel_Click);
            // 
            // ExcelExport
            // 
            this.ExcelExport.Dock = System.Windows.Forms.DockStyle.Left;
            this.ExcelExport.Image = ((System.Drawing.Image)(resources.GetObject("ExcelExport.Image")));
            this.ExcelExport.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ExcelExport.Location = new System.Drawing.Point(468, 0);
            this.ExcelExport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ExcelExport.Name = "ExcelExport";
            this.ExcelExport.Size = new System.Drawing.Size(78, 86);
            this.ExcelExport.TabIndex = 30;
            this.ExcelExport.Text = "Експорт";
            this.ExcelExport.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ExcelExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ExcelExport.UseVisualStyleBackColor = true;
            this.ExcelExport.Click += new System.EventHandler(this.ExcelExport_Click);
            // 
            // generate
            // 
            this.generate.Dock = System.Windows.Forms.DockStyle.Left;
            this.generate.Image = ((System.Drawing.Image)(resources.GetObject("generate.Image")));
            this.generate.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.generate.Location = new System.Drawing.Point(390, 0);
            this.generate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.generate.Name = "generate";
            this.generate.Size = new System.Drawing.Size(78, 86);
            this.generate.TabIndex = 29;
            this.generate.Text = "Генерирай";
            this.generate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.generate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.generate.UseVisualStyleBackColor = true;
            this.generate.Click += new System.EventHandler(this.generate_Click);
            // 
            // design
            // 
            this.design.Dock = System.Windows.Forms.DockStyle.Left;
            this.design.Image = ((System.Drawing.Image)(resources.GetObject("design.Image")));
            this.design.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.design.Location = new System.Drawing.Point(312, 0);
            this.design.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.design.Name = "design";
            this.design.Size = new System.Drawing.Size(78, 86);
            this.design.TabIndex = 28;
            this.design.Text = "Дизайн";
            this.design.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.design.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.design.UseVisualStyleBackColor = true;
            this.design.Click += new System.EventHandler(this.design_Click);
            // 
            // Classrooms
            // 
            this.Classrooms.Dock = System.Windows.Forms.DockStyle.Left;
            this.Classrooms.Image = ((System.Drawing.Image)(resources.GetObject("Classrooms.Image")));
            this.Classrooms.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Classrooms.Location = new System.Drawing.Point(234, 0);
            this.Classrooms.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Classrooms.Name = "Classrooms";
            this.Classrooms.Size = new System.Drawing.Size(78, 86);
            this.Classrooms.TabIndex = 0;
            this.Classrooms.Text = "Кабинети";
            this.Classrooms.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Classrooms.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Classrooms.UseVisualStyleBackColor = true;
            this.Classrooms.Click += new System.EventHandler(this.classrooms_Click);
            // 
            // TimeTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 548);
            this.Controls.Add(this.scroll);
            this.Controls.Add(this.Menu);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "TimeTableForm";
            this.Text = "Автоматичен генератор на седмично разписание";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TimeTableForm_FormClosing);
            this.Load += new System.EventHandler(this.TimeTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timeTableBindingSource)).EndInit();
            this.scroll.ResumeLayout(false);
            this.Content.ResumeLayout(false);
            this.Menu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource timeTableBindingSource;
        private System.Windows.Forms.Panel scroll;
        private System.Windows.Forms.Panel Content;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel TimeTableField;
        private System.Windows.Forms.Button Hours;
        private System.Windows.Forms.Button Classes;
        private System.Windows.Forms.Button Teachers;
        private System.Windows.Forms.Panel Menu;
        private System.Windows.Forms.Button TeacersExcel;
        private System.Windows.Forms.Button ExcelExport;
        private System.Windows.Forms.Button generate;
        private System.Windows.Forms.Button design;
        private System.Windows.Forms.Button Classrooms;
        private System.Windows.Forms.ToolTip subjectInfoHover;
        private System.Windows.Forms.Button ExcelClassrooms;
        private System.Windows.Forms.Button cabinetsView;
        private System.Windows.Forms.Button classView;
        private System.Windows.Forms.Button teacherView;
        private System.Windows.Forms.Button viewAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label line;
        private System.Windows.Forms.Button save;
    }
}