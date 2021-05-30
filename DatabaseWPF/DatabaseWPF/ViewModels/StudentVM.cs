using DatabaseWPF.Models;
using DatabaseWPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseWPF.Models.DataAccesLayer;
using DatabaseWPF.Models.DatabaseEntities;
using System.Windows;
using System.Collections;
using DatabaseWPF.ViewModels.Options;
using DatabaseWPF.Models.Helpers;

namespace DatabaseWPF.ViewModels
{
    class StudentVM : BaseVM
    {
        private static readonly List<string> noList = new (){ "Niciun obiect gasit" };
        private static readonly Dictionary<string, List<string>> options = OptionsReader.ReadOptions("MyOptions.txt");

        private object _objectsList;
        private Subject _currentSubject;
        private int _selectedIndex;
        private string _currentOption;

        private Student currentStudent;
        private string _currentSubjectLabel;
        private bool _downloadVis = false;

        public StudentVM(MainWindow window) : base(window)
        {
            ViewCommand = new ButtonCommand(ViewObjects);
            DownloadCommand = new ButtonCommand(Download);

            BackgroundImage = ImageHelper.GetImage("StudentBackground.png");

            currentStudent = DALHelper.GetObject<Student>("GetStudentByPersonID", ("@id", person.ID));
            Semesters = DALHelper.GetObjects<Semester>("GetSemesters");
            CurrentSemester = Semesters[Semesters.Count - 1];
        }

        private void GetCourses()
        {
            if(CurrentSubject is null)
            {
                MessageBox.Show("Alegeti o materie");
                return;
            }

            ClassSubject classSubject = DAL.GetClassSubject(currentStudent.Class_ID, CurrentSubject.ID);

            if(classSubject is null)
            {
                ObjectsList = noList;
                return;
            }

            ObjectsList = DALHelper.GetObjects<Course>("GetCoursesFromClassSubject", ("@id", classSubject.ID));
        }

        public ButtonCommand ViewCommand { get; init; }

        private void Download(object o)
        {
            Course course = CurrentObject as Course;

            if(course is null)
            {
                MessageBox.Show("Alegeti un curs");
            }
            else
            {
                course.Download();
            }
        }

        public ButtonCommand DownloadCommand { get; init; }

        private void ViewObjects(object o)
        {
            CurrentSubjectLabel = CurrentSubject?.SubjectName.Trim();

            switch (CurrentOption) 
            {
                case "Note":
                    DownloadVis = false;
                    ObjectsList = DAL.GetNotesSubject(currentStudent.ID, CurrentSemester.ID ,CurrentSubject?.ID);
                    break;
                case "Absente":
                    DownloadVis = false;
                    ObjectsList = DAL.GetStudentAbsences(currentStudent.ID, CurrentSemester.ID ,CurrentSubject?.ID);
                    break;
                case "Medii":
                    DownloadVis = false;
                    ObjectsList = MeanHelper.Get(currentStudent.ID, CurrentSemester.ID ,CurrentSubject?.ID);
                    break;
                case "Materiale":
                    DownloadVis = true;
                    GetCourses();
                    break;
            }
        }

        #region Properties

        public object ObjectsList
        {
            get => _objectsList;

            set
            {
                IList list = value as IList;

                if (list.Count > 0)
                {
                    _objectsList = value;
                }
                else
                {
                    _objectsList = new List<string>() { "Nu s-a gasit nimic" };
                }

                CurrentSubject = null;

                SelectedIndex = -1;

                OnPropertyChanged();
            }
        }

        public List<string> Options => options.Keys.ToList();

        public string CurrentOption
        {
            get => _currentOption;

            set
            {
                _currentOption = value;

                CurrentSubject = null;
                SelectedIndex = -1;

                CurrentSubjectLabel = null;

                OnPropertyChanged();
            }
        }

        public List<Subject> Subjects => DAL.GetSubjectsFromClass(currentStudent.Class_ID);

        public Subject CurrentSubject
        {
            get => _currentSubject;

            set
            {
                if (value is not null)
                {
                    _currentSubject = value;
                }

                OnPropertyChanged();
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;

            set
            {
                _selectedIndex = value;

                OnPropertyChanged();
            }
        }

        public string CurrentSubjectLabel
        {
            get => _currentSubjectLabel;

            set
            {
                if (value is not null)
                {
                    _currentSubjectLabel = $"Materia: {value}";
                }
                else
                {
                    _currentSubjectLabel = value;
                }
                OnPropertyChanged();
            }
        }

        public List<Semester> Semesters { get; init; }

        public Semester CurrentSemester { get; set; }

        public object CurrentObject { get; set; }

        public bool DownloadVis
        {
            get => _downloadVis;

            set
            {
                _downloadVis = value;

                OnPropertyChanged();
            }
        }

        #endregion
    }
}
