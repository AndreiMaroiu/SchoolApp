using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DatabaseWPF.Models.DataAccesLayer;
using System.Diagnostics;
using DatabaseWPF;
using DatabaseWPF.ViewModels;

namespace DatabaseWPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainFactories.MainFactory Factory { get; set; }

        public int Person_ID { get; set; } = 1;

        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new LoginPage(this);
            //Main.Content = new MainPage(this);
            Factory = new MainFactories.SimpleMainFactory(this);
        }
    }
}
