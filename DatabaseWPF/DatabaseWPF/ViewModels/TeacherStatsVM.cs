using DatabaseWPF.Models.DataAccesLayer;
using DatabaseWPF.Models.DatabaseEntities;
using DatabaseWPF.Models.Helpers;
using DatabaseWPF.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.ViewModels
{
    class TeacherStatsVM : BaseVM
    {
        #region Constants

        private static readonly List<string> noList = new() { "Lista este goala" };

        #endregion

        private Class currentClass;
        private Professor currentProfessor;
        private IList _allEntities;

        public TeacherStatsVM(MainWindow window) : base(window)
        {
            currentProfessor = DALHelper.GetObject<Professor>("GetProfessorByPerson", ("@personID", window.Person_ID));

            if(currentProfessor is null)
            {
                currentProfessor = DALHelper.GetObject<Professor>("GetProfessor", ("@id", 1));
            }

            currentClass = DALHelper.GetObject<Class>("GetClass", ("@id", currentProfessor.Class_ID));
            ClassLabel = $"Statistici clasa: {currentClass}";
            Students = DALHelper.GetObjects<StudentFull>("GetStudentsFromClass", ("@classID", currentClass.ID));
            Semesters = DALHelper.GetObjects<Semester>("GetSemesters");
            CurrentSemester = Semesters[Semesters.Count - 1];

            HierarchyCommand = new ButtonCommand(ViewHierarchy);
            ViewTopCommand = new ButtonCommand(ViewTop);
            ViewRepeatersCommand = new ButtonCommand(ViewRepeaters);
            ViewFailedStudentsCommand = new ButtonCommand(ViewFailed);
            ViewOutStudentsCommand = new ButtonCommand(ViewOutStudents);
            ViewStudentsCommand = new ButtonCommand((object o) => AllEntities = Students);
        }

        #region Commands

        private void ViewHierarchy(object o)
        {
            List<SubjectMean> means = GetClassMeans();

            AllEntities = means;
        }

        private List<SubjectMean> GetClassMeans()
        {
            List<SubjectMean> means = new();

            foreach (var student in Students)
            {
                var mean = MeanHelper.GetGenerealMean(student.ID, CurrentSemester.ID);
                Person person = student;
                mean.Subject = person.ToString();
                means.Add(mean);
            }

            means.Sort((left, right) => right.Score.CompareTo(left.Score));
            return means;
        }

        private List<List<SubjectMean>> GetClassFullMeans()
        {
            List<List<SubjectMean>> means = new();

            foreach (var student in Students)
            {
                var mean = MeanHelper.GetMeans(student.ID, CurrentSemester.ID);

                means.Add(mean);
            }

            return means;
        }

        public ButtonCommand HierarchyCommand { get; init; }

        private void ViewTop(object o)
        {
            List<SubjectMean> means = GetClassMeans();

            List<SubjectMean> result = new();
            for(int i = 0; i < 4; ++i)
            {
                means[i].Subject = $"{i + 1}. {means[i].Subject}";
                result.Add(means[i]);
            }

            AllEntities = result;
        }

        public ButtonCommand ViewTopCommand { get; init; }

        private void ViewRepeaters(object o)
        {
            List<Person> result = new();
            List<List<SubjectMean>> subjectMeans = GetClassFullMeans();

            for(int i = 0; i < Students.Count; ++i)
            {
                int count = GetFailedClassesCount(subjectMeans[i]);

                if (count >= 3)
                {
                    result.Add(Students[i]);
                }
            }

            AllEntities = result;
        }

        private static int GetFailedClassesCount(List<SubjectMean> means)
        {
            int count = 0;

            foreach (var mean in means)
            {
                if (mean.Score < 5f)
                {
                    ++count;
                }
            }

            return count;
        }

        public ButtonCommand ViewRepeatersCommand { get; init; }

        private void ViewFailed(object o)
        {
            List<string> result = new();
            List<List<SubjectMean>> subjectMeans = GetClassFullMeans();

            for (int i = 0; i < Students.Count; ++i)
            {
                foreach (var mean in subjectMeans[i])
                {
                    if (mean.Score < 5f)
                    {
                        result.Add($"{Students[i].FirstName.Trim()} {Students[i].LastName.Trim()} {mean}");
                    }
                }
            }

            AllEntities = result;
        }

        public ButtonCommand ViewFailedStudentsCommand { get; init; }

        private void ViewOutStudents(object o)
        {
            List<StudentFull> result = new();

            foreach(var student in Students)
            {
                IList absences = DAL.GetStudentAbsencesUnmotivated(student.ID, CurrentSemester.ID, null) as IList;

                if(absences.Count >= SettingsHelper.MaxAbsences)
                {
                    result.Add(student);
                }
            }

            AllEntities = result;
        }

        public ButtonCommand ViewOutStudentsCommand { get; init; }

        public ButtonCommand ViewStudentsCommand { get; init; }

        #endregion

        #region Bidings

        public string ClassLabel { get; init; }

        public List<StudentFull> Students { get; init; }

        public StudentFull CurrentStudent { get; set; }

        public IList AllEntities
        {
            get => _allEntities;

            set
            {
                if (value.Count is 0)
                {
                    _allEntities = noList;
                }
                else
                {
                    _allEntities = value;
                }

                OnPropertyChanged();
            }
        }

        public List<Semester> Semesters { get; init; }

        public Semester CurrentSemester { get; set; }

        #endregion
    }
}
