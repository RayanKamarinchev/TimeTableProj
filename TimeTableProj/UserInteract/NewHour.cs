using Database.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TimeTableProj
{
    public partial class NewHour : Form
    {
        private Controller.Controller controller = Controller.Controller.Instance();
        public NewHour()
        {
            InitializeComponent();
        }

        private void NewHour_Load(object sender, EventArgs e)
        {
            var hours = controller.GetSubjectsWithIds();
            foreach (var hour in hours)
            {
                var row = new DataGridViewRow();
                row.Cells.Add(new DataGridViewTextBoxCell()
                {
                    Value = hour.Name
                });
                row.Cells.Add(new DataGridViewComboBoxCell()
                {
                    Items = { "Обикновен", "Компютърен", "Биология", "Химия", "Физика", "НаОткрито"},
                    Value = hour.SubjectType.ToString()
                });
                row.Cells.Add(new DataGridViewTextBoxCell()
                {
                    Value = hour.Id
                });
                HourTable.Rows.Add(row);
            }
        }

        private void NewHour_FormClosing(object sender, FormClosingEventArgs e)
        {
            var subjects = controller.GetSubjectsWithIds();
            var ids = subjects.Select(s => s.Id).ToList();
            for (int i = 0; i < HourTable.RowCount-1; i++)
            {
                DataGridViewRow row = HourTable.Rows[i];
                if (row.Cells[2].Value is null)
                {
                    subjects.Add(new Subject()
                    {
                        Name = row.Cells[0].Value.ToString(),
                        SubjectType = (SubjectType)Enum.Parse(typeof(SubjectType), row.Cells[1].Value.ToString())
                    });
                }
                else
                {
                    int id = int.Parse(row.Cells[2].Value.ToString());
                    var subj = subjects.First(s => s.Id == id);
                    subj.Name = row.Cells[0].Value.ToString();
                    subj.SubjectType = (SubjectType)Enum.Parse(typeof(SubjectType), row.Cells[1].Value.ToString());
                    ids.Remove(id);
                }
            }

            var deleted = subjects.Where(e => ids.Contains(e.Id)).ToArray();
            controller.UpdateSubjects(subjects, deleted);
            controller.Subjects = controller.GetSubjectsInfo();
        }
    }
}
