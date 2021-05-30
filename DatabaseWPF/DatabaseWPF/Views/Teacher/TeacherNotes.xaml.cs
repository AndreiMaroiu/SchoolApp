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
    /// Interaction logic for TeacherNotes.xaml
    /// </summary>
    public partial class TeacherNotes : Page
    {
        public TeacherNotes(MainWindow window)
        {
            InitializeComponent();
            this.DataContext = new TeacherNotesVM(window);
        }
    }
}
