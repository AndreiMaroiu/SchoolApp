﻿using DatabaseWPF.ViewModels;
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
    /// Interaction logic for AdminUpdater.xaml
    /// </summary>
    public partial class AdminUpdater : Page
    {
        public AdminUpdater(MainWindow window)
        {
            InitializeComponent();
            this.DataContext = new AdminUpdaterVM(window);
        }
    }
}
