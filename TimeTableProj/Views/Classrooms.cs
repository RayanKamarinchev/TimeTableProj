using Database.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Model.DTOs;

namespace TimeTableProj
{
    public partial class classrooms : Form
    {
        private Controller.Controller controller = Controller.Controller.Instance();
        public classrooms()
        {
            InitializeComponent();
        }

        private void Visualize()
        {
            int count = sp.Controls.Count;
            for (int i = 0; i < count; i++)
            {
                sp.Controls.RemoveAt(0);
            }

            List<ClassroomDTO> rooms = controller.GetClassrooms();
            for (int i = 0; i < rooms.Count; i++)
            {
                Panel con = new Panel();
                con.Dock = DockStyle.Top;
                con.BackColor = SystemColors.ControlLight;
                con.Height = 50;
                ClassroomDTO classroom = rooms[i];

                Label className = new Label();
                className.Text = $"Номер: {classroom.Number}";
                className.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                className.TextAlign = ContentAlignment.MiddleCenter;
                className.Location = new Point(60, 9);
                className.AutoSize = true;
                con.Controls.Add(className);


                PictureBox hoursIcon = new PictureBox();
                hoursIcon.Image = Image.FromFile(controller.images + "icons8-spiral-bound-booklet-48.png");
                hoursIcon.Location = new Point(200, 12);
                hoursIcon.Size = new Size(25, 25);
                hoursIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                con.Controls.Add(hoursIcon);

                Label hoursLabel = new Label();
                hoursLabel.Text = $"Вид кабинет: {((SubjectType)classroom.SubjectType).ToString()}";
                hoursLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                hoursLabel.Location = new Point(230, 9);
                hoursLabel.TextAlign = ContentAlignment.MiddleLeft;
                hoursLabel.Size = new Size(350, 30);
                con.Controls.Add(hoursLabel);

                Panel btnPan = new Panel();
                btnPan.Dock = DockStyle.Right;
                btnPan.Width = 150;

                PictureBox edit = new PictureBox();
                edit.Image = Image.FromFile(controller.images + "icons8-edit-48.png");
                edit.SizeMode = PictureBoxSizeMode.StretchImage;
                edit.Location = new Point(50, 5);
                edit.Size = new Size(40, 40);
                edit.Cursor = Cursors.Hand;

                PictureBox del = new PictureBox();
                del.Image = Image.FromFile(controller.images + "icons8-remove-48.png");
                del.SizeMode = PictureBoxSizeMode.StretchImage;
                del.Location = new Point(100, 5);
                del.Size = new Size(40, 40);
                del.Cursor = Cursors.Hand;

                void Delete(object sender, EventArgs e)
                {
                    sp.Controls.Remove(con);
                    controller.RemoveClassroomByNumber(classroom.Number);
                    DialogResult dialogResult = MessageBox.Show("Искаш ли да оправиш номерацията на кабинетите", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        controller.FixClassroomsNumericalOrder();
                        Visualize();
                    }
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

        private void Classes_Activated(object sender, EventArgs e)
        {
            Visualize();
        }

        private void addClassrooom_Click(object sender, EventArgs e)
        {
            NewClassroom f = new NewClassroom();
            f.Show();
        }
        private void numberOrderBtn_Click(object sender, EventArgs e)
        {
            controller.FixClassroomsNumericalOrder();
            Visualize();
        }
    }
}
