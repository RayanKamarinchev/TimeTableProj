using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Database.Models;

namespace TimeTableProj
{
    public partial class NewClass : Form
    {
        Controller.Controller controller = Controller.Controller.Instance();
        public NewClass()
        {
            InitializeComponent();
        }

        private void NewClass_Load(object sender, EventArgs e)
        {
            var hours = controller.Subjects;
            foreach (var hour in hours)
            {
                HoursView.Rows.Add(hour);
            }
        }

        private void AddClass_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> hours = new Dictionary<string, int>();
            for (int i = 0; i < HoursView.RowCount; i++)
            {
                var row = HoursView.Rows[i];
                if (row.Cells[1].Value is null)
                {
                    hours.Add(row.Cells[0].Value.ToString(), 0);
                }
                else
                {
                    int num = int.Parse(row.Cells[1].Value.ToString());
                    hours.Add(row.Cells[0].Value.ToString(), num);
                }
            }

            var smth = controller.GetSubjectByName(hours.Keys.First());
            var clas = new Class()
            {
                Name = name.Text
            };
            controller.AddClass(clas);
            foreach (var hour in hours)
            {
                controller.AddClassSubject(clas, controller.GetSubjectByName(hour.Key));
            }

            controller.Classes = controller.GetClasses();
            Close();
        }
    }
}
