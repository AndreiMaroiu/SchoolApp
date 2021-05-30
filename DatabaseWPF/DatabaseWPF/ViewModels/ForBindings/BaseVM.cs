using DatabaseWPF.Models.DataAccesLayer;
using DatabaseWPF.Models.DatabaseEntities;
using DatabaseWPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DatabaseWPF.ViewModels
{
    public class BaseVM : NotifyPropertyChanged
    {
        protected MainWindow _window;
        protected Person person;

        protected BaseVM(MainWindow window)
        {
            _window = window;

            ExitCommand = new ButtonCommand((object o) => _window.Close());
            MainMenuCommand = new ButtonCommand((object o) => _window.Main.Content = _window.Factory.Create());
            LoginCommand = new ButtonCommand((object o) => _window.Main.Content = new LoginPage(_window));

            person = DALHelper.GetObject<Person>("GetPersonByID", ("@id", window.Person_ID));

            MenuLabel = person.ToString();
            WelcomeLabel = $"Bine ai venit {person}";
        }

        protected void InitVisibilities(int number)
        {
            for(int i = 0; i < number; ++i)
            {
                Visibilities.Add(false);
            }
        }

        protected void SetVisibilities(int number)
        {
            for (int i = 0; i < number; ++i)
            {
                Visibilities[i] = true;
            }
            for (int i = number; i < Visibilities.Count; ++i)
            {
                Visibilities[i] = false;
            }
        }

        public ObservableCollection<bool> Visibilities { get; set; } = new();

        public ObservableCollection<object> CurrentValues { get; set; } = new();

        public ObservableCollection<object> Values { get; set; } = new();

        public ButtonCommand ExitCommand { get; init; }

        public ButtonCommand MainMenuCommand { get; init; }

        public ButtonCommand LoginCommand { get; init; }

        public BitmapImage BackgroundImage { get; set; }

        public string MenuLabel { get; set; }

        public string WelcomeLabel { get; set; }
    }
}
