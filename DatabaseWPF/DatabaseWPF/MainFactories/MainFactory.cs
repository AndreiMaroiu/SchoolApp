using DatabaseWPF.Views;
using System.Windows.Controls;

namespace DatabaseWPF.MainFactories
{
    public abstract class MainFactory
    {
        protected MainWindow window;

        public MainFactory(MainWindow window)
        {
            this.window = window;
        }

        public abstract Page Create();
    }
}
