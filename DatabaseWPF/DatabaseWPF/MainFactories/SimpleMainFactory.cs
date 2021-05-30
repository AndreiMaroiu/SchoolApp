

using DatabaseWPF.Views;
using System.Windows.Controls;

namespace DatabaseWPF.MainFactories
{
    class SimpleMainFactory : MainFactory
    {
        public SimpleMainFactory(MainWindow window) : base(window) {}

        public override Page Create() => new MainPage(window);
    }
}
