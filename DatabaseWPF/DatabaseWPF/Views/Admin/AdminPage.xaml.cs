using DatabaseWPF.ViewModels;
using System.Windows.Controls;

namespace DatabaseWPF.Views
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage(MainWindow window)
        {
            InitializeComponent();
            this.DataContext = new AdminVM(window);
        }
    }
}
