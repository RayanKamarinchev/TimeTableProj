using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Model.DTOs;

namespace TimeTableProj
{
    public partial class Classes : Form
    {
        private Controller.Controller controller = Controller.Controller.Instance();

        public Classes()
        {
            InitializeComponent();
        }


        private void Form1_Activated(object sender, EventArgs e)
        {
            content.AutoSize = true;
            List<ClassDTO> classes = controller.Classes;
            content.Controls.Clear();

            for (int i = 0; i < classes.Count; i++)
            {
                classes[i] = new ClassDTO(classes[i].Name, classes[i].CountOfSubjectsForWeek, classes[i].Teachers);
                ClassDTO clas = classes[i];
                SplitContainer sp = new SplitContainer();
                sp.Name = "panel";
                sp.Orientation = Orientation.Horizontal;
                sp.Dock = DockStyle.Top;
                sp.Width = content.Width;
                sp.AutoSize = true;
                sp.Panel1.Name = "panel" + 1;
                sp.Panel2Collapsed = true;
                sp.Panel1.MaximumSize = new Size(10000, 40);
                sp.Panel1MinSize = 50;
                sp.SplitterDistance = 50;
                sp.Panel1.BackColor = SystemColors.ControlLight;
                sp.Panel2.AutoSize = true;
                sp.Panel2MinSize = 0;

                PictureBox classIcon = new PictureBox();
                classIcon.Image = Image.FromFile(controller.images + "icons8-staff-48.png");
                classIcon.Location = new Point(72, 12);
                classIcon.Size = new Size(20, 20);
                classIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                sp.Panel1.Controls.Add(classIcon);

                Label className = new Label();
                className.Text = $"Клас: {clas.Name}";
                className.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                className.TextAlign = ContentAlignment.MiddleCenter;
                className.Location = new Point(98, 9);
                className.AutoSize = true;
                sp.Panel1.Controls.Add(className);


                PictureBox hoursIcon = new PictureBox();
                hoursIcon.Image = Image.FromFile(controller.images + "icons8-alarm-clock-48.png");
                hoursIcon.Location = new Point(243, 12);
                hoursIcon.Size = new Size(20, 20);
                hoursIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                sp.Panel1.Controls.Add(hoursIcon);

                Label hoursLabel = new Label();
                hoursLabel.Text = $"Часове в седмицата: {clas.Subjects}";
                hoursLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                hoursLabel.Location = new Point(278, 9);
                hoursLabel.TextAlign = ContentAlignment.MiddleCenter;
                hoursLabel.AutoSize = true;
                sp.Panel1.Controls.Add(new Label()
                                           { Height = 1, Dock = DockStyle.Bottom, BackColor = Color.Black });
                sp.Panel1.Controls.Add(hoursLabel);


                for (int j = 0; j < clas.CountOfSubjectsForWeek.Count; j++)
                {
                    Panel pan = new Panel();
                    pan.Location = new Point(70, 50 * j);
                    pan.Size = new Size(600, 40);
                    string lessonName = clas.CountOfSubjectsForWeek.ElementAt(j).Key;
                    int timesPerWeek = clas.CountOfSubjectsForWeek.ElementAt(j).Value;

                    PictureBox lessonIcon = new PictureBox();
                    lessonIcon.Image = Image.FromFile(controller.images + "icons8-study-48.png");
                    lessonIcon.Location = new Point(18, 8);
                    lessonIcon.Size = new Size(20, 20);
                    lessonIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                    pan.Controls.Add(lessonIcon);

                    Label lessonsPerWeek = new Label();
                    lessonsPerWeek.Text = $"Час: {lessonName}, {(timesPerWeek>1? timesPerWeek + " пъти" : "1 път")} на седмицата: ";
                    lessonsPerWeek.Font = new Font("Segoe UI", 12);
                    lessonsPerWeek.TextAlign = ContentAlignment.MiddleCenter;
                    lessonsPerWeek.Location = new Point(44, 3);
                    lessonsPerWeek.AutoSize = true;
                    pan.Controls.Add(lessonsPerWeek);


                    PictureBox teacherIcon = new PictureBox();
                    teacherIcon.Image = Image.FromFile(controller.images + "icons8-teacher-64.png");
                    teacherIcon.Location = new Point(350, 8);
                    teacherIcon.Size = new Size(20, 20);
                    teacherIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                    pan.Controls.Add(teacherIcon);

                    Label teacherLabel = new Label();
                    teacherLabel.Text = clas.Teachers.Any(t=>t.Subject==lessonName)
                        ? $"Учител: {clas.Teachers.FirstOrDefault(t => t.Subject==lessonName).Name}"
                        : $"Учител: Няма";
                    teacherLabel.Font = new Font("Segoe UI", 12);
                    teacherLabel.Location = new Point(375, 3);
                    teacherLabel.TextAlign = ContentAlignment.MiddleCenter;
                    teacherLabel.AutoSize = true;
                    pan.Controls.Add(teacherLabel);

                    pan.Controls.Add(new Label()
                                         { Height = 1, Dock = DockStyle.Bottom, BackColor = Color.Black });

                    sp.Panel2.Controls.Add(pan);
                }
                void MyButtonHandler(object sender, EventArgs e)
                {
                    int height = sp.Panel1.Height;
                    sp.Panel2Collapsed = !sp.Panel2Collapsed;
                    sp.Height = height;
                }
                EventHandler click = new EventHandler(MyButtonHandler);
                sp.Panel1.Click += click;
                for (int j = 0; j < sp.Panel1.Controls.Count; j++)
                {
                    sp.Panel1.Controls[j].Click += click;
                }
                content.Controls.Add(sp);
            }

            content.Padding = new Padding(0, 0, 0, 100);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void addClass_Click(object sender, EventArgs e)
        {
            NewClass form = new NewClass();
            form.Show();
        }
        private void content_Paint(object sender, PaintEventArgs e)
        {
            if (panel1.BorderStyle == BorderStyle.FixedSingle)
            {
                int thickness = 3; //it's up to you
                int halfThickness = thickness / 2;
                using (Pen p = new Pen(Color.Black, thickness))
                {
                    e.Graphics.DrawRectangle(p, new Rectangle(halfThickness,
                                                              halfThickness,
                                                              panel1.ClientSize.Width - thickness,
                                                              panel1.ClientSize.Height - thickness));
                }
            }
        }
    }
}