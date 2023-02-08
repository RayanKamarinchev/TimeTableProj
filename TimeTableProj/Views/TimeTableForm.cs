using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Model.DTOs;
using Application = System.Windows.Forms.Application;
using Label = System.Windows.Forms.Label;
using Point = System.Drawing.Point;
using Range = Microsoft.Office.Interop.Excel.Range;
using Database.Models;
using System.Runtime.InteropServices;

namespace TimeTableProj
{
    //Different types of information showed
    enum View
    {
        All, Teacher, Class, Classroom
    }
    public partial class TimeTableForm : Form
    {
        //Variables
        private View view = View.All;
        private Controller.Controller controller = Controller.Controller.Instance();
        private List<TimeTable> data; 
        private static int dayOfWeekLabelWidth = 350;
        private static int dayOfWeekLabelCompactWidth = 140;
        private int leftMargin = 53;
        private static Panel subjectsDisplayMenu;
        private static Panel compactViewsSubjectsDisplayMenu;
        private List<ClassroomDTO>[,] ClassroomsList = new List<ClassroomDTO>[5,7];
        private ToolTip toolTip;
        private bool isSaved = true;
        private string downloadsPath = SHGetKnownFolderPath(new Guid("374DE290-123F-4565-9164-39C4925E467B"), 0);
        public TimeTableForm()
        {
            InitializeComponent();
        }

