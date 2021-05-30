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
using DatabaseWPF.ViewModels;

namespace DatabaseWPF.Views
{
    /// <summary>
    /// Interaction logic for TeacherStats.xaml
    /// </summary>
    public partial class TeacherStats : Page
    {
        public TeacherStats(MainWindow window)
        {
            InitializeComponent();
            this.DataContext = new TeacherStatsVM(window);
        }
    }
}
