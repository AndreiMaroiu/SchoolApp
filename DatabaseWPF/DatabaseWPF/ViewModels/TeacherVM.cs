using DatabaseWPF.Models.DataAccesLayer;
using DatabaseWPF.Models.DatabaseEntities;
using DatabaseWPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.ViewModels
{
    class TeacherVM : BaseVM
    {
        public TeacherVM(MainWindow window) : base (window)
        {
            WelcomeLabel = $"Bine ai venit\n{person}";

            Professor prof = DALHelper.GetObject<Professor>("GetProfessorByPerson", ("@personID", window.Person_ID));

            if(prof is null)
            {
                prof = DALHelper.GetObject<Professor>("GetProfessor", ("@id", 1));
            }

            OpenNotesCommand = new ButtonCommand((object o) => _window.Main.Content = new TeacherNotes(_window));
            OpenAbsencesCommand = new ButtonCommand((object o) => _window.Main.Content = new TeacherAbsences(_window));
            OpenStatsCommand = new ButtonCommand((object o) => _window.Main.Content = new TeacherStats(_window));
            TestCommand = new ButtonCommand((object o) => _window.Main.Content = new TeacherCourse(_window));

            if(prof.Class_ID is not null)
            {
                StatsVis = true;
            }
        }

        public ButtonCommand OpenNotesCommand { get; init; }

        public ButtonCommand OpenAbsencesCommand { get; init; }

        public ButtonCommand OpenStatsCommand { get; init; }

        public ButtonCommand TestCommand { get; init; }

        public bool StatsVis { get; init; } = false;
    }
}
