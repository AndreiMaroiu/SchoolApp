using DatabaseWPF.Models.DataAccesLayer;
using DatabaseWPF.Models.DatabaseEntities;
using DatabaseWPF.Models.Helpers;
using DatabaseWPF.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseWPF.ViewModels
{
    class TeacherCoursesVM : BaseVM
    {
        private static readonly List<Course> noList = new() { new Course() { Name = "Niciun curs gasit" } };

        private Professor currentProfessor;
        private string filePath;
        private string _fileName;
        private Class _currentClass;
        private List<Course> _allCourses;

        public TeacherCoursesVM(MainWindow window) : base(window)
        {
            OpenFileCommand = new ButtonCommand(OpenFile);
            SaveFileCommand = new ButtonCommand(SaveFile);
            DeleteCommand = new ButtonCommand(DeleteCourse);

            currentProfessor = DALHelper.GetObject<Professor>("GetProfessorByPerson", ("@personID", window.Person_ID));

            if (currentProfessor is null)
            {
                currentProfessor = DALHelper.GetObject<Professor>("GetProfessor", ("@id", 1));
            }

            Classes = DAL.GetClassesFromProfessor(currentProfessor.ID);
        }

        private void SetAllCourses()
        {
            ClassSubject classSubject = DAL.GetClassSubject(CurrentClass.ID, currentProfessor.Subject_ID);

            if (classSubject is null)
            {
                AllCourses = noList;
                return;
            }

            var list = DALHelper.GetObjects<Course>("GetCoursesFromClassSubject", ("@id", classSubject.ID));

            if (list.Count > 0)
            {
                AllCourses = list;
            }
            else
            {
                AllCourses = noList;
            }
        }

        #region Commands

        private void OpenFile(object o)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = FileFilters.Custom;

            if (dialog.ShowDialog() is true)
            {
                filePath = dialog.FileName;

                FileInfo info = new FileInfo(filePath);

                FileName = info.Name;
            }
        }

        public ButtonCommand OpenFileCommand { get; init; }

        private void SaveFile(object o)
        {
            try
            {
                using Stream stream = File.OpenRead(filePath);

                byte[] buffer = new byte[stream.Length];

                stream.Read(buffer, 0, buffer.Length);

                string extention = new FileInfo(filePath).Extension;

                ClassSubject classSubject = DAL.GetClassSubject(CurrentClass.ID, currentProfessor.Subject_ID);

                if (classSubject is null)
                {
                    MessageBox.Show("Professorul nu preda la aceasta clasa");
                    return;
                }

                object description;

                if (CourseDescription is null or "")
                {
                    description = DBNull.Value;
                }
                else
                {
                    description = CourseDescription;
                }

                DAL.AddCourse(CourseName, buffer, extention, description, classSubject.ID);

                MessageBox.Show("Curs adaugat cu succes");
                SetAllCourses();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public ButtonCommand SaveFileCommand { get; init; }

        private void DeleteCourse(object o)
        {
            if(CurrentCourse is not null)
            {
                try
                {
                    DALHelper.ExecuteNonQuery("DeleteCourse", ("@id", CurrentCourse.ID));
                    MessageBox.Show("Curs Sters");
                    SetAllCourses();
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                MessageBox.Show("Alegeti un curs");
            }
        }

        public ButtonCommand DeleteCommand { get; init; }

        #endregion

        #region Bidings

        public string CourseName { get; set; }

        public string CourseDescription { get; set; }

        public string FileName
        {
            get => _fileName;

            set
            {
                _fileName = value;

                OnPropertyChanged();
            }
        }

        public List<Class> Classes { get; init; }

        public Class CurrentClass
        {
            get => _currentClass;

            set
            {
                _currentClass = value;

                SetAllCourses();
            }
        }

        public List<Course> AllCourses
        {
            get => _allCourses;

            set
            {
                _allCourses = value;

                OnPropertyChanged();
            }
        }

        public Course CurrentCourse { get; set; }
        #endregion
    }
}
