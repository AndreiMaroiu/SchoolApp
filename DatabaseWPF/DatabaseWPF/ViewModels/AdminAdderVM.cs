using DatabaseWPF.Models.DataAccesLayer;
using DatabaseWPF.Models.DatabaseEntities;
using DatabaseWPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseWPF.ViewModels
{
    class AdminAdderVM : BaseVM
    {
        #region Constants

        private const int MAXFIELDS = 4;

        private static readonly List<string> entities = new()
        { "Persoana", "Clasa", "Materie", "Specializare", "Utilizator", "Semestru" };

        #endregion

        private string _buttonText;
        private string _currentEntity;
        private object _additionalSource;
        private DatabaseObject _currentAdditional;
        private object _allEntities;

        public AdminAdderVM(MainWindow window) : base(window)
        {
            MainButtonCommand = new ButtonCommand(Add);

            Visibilities = new();
            Texts = new();
            Labels = new();

            for(int i = 0; i < MAXFIELDS; ++i)
            {
                Visibilities.Add(false);
                Texts.Add(string.Empty);
                Labels.Add(string.Empty);
            }
        }

        #region Set Methods

        private void SetWindowsObject(string value)
        {
            switch(value)
            {
                case "Persoana":
                    SetPersonObjects();
                    break;
                case "Clasa":
                    SetClassObjects();
                    break;
                case "Materie":
                    SetSubjectObjects();
                    break;
                case "Specializare":
                    SetSpecializationObjects();
                    break;
                case "Utilizator":
                    SetUserObjects();
                    break;
                case "Semestru":
                    SetSemesterObjects();
                    break;
            }
        }

        private void SetPersonObjects()
        {
            SetVisibilities(2);

            Labels[0] = "Prenume";
            Labels[1] = "Nume";

            AllEntities = DAL.GetPeople();
        }

        private void SetClassObjects()
        {
            SetVisibilities(2);

            Labels[0] = "An";
            Labels[1] = "Litera";

            AllEntities = DAL.GetClasses();
        }

        private void SetSubjectObjects()
        {
            SetVisibilities(1);

            Labels[0] = "Materie";

            AllEntities = DAL.GetSubjects();
        }

        private void SetSpecializationObjects()
        {
            SetVisibilities(1);

            Labels[0] = "Specializare";

            AllEntities = DAL.GetSpecializations();
        }

        private void SetUserObjects()
        {
            SetVisibilities(4);

            Labels[0] = "Nume Utilizator";
            Labels[1] = "Parola";
            Labels[2] = "Rol";

            AdditionalSource = DALHelper.GetObjects<Person>("GetPersons");

            AllEntities = DALHelper.GetObjects<User>("GetUsers");
        }

        private void SetSemesterObjects()
        {
            SetVisibilities(2);

            Labels[0] = "Numar";
            Labels[1] = "An";

            AllEntities = DALHelper.GetObjects<Semester>("GetSemesters");
        }

        #endregion

        #region Add Methods

        private void AddPerson()
        {
            DAL.AddPerson(Texts[0], Texts[1]);

            MessageBox.Show($"S-a adaugat persoana: {Texts[0]} {Texts[1]}");
        }

        private void AddClass()
        {
            DAL.AddClass(int.Parse(Texts[0]), Texts[1]);

            MessageBox.Show($"S-a adaugat clasa: {Texts[0]} {Texts[1]}");
        }

        private void AddSubject()
        {
            DAL.AddSubject(Texts[0]);

            MessageBox.Show($"S-a adaugat materia: {Texts[0]}");
        }

        private void AddSpecilization()
        {
            DAL.AddSpecilization(Texts[0]);

            MessageBox.Show($"S-a adaugat specializarea: {Texts[0]}");
        }

        private void AddUser()
        {
            DAL.AddUser(Texts[0], Texts[1], Texts[2], CurrentAdditional.ID);

            MessageBox.Show($"S-a adaugat utilizatorul: {Texts[0]} {CurrentAdditional}");
        }

        private void AddSemester()
        {
            DAL.AddSemester(int.Parse(Texts[0]), int.Parse(Texts[1]));

            MessageBox.Show($"S-a adaugat semestul: {Texts[0]} {Texts[1]}");
        }

        private void Add(object o)
        {
            switch(CurrentEntity)
            {
                case "Persoana":
                    AddPerson();
                    break;
                case "Clasa":
                    AddClass();
                    break;
                case "Materie":
                    AddSubject();
                    break;
                case "Specializare":
                    AddSpecilization();
                    break;
                case "Utilizator":
                    AddUser();
                    break;
                case "Semestru":
                    AddSemester();
                    break;
            }

            SetWindowsObject(CurrentEntity);
        }

        #endregion

        #region Commands

        public ButtonCommand MainButtonCommand { get; init; }

        #endregion

        #region Bindings

        public string ButtonText
        {
            get => _buttonText;

            set
            {
                _buttonText = value;

                OnPropertyChanged();
            }
        }

        public string CurrentEntity
        {
            get => _currentEntity;

            set
            {
                _currentEntity = value;

                SetWindowsObject(value);

                OnPropertyChanged();
            }
        }

        public List<string> Entities => entities;

        public ObservableCollection<string> Texts { get; set; }

        public ObservableCollection<string> Labels { get; set; }

        public object AdditionalSource
        {
            get => _additionalSource;

            set
            {
                _additionalSource = value;

                OnPropertyChanged();
            }
        }

        public DatabaseObject CurrentAdditional
        {
            get => _currentAdditional;

            set
            {
                _currentAdditional = value;

                OnPropertyChanged();
            }
        }

        public object AllEntities
        {
            get => _allEntities;

            set
            {
                _allEntities = value;

                OnPropertyChanged();
            }
        }

        #endregion
    }
}
