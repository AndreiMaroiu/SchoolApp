using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using DatabaseWPF.Models.Helpers;
using DatabaseWPF.Views;

namespace DatabaseWPF.ViewModels
{
    class MainVM
    {
        private MainWindow window;
        private BitmapImage _backgroundImage;

        public MainVM(MainWindow main)
        {
            window = main;

            AdminClick = new ButtonCommand((object o) => window.Main.Content = new AdminPage(window));
            TeacherClick = new ButtonCommand((object o) => window.Main.Content = new TeacherPage(window));
            StudentClick = new ButtonCommand((object o) => window.Main.Content = new StudentPage(window));
            _backgroundImage = ImageHelper.GetImage("background.jpg");
        }

        #region Commands

        public ButtonCommand AdminClick { get; init; }

        public ButtonCommand TeacherClick { get; init; }

        public ButtonCommand StudentClick { get; init; }

        #endregion

        #region Bindings

        public BitmapImage BackgroundImage => _backgroundImage;

        #endregion
    }
}
