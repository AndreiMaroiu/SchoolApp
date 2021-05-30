using DatabaseWPF.Views;
using System.Windows.Controls;

namespace DatabaseWPF.MainFactories
{
    class AdminMainFactory : MainFactory
    {
        public AdminMainFactory(MainWindow window) : base(window) { }

        public override Page Create() => new AdminPage(window);
    }
}
