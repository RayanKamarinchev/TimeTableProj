using Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TimeTableProj
{
    public partial class NewTeacher : Form
    {
        private Controller.Controller controller = Controller.Controller.Instance();
        public List<int> busyDays = new List<int>();
        public string teacherName = "";
        public NewTeacher()
        {
            InitializeComponent();
        }

        private void NewTeacher_Load(object sender, EventArgs e)
        {
            ClassChoice.Items.AddRange(controller.Classes.Select(c=>c.Name).ToArray());

            subject.Items.AddRange(controller.GetSubjectNames());
            busyDaysPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
        }

        private void AddClass_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NewTeacher_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<string> classes = ClassChoice.CheckedItems.Cast<string>().ToList();
            if (subject.Text == "" || name.Text == "")
            {
                MessageBox.Show("Невалидно име или предмет");
                return;
            }

            if (teacherName=="")
            {
                Teacher teacher = new Teacher();
                teacher.Name = name.Text;
                teacher.Subject = controller.GetSubjectByName(subject.Text);
                teacher.ClassTeachers = classes.Select(c => new ClassTeacher()
                {
                    Class = controller.GetClassByName(c)
                }).ToList();
                teacher.TeacherBusies = busyDays.Select(b => new TeacherBusy()
                {
                    Day = b
                }).ToList();
                controller.AddTeacher(teacher);
            }
            else
            {
                Teacher teacher = controller.GetTeacherByName(teacherName);
                teacher.Name = name.Text;
                teacher.Subject = controller.GetSubjectByName(subject.Text);
                teacher.ClassTeachers = classes.Select(c => new ClassTeacher()
                {
                    Class = controller.GetClassByName(c)
                }).ToList();
                teacher.TeacherBusies = busyDays.Select(b => new TeacherBusy()
                {
                    Day = b
                }).ToList();
                controller.UpdateTeacher(teacher);
            }

            controller.Teachers = controller.GetTeachers();
            Application.OpenForms["Teachers"].Close();
            Form f = new Teachers();
            f.Show();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int val = int.Parse(btn.Name.Replace("btn", ""));
            if (btn.BackColor == Color.PaleGreen)
            {
                btn.BackColor =  Color.PaleVioletRed;
                busyDays.Add(val);
            }
            else
            {
                btn.BackColor = Color.PaleGreen;
                busyDays.Remove(val);
            }
        }
    }
}
