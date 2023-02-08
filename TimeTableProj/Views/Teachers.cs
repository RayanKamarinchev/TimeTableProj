using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Model.DTOs;

namespace TimeTableProj
{
    public partial class Teachers : Form
    {
        Controller.Controller controller = Controller.Controller.Instance();
        public Teachers()
        {
            InitializeComponent();
        }

        private void NewTeacher_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewTeacher form = new NewTeacher();
            form.Show();
        }
        private void sp_Paint(object sender, PaintEventArgs e)
        {
            if (sp.BorderStyle == BorderStyle.FixedSingle)
            {
                int thickness = 3; //it's up to you
                int halfThickness = thickness / 2;
                using (Pen p = new Pen(Color.Black, thickness))
                {
                    e.Graphics.DrawRectangle(p, new Rectangle(halfThickness,
                                                              halfThickness,
                                                              sp.ClientSize.Width - thickness,
                                                              sp.ClientSize.Height - thickness));
                }
            }
        }

        private void Teachers_Activated(object sender, EventArgs e)
        {
            DisplayTeachers();
        }

        private void DisplayTeachers()
        {
            int count = sp.Controls.Count;
            for (int i = 0; i < count; i++)
            {
                sp.Controls.RemoveAt(0);
            }

            List<TeacherDTO> teachers = controller.GetTeachers();
            for (int i = 0; i < teachers.Count; i++)
            {
                Panel con = new Panel();
                con.Dock = DockStyle.Top;
                con.BackColor = SystemColors.ControlLight;
                con.Height = 50;
                TeacherDTO teacher = teachers[i];

                PictureBox classIcon = new PictureBox();
                classIcon.Image = Image.FromFile(controller.images + "icons8-teacher-64.png");
                classIcon.Location = new Point(30, 12);
                classIcon.Size = new Size(25, 25);
                classIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                con.Controls.Add(classIcon);

                Label className = new Label();
                className.Text = $"Име: {teacher.Name}";
                className.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                className.TextAlign = ContentAlignment.MiddleCenter;
                className.Location = new Point(60, 9);
                className.AutoSize = true;
                con.Controls.Add(className);


                PictureBox hoursIcon = new PictureBox();
                hoursIcon.Image = Image.FromFile(controller.images + "icons8-class-48.png");
                hoursIcon.Location = new Point(500, 12);
                hoursIcon.Size = new Size(25, 25);
                hoursIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                con.Controls.Add(hoursIcon);

                Label hoursLabel = new Label();
                hoursLabel.Text = $"Класове: {String.Join(", ", teacher.classes.Select(c => c.Name))}";
                hoursLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                hoursLabel.Location = new Point(530, 9);
                hoursLabel.TextAlign = ContentAlignment.MiddleLeft;
                hoursLabel.Size = new Size(250, 30);
                con.Controls.Add(hoursLabel);

                PictureBox subjectIcon = new PictureBox();
                subjectIcon.Image = Image.FromFile(controller.images + "icons8-spiral-bound-booklet-48.png");
                subjectIcon.Location = new Point(300, 12);
                subjectIcon.Size = new Size(25, 25);
                subjectIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                con.Controls.Add(subjectIcon);

                Label subjectLabel = new Label();
                subjectLabel.Text = $"Предмет: {teacher.Subject}";
                subjectLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                subjectLabel.Location = new Point(330, 9);
                subjectLabel.TextAlign = ContentAlignment.MiddleLeft;
                subjectLabel.Size = new Size(200, 30);
                con.Controls.Add(subjectLabel);

                Panel btnPan = new Panel();
                btnPan.Dock = DockStyle.Right;
                btnPan.Width = 150;

                PictureBox edit = new PictureBox();
                edit.Image = Image.FromFile(controller.images + "icons8-edit-48.png");
                edit.SizeMode = PictureBoxSizeMode.StretchImage;
                edit.Location = new Point(50, 5);
                edit.Size = new Size(40, 40);
                edit.Cursor = System.Windows.Forms.Cursors.Hand;

                void Edit(object sender, EventArgs e)
                {
                    NewTeacher f = new NewTeacher();
                    sp.Controls.Remove(con);
                    f.Show();
                    f.teacherName = teacher.Name;
                    for (int k = 0; k < f.Controls.Count; k++)
                    {
                        var element = f.Controls[k];
                        if (element.Name == "subject")
                        {
                            element.Text = teacher.Subject;
                        }
                        else if (element.Name == "ClassChoice")
                        {
                            try
                            {
                                var checkList = (CheckedListBox)element;
                                for (int j = 0; j < checkList.Items.Count; j++)
                                {
                                    if (teacher.classes.Select(c => c.Name).Contains(checkList.Items[j]))
                                    {
                                        checkList.SetItemChecked(j, true);
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show(exception.Message);
                            }
                        }
                        else if (element.Name == "AddClass")
                        {
                            f.Controls.Remove(element);
                        }
                        else if (element.Name == "busyDaysPanel")
                        {
                            foreach (Control el in element.Controls)
                            {
                                string btnId = el.Name.Replace("btn", "");
                                if (btnId == el.Name)
                                {
                                    continue;
                                }
                                if (teacher.BusyDays.Contains(int.Parse(btnId)))
                                {
                                    el.BackColor = Color.PaleVioletRed;
                                    f.busyDays = teacher.BusyDays;
                                }
                            }
                        }
                    }

                    f.Controls[6].Text = teacher.Name;
                }
                edit.Click += new EventHandler(Edit);

                PictureBox del = new PictureBox();
                del.Image = Image.FromFile(controller.images + "icons8-remove-48.png");
                del.SizeMode = PictureBoxSizeMode.StretchImage;
                del.Location = new Point(100, 5);
                del.Size = new Size(40, 40);
                del.Cursor = Cursors.Hand;

                void Delete(object sender, EventArgs e)
                {
                    sp.Controls.Remove(con);
                    controller.RemoveTeacherByName(teacher.Name);
                }
                del.Click += new EventHandler(Delete);
                btnPan.Controls.Add(del);
                btnPan.Controls.Add(edit);
                con.Controls.Add(btnPan);
                con.Controls.Add(new Label()
                { Height = 1, Dock = DockStyle.Bottom, BackColor = Color.Black });
                sp.Controls.Add(con);
            }
        }
    }
}
