using DatabaseWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DatabaseWPF.Views
{
    /// <summary>
    /// Interaction logic for AdminAdder.xaml
    /// </summary>
    public partial class AdminAdder : Page
    {
        public AdminAdder(MainWindow window)
        {
            InitializeComponent();

            this.DataContext = new AdminAdderVM(window);
        }
    }
}
