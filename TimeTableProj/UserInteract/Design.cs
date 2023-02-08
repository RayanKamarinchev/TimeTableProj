using Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TimeTableProj
{
    public partial class Design : Form
    {
        private Controller.Controller controller = Controller.Controller.Instance();
        public Design()
        {
            InitializeComponent();
        }


        private void Design_Load(object sender, EventArgs e)
        {
            var subjects = controller.GetSubjectsColors();
            void PickColor(object sender, EventArgs e)
            {
                colorDialog1.CustomColors = new int[]
                {
                    1657832, 13797909, 15418714, 12297747, 8950678, 16574930, 14570432, 6918584, 5264095, 6481328, 10905188, 10352729, 5689851
                };
                colorDialog1.ShowDialog();
                Button btn = (Button)sender;
                btn.BackColor = colorDialog1.Color;
                controller.ChangeSubjectColor(btn.Name.Substring(3, btn.Name.Length-3), colorDialog1.Color.ToArgb());
            }

            for (int i = 0; i < subjects.Count; i++)
            {
                Label l = new Label();
                l.Location = new Point(30, i * 30);
                l.Text = subjects[i][0];
                Controls.Add(l);
                Button btn = new Button();
                btn.Name = "btn" + subjects[i][0];
                btn.Location = new Point(200, i * 30);
                btn.Size = new Size(60, 20);
                btn.BackColor = Color.FromArgb(int.Parse(subjects[i][1]));
                btn.Click += new EventHandler(PickColor);
                Controls.Add(btn);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Design_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.Subjects = controller.GetSubjectsInfo();
            TimeTableForm f = (TimeTableForm)Application.OpenForms[0];
            f.Refresh();
        }
    }
}