        [DllImport("shell32",
                   CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
        private static extern string SHGetKnownFolderPath(
            [MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags,
            int hToken = 0);

        private void TimeTable_Load(object sender, EventArgs e)
        {
            TimeTableField.HorizontalScroll.Maximum = 0;
            TimeTableField.AutoScroll = false;
            TimeTableField.HorizontalScroll.Visible = false;
            TimeTableField.AutoScroll = true;
            toolTip = subjectInfoHover;
            this.HorizontalScroll.Maximum = 10000;
            this.VerticalScroll.Maximum = 10000;
            DisplayOutline();
            try
            {
                var tuple = controller.GetSavedTable();
                data = tuple.Item1;
                ClassroomsList = tuple.Item2;
            }
            catch (Exception exception)
            {

            }
            DisplayTimetableSubjects();
        }

        //Click Events

        private void Classes_Click(object sender, EventArgs e)
        {
            Classes form = new Classes();
            form.Show();
        }

        private void Hours_Click(object sender, EventArgs e)
        {
            NewHour form = new NewHour();
            form.Show();
        }

        private void Teachers_Click(object sender, EventArgs e)
        {
            Teachers form = new Teachers();
            form.Show();
        }
        private void generate_Click(object sender, EventArgs e)
        {
            subjectsDisplayMenu.Controls.Clear();
            Cursor.Current = Cursors.WaitCursor;
            var tuple = controller.GenerateTable(controller.Classes, controller.Teachers, controller.Classrooms, controller.Subjects);
            data = tuple.Item1;
            ClassroomsList = tuple.Item2;
            if (view == View.All)
            {
                DisplayTimetableSubjects();
            }

            isSaved = false;
        }

        public void Refresh()
        {
            subjectsDisplayMenu.Controls.Clear();
            if (view == View.All)
            {
                DisplayTimetableSubjects();
            }
        }
        private void design_Click(object sender, EventArgs e)
        {
            Design form = new Design();
            form.Show();
        }
        private void teacherView_Click(object sender, EventArgs e)
        {
            if (view!=View.Teacher)
            {
                TimeTableField.Controls.Clear();
                MakeTeacherViewable();
                view = View.Teacher;
            }
        }
        private void viewAll_Click(object sender, EventArgs e)
        {
            if (view != View.All)
            {
                TimeTableField.Controls.Clear();
                DisplayOutline();
                DisplayTimetableSubjects();
                view = View.All;
            }
        }

        private void classView_Click(object sender, EventArgs e)
        {
            if (view != View.Class)
            {
                TimeTableField.Controls.Clear();
                MakeClassViewable();
                view = View.Class;
            }
        }
        private void classrooms_Click(object sender, EventArgs e)
        {
            classrooms f = new classrooms();
            f.Show();
        }

        private void cabinetsView_Click(object sender, EventArgs e)
        {
            if (view != View.Classroom)
            {
                TimeTableField.Controls.Clear();
                MakeCabinetViewable();
                view = View.Classroom;
            }
        }
        private void TimeTableField_Scroll(object sender, ScrollEventArgs se)
        {
            if (se.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                this.VerticalScroll.Value = se.NewValue;
            }
            else if (se.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                this.HorizontalScroll.Value = se.NewValue;
            }
            this.Invalidate();
            base.OnScroll(se);
        }


        //Main view

        //displays all elements needed for the main view such as week day labels and class names and 
        private void DisplayOutline()
        {

            subjectsDisplayMenu= new Panel()
            {
                AutoScroll = true,
                Location = new Point(0, 61),
                //Dock = DockStyle.Fill
                Width = dayOfWeekLabelWidth * 5+this.leftMargin+18,
                Height = TimeTableField.Height-80
            };
            Panel offset=new Panel()
            {
                Location = new Point(0,0),
                Width = this.leftMargin,
                Height = 30
            };
            TimeTableField.Controls.Add(offset);
            Label mon = new Label()
            {
                BackColor = Color.FromArgb(56, 166, 40),
                Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 30,
                Width = dayOfWeekLabelWidth - 1
            }; ;
            mon.Text = "Понеделник";
            mon.Location = new Point(offset.Width,0);
            Label tue = new Label()
            {
                BackColor = Color.FromArgb(56, 166, 40),
                Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 30,
                Width = dayOfWeekLabelWidth - 1
            }; ;
            tue.Text = "Вторник";
            tue.Location = new Point((dayOfWeekLabelWidth) + offset.Width,0);
            Label wed = new Label()
            {
                BackColor = Color.FromArgb(56, 166, 40),
                Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 30,
                Width = dayOfWeekLabelWidth - 1
            }; ;
            wed.Location = new Point((dayOfWeekLabelWidth) * 2 + offset.Width,0);
            wed.Text = "Сряда";
            Label thu = new Label()
            {
                BackColor = Color.FromArgb(56, 166, 40),
                Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 30,
                Width = dayOfWeekLabelWidth - 1
            }; ;
            thu.Location = new Point((dayOfWeekLabelWidth) * 3 + offset.Width,0);
            thu.Text = "Четвъртък";
            Label fri = new Label()
            {
                BackColor = Color.FromArgb(56, 166, 40),
                Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 30,
                Width = dayOfWeekLabelWidth - 1
            }; ;
            fri.Text = "Петък";
            fri.Location = new Point((dayOfWeekLabelWidth)* 4 + offset.Width,0);

            TimeTableField.Controls.AddRange(new Control[]{mon,new Label()
            {
                BackColor = Color.Black,
                Height = 30,
                Width = 1,
                Location = new Point((dayOfWeekLabelWidth-1) + offset.Width, 0)
            },tue,new Label()
            {
                BackColor = Color.Black,
                Height = 30,
                Width = 1,
                Location = new Point(dayOfWeekLabelWidth*2-1 + offset.Width, 0)
            },wed,new Label()
            {
                BackColor = Color.Black,
                Height = 30,
                Width = 1,
                Location = new Point(dayOfWeekLabelWidth*3-1 + offset.Width, 0)
            },thu,new Label()
            {
                BackColor = Color.Black,
                Height = 30,
                Width = 1,
                Location = new Point(dayOfWeekLabelWidth*4-1 + offset.Width, 0)
            },fri,new Label()
            {
                BackColor = Color.Black,
                Height = 30,
                Width = 1,
                Location = new Point(dayOfWeekLabelWidth*5-1 + offset.Width, 0)
            }, new Label()
            {
                BackColor = Color.FromKnownColor(KnownColor.Control),
                Height = 60,
                Width = 20,
                Location = new Point(dayOfWeekLabelWidth*5 + offset.Width, 0)
            }});
            int partSize = dayOfWeekLabelWidth / 7;
            Control[] labels = new Control[70];
            for (int i = 0; i < 70; i+=2)
            {
                labels[i] = new Label()
                {
                    Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Height = 30,
                    Width = partSize-1,
                    Location = new Point(i * partSize/2 + offset.Width-1, mon.Height),
                    Text = ((i/2)%7+1).ToString()
                };
                labels[i + 1] = new Label()
                {
                    BackColor = Color.Black,
                    Height = 30,
                    Width = 1,
                    Location = new Point((i/2+1) * partSize + offset.Width-1, mon.Height)
                };
            }
            TimeTableField.Controls.Add(new Label()
            {
                BackColor = Color.Black,
                Height = 60,
                Width = 1,
                Location = new Point(offset.Width - 1, 0)
            });
            TimeTableField.Controls.AddRange(labels);
            TimeTableField.Controls.Add(subjectsDisplayMenu);
            TimeTableField.Controls.Add(new Label()
            {
                Width = subjectsDisplayMenu.Width,
                Height = 1,
                BackColor = Color.Black,
                Location = new Point(0, 60),
            });
        }
        public void DisplayTimetableSubjects()
        {
            if (data is null)
            {
                return;
            }
            int i = 0;
            int startHeight = -3;
            int width = dayOfWeekLabelWidth;

            int classHeight = 49;
            bool lastHours = false;
            subjectsDisplayMenu.Controls.Clear();
            foreach (var timeTable in data)
            {
                Label c = new Label();
                c.AutoSize = false;
                c.Location = new Point(0, startHeight + i * classHeight);
                c.Height = classHeight;
                c.Width = leftMargin;
                c.TextAlign = ContentAlignment.MiddleCenter;
                c.Text = controller.Classes[i].Name;
                c.Controls.Add(new Label()
                { Height = 1, Dock = DockStyle.Top, BackColor = Color.Black });
                c.Controls.Add(new Label()
                { Width = 1, Dock = DockStyle.Right, BackColor = Color.Black });
                subjectsDisplayMenu.Controls.Add(c);
                int d = 0;
                foreach (var day in timeTable.days)
                {
                    int h = 0;
                    foreach (var hour in day.IndexedHours)
                    {
                        Label l = new Label();
                        l.AutoSize = false;
                        toolTip.SetToolTip(l, String.Join(", ",hour.Teacher.Select(t=>t.Name).ToList()));
                        l.Location = new Point(dayOfWeekLabelWidth * h / 7 + width * d + leftMargin, startHeight + classHeight * i + 1);
                        l.Height = classHeight;
                        l.Width = dayOfWeekLabelWidth / 7;
                        l.TextAlign = ContentAlignment.MiddleCenter;
                        l.Text = hour.Subject;
                        //If the subjects name is too long it cuts it
                        if (hour.Subject.Length > 4)
                        {
                            l.Text = hour.Subject.Substring(0, 4);
                        }
                        //Show color
                        var subj = controller.Subjects.FirstOrDefault(h => h.Subject == hour.Subject);
                        if (!(subj is null))
                        {
                            l.BackColor = Color.FromArgb(subj.ColorCode);
                        }
                        //Bottom line
                        l.Controls.Add(new Label()
                        { Height = 1, Dock = DockStyle.Bottom, BackColor = Color.Black });
                        //right line unless its 6th subject at Friday
                        if (h != 6 || d != 4)
                        {
                            l.Controls.Add(new Label()
                            { Width = 1, Dock = DockStyle.Right, BackColor = Color.Black });
                        }
                        //If there was no subject cell before, draw left line
                        if (0 == h && lastHours)
                        {
                            l.Controls.Add(new Label()
                            { Width = 1, Dock = DockStyle.Left, BackColor = Color.Black });
                            l.Location = new Point(l.Location.X - 1, l.Location.Y);
                            l.Width++;
                        }
                        //draw top line
                        if (i != 0 && h == 6)
                        {
                            if (data[i - 1].days[d].IndexedHours.Count != 7)
                            {
                                l.Controls.Add(new Label()
                                { Height = 1, Dock = DockStyle.Top, BackColor = Color.Black });
                                l.Height++;
                                l.Location = new Point(l.Location.X, l.Location.Y - 1);
                            }
                        }
                        subjectsDisplayMenu.Controls.Add(l);
                        h++;
                    }

                    lastHours = h == 6;
                    d++;
                }
                i++;
            }
        }


        //Views

        //Displays all elements needed for the compact views such as week day labels and subject numeration
        private ComboBox PlotSpecificViews(string name, string elementName, Action<object, EventArgs, ComboBox, int> updater)
        {
            int classHeight = 50;
            Label teacherLabel = new Label()
            {
                Text = $"{name}: ",
                Location = new Point(30, 30),
                Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Bold)
            };
            ComboBox teachersList = new ComboBox()
            {
                Name = $"{elementName}ListDropdown",
                Width = 170,
                Location = new Point(150, 30),
                Font = new System.Drawing.Font("Segoe UI", 12),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            teachersList.SelectionChangeCommitted += new EventHandler((s, e) => updater(s, e, teachersList, classHeight));

            compactViewsSubjectsDisplayMenu = new Panel()
            {
                AutoScroll = true,
                Location = new Point(30, 100),
                Width = dayOfWeekLabelCompactWidth * 5 + this.leftMargin,
                Height = TimeTableField.Height - 100
            };
            compactViewsSubjectsDisplayMenu.Controls.Add(new Label()
            {
                Width = dayOfWeekLabelCompactWidth * 5 + this.leftMargin,
                Height = 1,
                BackColor = Color.Black,
                Location = new Point(0, 30)
            });

            for (int i = 0; i < 7; i++)
            {
                Label c = new Label();
                c.AutoSize = false;
                c.Location = new Point(0, 30 + i * classHeight);
                c.Height = classHeight;
                c.Width = this.leftMargin;
                c.TextAlign = ContentAlignment.MiddleCenter;
                c.Text = (i + 1).ToString();
                c.Controls.Add(new Label()
                { Height = 1, Dock = DockStyle.Top, BackColor = Color.Black });
                c.Controls.Add(new Label()
                { Width = 1, Dock = DockStyle.Right, BackColor = Color.Black });
                c.Controls.Add(new Label()
                { Width = 1, Dock = DockStyle.Left, BackColor = Color.Black });
                if (i == 6)
                {
                    c.Controls.Add(new Label()
                    { Height = 1, Dock = DockStyle.Bottom, BackColor = Color.Black });
                }
                compactViewsSubjectsDisplayMenu.Controls.Add(c);
            }
            compactViewsSubjectsDisplayMenu.Controls.Add(new Label()
            { Height = 1, Dock = DockStyle.Top, BackColor = Color.Black });

            compactViewsSubjectsDisplayMenu.Controls.Add(new Label()
            { Width = 1, Height = 30, BackColor = Color.Black, Location = new Point(0, 0) });
            Panel offset = new Panel()
            {
                Location = new Point(0, 0),
                Width = this.leftMargin,
                Height = 30
            };
            Label mon = new Label()
            {
                BackColor = Color.FromArgb(121, 134, 223),
                Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 30,
                Width = dayOfWeekLabelCompactWidth - 1
            }; ;
            mon.Text = "Понеделник";
            mon.Location = new Point(offset.Width, 0);
            Label tue = new Label()
            {
                BackColor = Color.FromArgb(121, 134, 223),
                Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 30,
                Width = dayOfWeekLabelCompactWidth - 1
            }; ;
            tue.Text = "Вторник";
            tue.Location = new Point((dayOfWeekLabelCompactWidth) + offset.Width, 0);
            Label wed = new Label()
            {
                BackColor = Color.FromArgb(121, 134, 223),
                Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 30,
                Width = dayOfWeekLabelCompactWidth - 1
            }; ;
            wed.Location = new Point((dayOfWeekLabelCompactWidth) * 2 + offset.Width, 0);
            wed.Text = "Сряда";
            Label thu = new Label()
            {
                BackColor = Color.FromArgb(121, 134, 223),
                Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 30,
                Width = dayOfWeekLabelCompactWidth - 1
            }; ;
            thu.Location = new Point((dayOfWeekLabelCompactWidth) * 3 + offset.Width, 0);
            thu.Text = "Четвъртък";
            Label fri = new Label()
            {
                BackColor = Color.FromArgb(121, 134, 223),
                Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 30,
                Width = dayOfWeekLabelCompactWidth - 1
            }; ;
            fri.Text = "Петък";
            fri.Location = new Point((dayOfWeekLabelCompactWidth) * 4 + offset.Width, 0);

            compactViewsSubjectsDisplayMenu.Controls.AddRange(new Control[]{offset, mon,new Label()
            {
                BackColor = Color.Black,
                Height = 30,
                Width = 1,
                Location = new Point((dayOfWeekLabelCompactWidth-1) + offset.Width, 0)
            },tue,new Label()
            {
                BackColor = Color.Black,
                Height = 30,
                Width = 1,
                Location = new Point(dayOfWeekLabelCompactWidth*2-1 + offset.Width, 0)
            },wed,new Label()
            {
                BackColor = Color.Black,
                Height = 30,
                Width = 1,
                Location = new Point(dayOfWeekLabelCompactWidth*3-1 + offset.Width, 0)
            },thu,new Label()
            {
                BackColor = Color.Black,
                Height = 30,
                Width = 1,
                Location = new Point(dayOfWeekLabelCompactWidth*4-1 + offset.Width, 0)
            },fri,new Label()
            {
                BackColor = Color.Black,
                Height = 30,
                Width = 1,
                Location = new Point(dayOfWeekLabelCompactWidth*5-1 + offset.Width, 0)
            }});

            TimeTableField.Controls.Add(teacherLabel);
            TimeTableField.Controls.Add(compactViewsSubjectsDisplayMenu);
            return teachersList;
        }
        //Display compact views subject information
        void UpdateTeacherState(object sender, EventArgs e, ComboBox teachersList, int classHeight)
        {
            var removable = new List<Control>();
            foreach (Control control in compactViewsSubjectsDisplayMenu.Controls)
            {
                if (control.Name.Contains("removable"))
                {
                    removable.Add(control);
                }
            }

            for (int i = 0; i < removable.Count; i++)
            {

                compactViewsSubjectsDisplayMenu.Controls.Remove(removable[i]);
            }
            var teacherTable = new { subj = new SubjectDTO[5, 7], classes = new int[5, 7] };
            int day = -1;
            if (data is null)
            {
                MessageBox.Show("Няма генерирана таблица");
                return;
            }

            int st = -1;
            data.ForEach(t =>
            {
                st++;
                t.days.ToList().ForEach(d =>
                {
                    day++;
                    int hourInDay = -1;
                    d.IndexedHours.ForEach(h =>
                    {
                        hourInDay++;
                        if (h.Teacher.Any(te => te.Name == teachersList.Text))
                        {
                            teacherTable.subj[day, hourInDay] = h;
                            teacherTable.classes[day, hourInDay] = st;
                        }
                    });
                });
                day = -1;
            });
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Label l = new Label();
                    l.Name = "removable" + i + " " + j;
                    l.AutoSize = false;
                    l.Location = new Point(dayOfWeekLabelCompactWidth * i + this.leftMargin, 30 + classHeight * j + 1);
                    l.Height = classHeight;
                    l.Width = dayOfWeekLabelCompactWidth;
                    l.TextAlign = ContentAlignment.TopLeft;
                    l.Font = new System.Drawing.Font("Seorge UI", 10);
                    var hour = teacherTable.subj[i, j];
                    if (!(hour is null))
                    {
                        l.Text = controller.Classes[teacherTable.classes[i, j]].Name + "\n\t\t" + hour.Subject;
                    }
                    l.Controls.Add(new Label()
                    { Height = 1, Dock = DockStyle.Bottom, BackColor = Color.Black });
                    l.Controls.Add(new Label()
                    { Width = 1, Dock = DockStyle.Right, BackColor = Color.Black });
                    compactViewsSubjectsDisplayMenu.Controls.Add(l);
                }
            }
        }
        void UpdateClassState(object sender, EventArgs e, ComboBox classesList, int classHeight)
        {
            var removable = new List<Control>();
            foreach (Control control in compactViewsSubjectsDisplayMenu.Controls)
            {
                if (control.Name.Contains("removable"))
                {
                    removable.Add(control);
                }
            }

            for (int i = 0; i < removable.Count; i++)
            {
                compactViewsSubjectsDisplayMenu.Controls.Remove(removable[i]);
            }
            int day = -1;
            if (data is null)
            {
                MessageBox.Show("Няма генерирана таблица");
                return;
            }

            int st = -1;
            var classTable = data.Find(t => t.Class.Name == classesList.Text).days.Select(d => d.IndexedHours).ToArray();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Label l = new Label();
                    l.Name = "removable" + i + " " + j;
                    l.AutoSize = false;
                    l.Location = new Point(dayOfWeekLabelCompactWidth * i + this.leftMargin, 30 + classHeight * j + 1);
                    l.Height = classHeight;
                    l.Width = dayOfWeekLabelCompactWidth;
                    l.TextAlign = ContentAlignment.TopLeft;
                    l.Font = new System.Drawing.Font("Seorge UI", 10);
                    if (classTable[i].Count>j)
                    {
                        var subject = classTable[i][j];
                        ClassroomDTO classroom =
                            ClassroomsList[i, j].FirstOrDefault(c => c.Class.Name == classesList.Text);
                        if (classroom is null)
                        {
                            l.Text = subject.Subject + "\n\t\t" +
                                     string.Join("\n", subject.Teacher.Select(t => t.Name));
                        }
                        else
                        {
                            l.Text = subject.Subject + " - " + classroom.Number + "\n\t\t" +
                                     string.Join("\n", subject.Teacher.Select(t => t.Name));
                        }
                    }
                    l.Controls.Add(new Label()
                                       { Height = 1, Dock = DockStyle.Bottom, BackColor = Color.Black });
                    l.Controls.Add(new Label()
                    { Width = 1, Dock = DockStyle.Right, BackColor = Color.Black });
                    compactViewsSubjectsDisplayMenu.Controls.Add(l);
                }
            }
        }
        void UpdateCabinetState(object sender, EventArgs e, ComboBox cabinetsList, int classHeight)
        {
            var removable = new List<Control>();
            foreach (Control control in compactViewsSubjectsDisplayMenu.Controls)
            {
                if (control.Name.Contains("removable"))
                {
                    removable.Add(control);
                }
            }

            for (int i = 0; i < removable.Count; i++)
            {
                compactViewsSubjectsDisplayMenu.Controls.Remove(removable[i]);
            }
            int day = -1;
            if (data is null)
            {
                MessageBox.Show("Няма генерирана таблица");
                return;
            }

            int st = -1;
            var cabinetTable = new string[5, 7];
            var cabinetSubjectsTable = new SubjectDTO[5, 7];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    foreach (var cabinet in ClassroomsList[i, j])
                    {
                        if (cabinet.Number == int.Parse(cabinetsList.SelectedItem.ToString()))
                        {
                            cabinetTable[i, j] = cabinet.Class.Name;
                            try
                            {
                                var subj = data
                                           .Find(t => t.Class.Name == cabinet.Class.Name)
                                           .days[i].IndexedHours[j];
                                //if (subj.SubjectType==cabinet.SubjectType)
                                //{
                                //    cabinetSubjectsTable[i, j] = subj;
                                //}
                                cabinetSubjectsTable[i, j] = subj;
                            }
                            catch (Exception exception)
                            {
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Label l = new Label();
                    l.Name = "removable" + i + " " + j;
                    l.AutoSize = false;
                    l.Location = new Point(dayOfWeekLabelCompactWidth * i + this.leftMargin, 30 + classHeight * j + 1);
                    l.Height = classHeight;
                    l.Width = dayOfWeekLabelCompactWidth;
                    l.TextAlign = ContentAlignment.TopLeft;
                    l.Font = new System.Drawing.Font("Seorge UI", 10);
                    if (cabinetSubjectsTable[i, j] != null)
                    {
                        var clas = cabinetTable[i, j];
                        l.Text = clas + "\n\t\t" + cabinetSubjectsTable[i, j].Subject;
                    }
                    l.Controls.Add(new Label()
                    { Height = 1, Dock = DockStyle.Bottom, BackColor = Color.Black });
                    l.Controls.Add(new Label()
                    { Width = 1, Dock = DockStyle.Right, BackColor = Color.Black });
                    compactViewsSubjectsDisplayMenu.Controls.Add(l);
                }
            }
        }

        //Loads the items in the dropdowns
        private void MakeClassViewable()
        {
            var teachersList = PlotSpecificViews("Клас", "class", UpdateClassState);

            teachersList.Items.AddRange(controller.Classes.Select(t => t.Name).ToArray());
            TimeTableField.Controls.Add(teachersList);
        }
        private void MakeTeacherViewable()
        {
            var teachersList = PlotSpecificViews("Учител", "teacher", UpdateTeacherState);
            teachersList.Items.AddRange(controller.Teachers.Select(t => t.Name).ToArray());
            TimeTableField.Controls.Add(teachersList);
        }

        private void MakeCabinetViewable()
        {
            var cabinetsList = PlotSpecificViews("Кабинет", "cabinet", UpdateCabinetState);
            cabinetsList.Items.AddRange(controller.Classrooms.Select(t => t.Number.ToString()).ToArray());
            TimeTableField.Controls.Add(cabinetsList);
        }

        //Excel
        //Exports a standart student curriculum
        private void ExcelExport_Click(object sender, EventArgs e)
        {

            var oXL = new Microsoft.Office.Interop.Excel.Application();
            var oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
            var oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

            int maxHoursPerDay = 8;

            void LoadClass(int s)
            {
                int step = (3 + maxHoursPerDay);
                var heading = oSheet.get_Range($"A{s * step + 1}", $"F{s * (3 + maxHoursPerDay) + 1}");
                heading.Font.Bold = true;
                heading.VerticalAlignment =
                    Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                heading.Merge();
                heading.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                heading.Font.Size = 24;
                var range = oSheet.get_Range($"A{s * (2 + maxHoursPerDay) + s + 1}", $"F{(s + 1) * (2 + maxHoursPerDay) + s}");
                range.Borders.LineStyle = XlLineStyle.xlContinuous;
                range.Borders.Weight = XlBorderWeight.xlThin;
                oSheet.Cells[s * step + 1, 1] = controller.Classes[s].Name;
                Range r = oSheet.Columns[1];
                Range rows = oSheet.Rows[2 + s * step];
                rows.Font.Size = 16;

                r.ColumnWidth = 3;
                r.HorizontalAlignment = XlVAlign.xlVAlignCenter;
                r.VerticalAlignment = XlVAlign.xlVAlignCenter;
                r.Font.Size = 14;
                for (int i = 2; i < 7; i++)
                {
                    r = oSheet.Columns[i];
                    r.ColumnWidth = 18;
                }
                //Hour number
                for (int i = 3; i < 3 + maxHoursPerDay; i++)
                {
                    oSheet.Cells[i + s * step, 1] = i - 2;
                }
                //days of week
                oSheet.Cells[s * step + 2, 2] = "Понеделник";
                oSheet.Cells[s * step + 2, 3] = "Вторник";
                oSheet.Cells[s * step + 2, 4] = "Сряда";
                oSheet.Cells[s * step + 2, 5] = "Четвъртък";
                oSheet.Cells[s * step + 2, 6] = "Петък";
                //subjects
                oSheet.get_Range(oSheet.Cells.get_Address(3 + step * s, 2),
                                 oSheet.Cells.get_Address(2 + maxHoursPerDay + step * s, 6)).VerticalAlignment =
                    XlVAlign.xlVAlignCenter;
                for (int i = 2; i < 7; i++)
                {
                    for (int j = 3; j < 3 + maxHoursPerDay; j++)
                    {
                        if (data[s].days[i - 2].Sum <= j - 3)
                        {
                            continue;
                        }

                        var subject = data[s].days[i - 2].IndexedHours[j - 3];
                        oSheet.Cells[j + step * s, i] = $"{subject.Subject}\n{string.Join("\n", subject.Teacher.Select(t => t.Name))}";

                    }
                }
            }

            if (data is null)
            {
                MessageBox.Show("Първо трябва да генерираш програма");
                return;
            }
            for (int i = 0; i < data.Count; i++)
            {
                LoadClass(i);
            }
            
            try
            {
                oWB.SaveAs(@$"{downloadsPath}\GeneratedTable.xlsx", XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                           false, false, XlSaveAsAccessMode.xlNoChange,
                           Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Моля затвори таблицата");
            }

            oWB.Close();
            oXL.Quit();
            MessageBox.Show("Таблицата е запазена в Изтеглените файлове");
        }
        //Exports teacher`s curriculum
        private void TeachersExcel_Click(object sender, EventArgs e)
        {
            var oXL = new Microsoft.Office.Interop.Excel.Application();
            var oWB = (_Workbook)(oXL.Workbooks.Add(""));

            int maxHoursPerDay = 8;

            void LoadTeacher(int s, string name)
            {
                var oSheet = (_Worksheet)oWB.Worksheets.Add();
                oSheet.Name = name;
                oSheet.Rows.RowHeight = 30;
                var heading = oSheet.get_Range($"A1", $"F1");
                heading.Font.Bold = true;
                heading.VerticalAlignment =
                    XlVAlign.xlVAlignCenter;
                heading.Merge();
                heading.HorizontalAlignment = XlVAlign.xlVAlignCenter;
                heading.Font.Size = 24;
                var range = oSheet.get_Range($"A1", $"F{2 + maxHoursPerDay}");
                range.Borders.LineStyle = XlLineStyle.xlContinuous;
                range.Borders.Weight = XlBorderWeight.xlThin;
                oSheet.Cells[1, 1] = name;
                Range r = oSheet.Columns[1];
                Range rows = oSheet.Rows[2];
                rows.Font.Size = 16;

                r.ColumnWidth = 3;
                r.HorizontalAlignment = XlVAlign.xlVAlignCenter;
                r.VerticalAlignment = XlVAlign.xlVAlignCenter;
                r.Font.Size = 14;
                for (int i = 2; i < 7; i++)
                {
                    r = oSheet.Columns[i];
                    r.ColumnWidth = 18;
                }
                //Hour number
                for (int i = 3; i < 3 + maxHoursPerDay; i++)
                {
                    oSheet.Cells[i, 1] = i - 2;
                }
                //days of week
                oSheet.Cells[2, 2] = "Понеделник";
                oSheet.Cells[2, 3] = "Вторник";
                oSheet.Cells[2, 4] = "Сряда";
                oSheet.Cells[2, 5] = "Четвъртък";
                oSheet.Cells[2, 6] = "Петък";

                var teacherTable = new { subj = new SubjectDTO[5, 8], classes = new string[5, 8] };
                int day = -1;
                int st = -1;
                data.ForEach(t =>
                {
                    st++;
                    t.days.ToList().ForEach(d =>
                    {
                        day++;
                        int hourInDay = -1;
                        d.IndexedHours.ForEach(h =>
                        {
                            hourInDay++;
                            if (h.Teacher.Any(te => te.Name == name))
                            {
                                teacherTable.subj[day, hourInDay] = h;
                                teacherTable.classes[day, hourInDay] = t.Class.Name;
                            }
                        });
                    });
                    day = -1;
                });

                //subjects
                oSheet.get_Range(oSheet.Cells.get_Address(3, 2),
                                    oSheet.Cells.get_Address(2 + maxHoursPerDay, 6)).VerticalAlignment =
                    XlVAlign.xlVAlignCenter;
                for (int i = 2; i < 7; i++)
                {
                    for (int j = 3; j < 3 + maxHoursPerDay; j++)
                    {
                        var subject = teacherTable.subj[i - 2, j - 3];
                        if (!(subject is null))
                        {
                            oSheet.Cells[j, i] = $"{subject.Subject}\n\t{teacherTable.classes[i - 2, j - 3]}";
                        }
                    }
                }
            }

            if (data is null)
            {
                MessageBox.Show("Първо трябва да генерираш програма");
                return;
            }
            for (int i = 0; i < controller.Teachers.Count; i++)
            {
                LoadTeacher(i, controller.Teachers[i].Name);
            }
           
            string user = Application.ExecutablePath.Split("\\")[2];
            try
            {
                oWB.SaveAs(@$"{downloadsPath}\GeneratedTeachersTable.xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                           false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                           Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Моля затвори таблицата");
            }

            oWB.Close();
            oXL.Quit();
            MessageBox.Show("Таблицата е запазена в Изтеглените файлове");
        }

        private void ExcelClassrooms_Click(object sender, EventArgs e)
        {
            var oXL = new Microsoft.Office.Interop.Excel.Application();
            var oWB = (_Workbook)(oXL.Workbooks.Add(""));

            int maxHoursPerDay = 8;

            void LoadClassroom(int s, string name, int num)
            {
                var oSheet = (_Worksheet)oWB.Worksheets.Add();
                oSheet.Name = name;
                oSheet.Rows.RowHeight = 30;
                var heading = oSheet.get_Range($"A1", $"F1");
                heading.Font.Bold = true;
                heading.VerticalAlignment =
                    XlVAlign.xlVAlignCenter;
                heading.Merge();
                heading.HorizontalAlignment = XlVAlign.xlVAlignCenter;
                heading.Font.Size = 24;
                var range = oSheet.get_Range($"A1", $"F{2 + maxHoursPerDay}");
                range.Borders.LineStyle = XlLineStyle.xlContinuous;
                range.Borders.Weight = XlBorderWeight.xlThin;
                oSheet.Cells[1, 1] = name;
                Range r = oSheet.Columns[1];
                Range rows = oSheet.Rows[2];
                rows.Font.Size = 16;

                r.ColumnWidth = 3;
                r.HorizontalAlignment = XlVAlign.xlVAlignCenter;
                r.VerticalAlignment = XlVAlign.xlVAlignCenter;
                r.Font.Size = 14;
                for (int i = 2; i < 7; i++)
                {
                    r = oSheet.Columns[i];
                    r.ColumnWidth = 18;
                }
                //Hour number
                for (int i = 3; i < 3 + maxHoursPerDay; i++)
                {
                    oSheet.Cells[i, 1] = i - 2;
                }
                //days of week
                oSheet.Cells[2, 2] = "Понеделник";
                oSheet.Cells[2, 3] = "Вторник";
                oSheet.Cells[2, 4] = "Сряда";
                oSheet.Cells[2, 5] = "Четвъртък";
                oSheet.Cells[2, 6] = "Петък";
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        ClassroomDTO clasroom = ClassroomsList[i, j].FirstOrDefault(c => c.Number == num);
                        if (!(clasroom is null))
                        {
                            SubjectDTO subj = data.FirstOrDefault(t => t.Class.Name == clasroom.Class.Name).days[i]
                                                 .IndexedHours[j];
                            oSheet.Cells[j + 3, i + 2] = $"{clasroom.Class.Name} - {subj.Subject}\n\t{string.Join("\n", subj.Teacher.Select(t => t.Name))}";
                        }
                    }
                }

                //subjects
                oSheet.get_Range(oSheet.Cells.get_Address(3, 2),
                                    oSheet.Cells.get_Address(2 + maxHoursPerDay, 6)).VerticalAlignment =
                    XlVAlign.xlVAlignCenter;
            }

            if (data is null)
            {
                MessageBox.Show("Първо трябва да генерираш програма");
                return;
            }
            for (int i = 0; i < controller.Classrooms.Count; i++)
            {
                LoadClassroom(i, $"{controller.Classrooms[i].Number} - {controller.Classrooms[i].SubjectType}", controller.Classrooms[i].Number);
            }

            string user = Application.ExecutablePath.Split("\\")[2];
            try
            {
                oWB.SaveAs(@$"{downloadsPath}\GeneratedClassroomsTable.xlsx", XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                           false, false, XlSaveAsAccessMode.xlNoChange,
                           Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Моля затвори таблицата");
            }

            oWB.Close();
            oXL.Quit();
            MessageBox.Show("Таблицата е запазена в Изтеглените файлове");
        }

        private void save_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            controller.SaveTable(data, ClassroomsList);
            isSaved = true;
        }

        private void TimeTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isSaved)
            {
                DialogResult dialogResult = MessageBox.Show("Таблицата не е запазена, искаш ли да я запазиш?",
                                                            "Предупреждение", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //Save hte table
                    Cursor.Current = Cursors.WaitCursor;
                    controller.SaveTable(data, ClassroomsList);
                }
            }

        }
    }
}
