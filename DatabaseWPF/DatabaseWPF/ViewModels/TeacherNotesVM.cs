using DatabaseWPF.Models.DataAccesLayer;
using DatabaseWPF.Models.DatabaseEntities;
using DatabaseWPF.Models.Helpers;
using DatabaseWPF.ViewModels.ForBindings;
using DatabaseWPF.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseWPF.ViewModels
{
    class TeacherNotesVM : BaseVM
    {
        #region Constants

        private static readonly List<string> noList = new() { "Lista este goala" };

        private static readonly Regex dateRegex = new Regex(@"\d\d\d\d-\d\d-\d\d");

        private const string dateFormat = "AAAA-LL-ZZ";

        #endregion

        private IEnumerable _allEntities;

        private Professor currentProfessor;
        private bool canUpdate = false;
        private bool canAdd = false;
        private int semesterNow;

        private Class _currentClass;
        private List<StudentFull> _students;
        private bool _noteVisibility = false;
        private bool _isThesis;
        private DateTime _noteDate = DateTime.Now;
        private string _note;
        private StudentFull _currrentStudent;
        private bool _allNotesVis = false;
        private bool _viewAllNotes;
        private Counter _totalCount;
        private Semester _currentSemester;

        public TeacherNotesVM(MainWindow window) : base(window)
        {
            currentProfessor = DALHelper.GetObject<Professor>("GetProfessorByPerson", ("@personID", window.Person_ID));

            if (currentProfessor is null)
            {
                currentProfessor = DALHelper.GetObject<Professor>("GetProfessorByPerson", ("@personID", 13));
            }

            Classes = GetClasses();

            OpenAddNoteCommand = new ButtonCommand(OpenAddNote);
            UpdateNoteCommand = new ButtonCommand(UpdateNote);
            DeleteNoteCommand = new ButtonCommand(DeleteNote);
            MeansCommand = new ButtonCommand(Means);

            Semesters = DALHelper.GetObjects<Semester>("GetSemesters");
            CurrentSemester = Semesters[Semesters.Count - 1];
            semesterNow = CurrentSemester.ID;
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

        private bool CheckNote()
        {
            if (!int.TryParse(Note, out var note))
            {
                MessageBox.Show("Introduceti un numar valid");
                return false;
            }
            else
            {
                if (note is < 1 or > 10)
                {
                    MessageBox.Show("Introduceti un numar de la 1 la 10");
                    return false;
                }
            }

            return true;
        }

        private void AddNote()
        {
            if (!CheckNote())
            {
                return;
            }

            try
            {
                DAL.AddNote(int.Parse(Note), currentProfessor.Subject_ID, CurrentStudent.ID, NoteDate, IsThesis, semesterNow);

                ViewNotes(null);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #region Commands

        private void ViewNotes(object o)
        {
            if(CurrentClass is null)
            {
                return;
            }

            if (CurrentStudent is not null)
            {
                int? subjectID = (ViewAllNotes) ? null : currentProfessor.Subject_ID;
                var list = DAL.GetNotesSubject(CurrentStudent.ID, CurrentSemester.ID ,subjectID) as IList;

                if (list.Count > 0)
                {
                    AllEntities = list;
                }
                else
                {
                    AllEntities = noList;
                }
            }
            else
            {
                MessageBox.Show("Alegeti un elev");
            }
        }

        public void OpenAddNote(object o)
        {
            if (CurrentStudent is not null)
            {
                if (canAdd == false)
                {
                    canAdd = true;
                    canUpdate = false;

                    NoteVisibility = true;
                    NoteDate = DateTime.Now;
                    Note = string.Empty;
                    IsThesis = false;
                }
                else
                {
                    AddNote();
                }
            }
            else
            {
                MessageBox.Show("Alegeti un student");
            }
        }

        public ButtonCommand OpenAddNoteCommand { get; init; }

        private void ShowUpdate()
        {
            NoteVisibility = true;

            SimpleNote aux = CurrentNote as SimpleNote;

            Note = aux.Score.ToString();
            NoteDate = aux.Date;
            IsThesis = aux.IsThesis;

            canUpdate = true;
            canAdd = false;
        }

        private void UpdateNote(object o)
        {
            if(CurrentNote is null)
            {
                return;
            }

            if(canUpdate is false)
            {
                ShowUpdate();
            }
            else
            {
                try
                {
                    DAL.UpdateNote(CurrentNote.ID, int.Parse(Note), NoteDate, IsThesis);
                    MessageBox.Show("Note Updated");

                    ViewNotes(null);
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public ButtonCommand UpdateNoteCommand { get; init; }

        private void DeleteNote(object o)
        {
            if(CurrentNote is not null)
            {
                try
                {
                    DALHelper.ExecuteNonQuery("DeleteNote", ("@id", CurrentNote.ID));

                    MessageBox.Show("Nota a fost stearsa");
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }

                ViewNotes(null);
            }
        }

        public ButtonCommand DeleteNoteCommand { get; init; }

        public void Means(object o)
        {
            if (!ViewAllNotes)
            {
                AllEntities = MeanHelper.Get(CurrentStudent.ID, CurrentSemester.ID ,currentProfessor.Subject_ID) as IEnumerable;
            }
            else
            {
                AllEntities = MeanHelper.Get(CurrentStudent.ID, CurrentSemester.ID ,null) as IEnumerable;
            }
        }

        public ButtonCommand MeansCommand { get; set; }

        #endregion

        #region Bidings

        public IEnumerable AllEntities
        {
            get => _allEntities;

            set
            {
                _allEntities = value;

                OnPropertyChanged();
            }
        }

        public DatabaseObject CurrentNote { get; set; }

        public List<Class> Classes { get; init; }

        public Class CurrentClass
        {
            get => _currentClass;

            set
            {
                _currentClass = value;

                if(value.ID == currentProfessor.Class_ID)
                {
                    AllNotesVis = true;
                }
                else
                {
                    AllNotesVis = false;
                }

                Students = DALHelper.GetObjects<StudentFull>("GetStudentsFromClass", ("@classID", CurrentClass.ID));
            }
        }

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

                if (Values is not null)
                {
                    ViewNotes(null);
                }
            }
        }

        public string Note
        {
            get => _note;

            set
            {
                _note = value;

                OnPropertyChanged();
            }
        }

        public bool NoteVisibility
        {
            get => _noteVisibility;

            set
            {
                _noteVisibility = value;

                OnPropertyChanged();
            }
        }

        public DateTime NoteDate 
        {
            get => _noteDate;

            set
            {
                _noteDate = value;

                OnPropertyChanged();
            }
        }

        public bool IsThesis 
        {
            get => _isThesis;

            set
            {
                _isThesis = value;

                OnPropertyChanged();
            }
        }

        public bool AllNotesVis
        {
            get => _allNotesVis;

            set
            {
                _allNotesVis = value;

                OnPropertyChanged();
            }
        }

        public bool ViewAllNotes
        {
            get => _viewAllNotes;

            set
            {
                _viewAllNotes = value;

                OnPropertyChanged();

                ViewNotes(null);
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

        public Semester CurrentSemester
        {
            get => _currentSemester;

            set
            {
                _currentSemester = value;

                ViewNotes(null);
            }
        }

        public List<Semester> Semesters { get; init; }

        #endregion
    }
}
