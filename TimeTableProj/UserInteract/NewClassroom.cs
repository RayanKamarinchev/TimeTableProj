using Database.Models;
using System;
using System.Windows.Forms;

namespace TimeTableProj
{
    public partial class NewClassroom : Form
    {
        private Controller.Controller controller = Controller.Controller.Instance();
        public NewClassroom()
        {
            InitializeComponent();
        }

        private void NewClassroom_Load(object sender, EventArgs e)
        {
            cabinetNumber.Text = (controller.GetClassroomsCount()+1).ToString();
            subjectSelect.Items.Add("Няма");
            subjectSelect.SelectedIndex = 0;
            subjectSelect.Items.AddRange(new string[]{ "Обикновен", "Компютърен", "ПриродниНауки", "Биология", "Химия", "Физика", "НаОткрито" });
        }

        private void AddCabinet_Click(object sender, EventArgs e)
        {
            if (subjectSelect.SelectedItem.ToString() == "Няма")
            {
                controller.AddClassroomWithoutSubject(int.Parse(cabinetNumber.Text));
            }
            else
            {
                controller.AddClassroomWithSubject(int.Parse(cabinetNumber.Text), (SubjectType)Enum.Parse(typeof(SubjectType), subjectSelect.SelectedItem.ToString()));
            }

            controller.Classrooms = controller.GetClassrooms();
            Close();
        }
    }
}
