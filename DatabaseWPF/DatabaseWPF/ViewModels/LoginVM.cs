using DatabaseWPF.MainFactories;
using DatabaseWPF.Models.DataAccesLayer;
using DatabaseWPF.Models.DatabaseEntities;
using DatabaseWPF.Models.Helpers;
using DatabaseWPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseWPF.ViewModels
{
    class LoginVM : BaseVM
    {
        private LoginPage page;

        public LoginVM(MainWindow window, LoginPage page) : base(window)
        {
            this.page = page;

            LoginCommand = new ButtonCommand(Login);
            BackgroundImage = ImageHelper.GetImage("background.jpg");
        }

        private void ChangePage(User user)
        {
            switch (user.Role.Trim())
            {
                case "Student":
                    _window.Factory = new StudentMainFactory(_window);
                    break;
                case "Admin":
                    _window.Factory = new AdminMainFactory(_window);
                    break;
                case "Professor":
                    _window.Factory = new TeacherMainFactory(_window);
                    break;
            }

            _window.Person_ID = user.Person_ID;
            _window.Main.Content = _window.Factory.Create();
        }

        public void Login(object o)
        {
            string username = page.usernameText.Text;
            string password = page.passwordText.Password;
            var user = DALHelper.GetObject<User>("GetUser", ("@username", username), ("@password", password));

            if (user is not null)
            {
                ChangePage(user);
            }
            else
            {
                MessageBox.Show($"Nume sau parola gresita");
            }
        }

        public ButtonCommand LoginCommand { get; init; }
    }
}
