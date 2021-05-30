using DatabaseWPF.Models.DataAccesLayer;
using DatabaseWPF.Models.DatabaseEntities;
using DatabaseWPF.ViewModels.ForBindings;
using DatabaseWPF.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseWPF.ViewModels
{
    class TeacherAbsencesVM : BaseVM
    {
        #region Constants

        private string[] noList = { "Nici o absenta" };

        #endregion

        private Professor currentProfessor;
        private bool canAdd = false;
        private bool canUdate = false;

        private Class _currentClass;
        private List<StudentFull> _students;
        private StudentFull _currrentStudent;
        private IEnumerable _allEntities;
        private bool _dateVisibility = false;
        private bool _motivatedVisibility = false;
        private DateTime _date = DateTime.Now;
        private bool _viewAllAbsences = false;
        private bool _allAbsencesVis;
        private Counter _totalCount;
        private bool _justUnmotivated;
        private Semester _currentSemester;
        private int semesterNow;

        public TeacherAbsencesVM(MainWindow window) : base(window)
        {
            currentProfessor = DALHelper.GetObject<Professor>("GetProfessorByPerson", ("@personID", window.Person_ID));

            if (currentProfessor is null)
            {
                currentProfessor = DALHelper.GetObject<Professor>("GetProfessorByPerson", ("@personID", 13));
            }

            Classes = GetClasses();
            Semesters = DALHelper.GetObjects<Semester>("GetSemesters");
            CurrentSemester = Semesters[Semesters.Count - 1];
            semesterNow = CurrentSemester.ID;

            AddCommand = new ButtonCommand(Add);
            UpdateCommand = new ButtonCommand(Update);
            DeleteCommand = new ButtonCommand(Delete);
            AllAbsencesCommand = new ButtonCommand(AllAbsences);
            ViewAbsencesCommand = new ButtonCommand(ViewAbsences);
        }

        private List<Class> GetClasses()
        {
            List<Class> result = DAL.GetClassesFromProfessor(currentProfessor.ID);

            if (currentProfessor.Class_ID is not null)
            {
                foreach (var @class in result)
                {
                    if (@class.ID == currentProfessor.Class_ID)
                    {
                        return result;
                    }
                }

                result.Add(DALHelper.GetObject<Class>("GetClass", ("@id", currentProfessor.Class_ID)));
            }

            return result;
        }

        private void ViewAbsences(object o)
        {
            if (CurrentClass is not null && CurrentStudent is not null)
            {
                int? subjectID = (ViewAllAbsences) ? null : currentProfessor.Subject_ID;
                IList list = null;

                if (JustUnmotivated is false)
                {
                    list = DAL.GetStudentAbsences(CurrentStudent.ID, CurrentSemester.ID ,subjectID) as IList;
                }
                else
                {
                    list = DAL.GetStudentAbsencesUnmotivated(CurrentStudent.ID, CurrentSemester.ID, subjectID) as IList;
                }

                if (list.Count > 0)
                {
                    AllEntities = list;

                    TotalCount = new Counter(list.Count);
                }
                else
                {
                    AllEntities = noList;
                }
            }
        }

        #region Commands

        private void Add(object o)
        {
            if(canAdd is false)
            {
                canAdd = true;
                canUdate = false;

                DateVisibility = true;
                MotivatedVisibility = false;
            }
            else
            {
                try
                {
                    DAL.AddAbsence(Date, CurrentStudent.ID, currentProfessor.Subject_ID, semesterNow);
                    MessageBox.Show(Date.ToString());
                    canAdd = false;
                    DateVisibility = false;
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                ViewAbsences(null);
            }
        }
        
        public ButtonCommand AddCommand { get; init; }

        public void Update(object o)
        {
            if(canUdate is false)
            {
                if (CurrentAbsence is not null)
                {
                    canUdate = true;
                    canAdd = false;

                    DateVisibility = true;
                    MotivatedVisibility = true;

                    SimpleAbsence value = CurrentAbsence as SimpleAbsence;

                    Date = value.Date;
                    Motivated = value.Motivated;
                }
                else
                {
                    MessageBox.Show("Alegeti o absenta");
                }
            }
            else
            {
                try
                {
                    DAL.UpdateAbsence(CurrentAbsence.ID, Date, Motivated);

                    MessageBox.Show($"Absenta actualizata");

                    ViewAbsences(null);
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public ButtonCommand UpdateCommand { get; init; }

        private void Delete(object o)
        {
            if (CurrentAbsence is not null)
            {
                canUdate = false;
                canAdd = false;

                DateVisibility = false;
                MotivatedVisibility = false;

                try
                {
                    DALHelper.ExecuteNonQuery("DeleteAbsence", ("id", CurrentAbsence.ID));

                    MessageBox.Show($"Absenta streasa");

                    ViewAbsences(null);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                MessageBox.Show("Alegeti o absenta");
            }
        }

        public ButtonCommand DeleteCommand { get; init; }

        private void AllAbsences(object o)
        {
            List<StudentAbsence> list = null;

            if(JustUnmotivated)
            {
                list = DALHelper.GetObjects<StudentAbsence>("GetUnmotivatedClassAbsences", ("@classID", currentProfessor.Class_ID), ("@semesterID", CurrentSemester.ID));
            }
            else
            {
               list = DALHelper.GetObjects<StudentAbsence>("GetClassAbsences", ("@classID", currentProfessor.Class_ID), ("@semesterID", CurrentSemester.ID));
            }

            AllEntities = list;
            TotalCount = new Counter(list.Count);
        }

        public ButtonCommand AllAbsencesCommand { get; init; }

        public ButtonCommand ViewAbsencesCommand { get; init; }

        #endregion

        #region Bidings

        public Class CurrentClass
        {
            get => _currentClass;

            set
            {
                _currentClass = value;

                Students = DALHelper.GetObjects<StudentFull>("GetStudentsFromClass", ("@classID", CurrentClass.ID));

                if(value.ID == currentProfessor.Class_ID)
                {
                    AllAbsencesVis = true;
                }
                else
                {
                    AllAbsencesVis = false;
                    ViewAllAbsences = false;
                }
            }
        }

        public List<Class> Classes { get; init; }

        public List<StudentFull> Students
        {
            get => _students;

            set
            {
                _students = value;

                OnPropertyChanged();
            }
        }

        public StudentFull CurrentStudent
        {
            get => _currrentStudent;

            set
            {
                _currrentStudent = value;

                if (value is not null)
                {
                    ViewAbsences(null);
                }
                else
                {
                    AllEntities = null;
                }
            }
        }

        public IEnumerable AllEntities
        {
            get => _allEntities;

            set
            {
                _allEntities = value;

                OnPropertyChanged();
            }
        }

        public DatabaseObject CurrentAbsence{ get; set; }

        public bool DateVisibility
        {
            get => _dateVisibility;

            set
            {
                _dateVisibility = value;

                OnPropertyChanged();
            }
        }

        public bool MotivatedVisibility
        {
            get => _motivatedVisibility;

            set
            {
                _motivatedVisibility = value;

                OnPropertyChanged();
            }
        }

        public bool Motivated { get; set; }

        public DateTime Date
        {
            get => _date;

            set
            {
                _date = value;

                OnPropertyChanged();
            }
        }

        public bool ViewAllAbsences
        {
            get => _viewAllAbsences;

            set
            {
                _viewAllAbsences = value;

                ViewAbsences(null);

                OnPropertyChanged();
            }
        }

        public bool AllAbsencesVis
        {
            get => _allAbsencesVis;

            set
            {
                _allAbsencesVis = value;
                JustUnmotivated = false;

                OnPropertyChanged();
            }
        }

        public Counter TotalCount
        {
            get => _totalCount;

            set
            {
                _totalCount = value;

                OnPropertyChanged();
            }
        }

        public bool JustUnmotivated
        {
            get => _justUnmotivated;

            set
            {
                _justUnmotivated = value;

                OnPropertyChanged();
            }
        }

        public List<Semester> Semesters { get; init; }

        public Semester CurrentSemester
        {
            get => _currentSemester;

            set
            {
                _currentSemester = value;

                ViewAbsences(null);

                OnPropertyChanged();
            }
        }
        #endregion
    }
}
