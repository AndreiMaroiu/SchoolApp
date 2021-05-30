using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DatabaseWPF.Models.DataAccesLayer;
using DatabaseWPF.Models.DatabaseEntities;
using DatabaseWPF.Views;
using DatabaseWPF.Models.Helpers;

namespace DatabaseWPF.ViewModels
{
    public class AdminVM : BaseVM
    {
        #region Constants

        private static readonly List<string> _allTables = new List<string>() { "Persons", "Students", "Notes" };

        #endregion

        private List<object> _allData;
        private BitmapImage _addImage;

        public AdminVM(MainWindow window) : base(window)
        {
            OpenLinkerCommand = new ButtonCommand(OpenLinker);
            OpenAdderCommand = new ButtonCommand(OpenAdder);
            OpenEraserCommand = new ButtonCommand(OpenEraser);
            OpenUpdaterCommand = new ButtonCommand(OpenUpdater);

            BackgroundImage = ImageHelper.GetImage("adminBackground.jpg", 600);
            _addImage = ImageHelper.GetImage("add.png", 50);
        }

        #region Commands

        private void OpenLinker(object o)
        {
            _window.Main.Content = new AdminLinker(_window);
        }

        public ButtonCommand OpenLinkerCommand { get; init; }

        private void OpenAdder(object o)
        {
            _window.Main.Content = new AdminAdder(_window);
        }

        public ButtonCommand OpenAdderCommand { get; init; }

        private void OpenEraser(object o)
        {
            _window.Main.Content = new AdminEraser(_window);
        }

        public ButtonCommand OpenEraserCommand { get; init; }

        private void OpenUpdater(object o)
        {
            _window.Main.Content = new AdminUpdater(_window);
        }

        public ButtonCommand OpenUpdaterCommand { get; init; }

        #endregion

        #region Bindings

        public List<object> AllData
        {
            get => _allData;

            private set
            {
                _allData = value;

                OnPropertyChanged();
            }
        }

        public List<string> AllTables => _allTables;

        public BitmapImage AddImage => _addImage;

        #endregion
    }
}
