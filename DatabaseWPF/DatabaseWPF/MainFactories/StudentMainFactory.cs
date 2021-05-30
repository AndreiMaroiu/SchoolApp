using DatabaseWPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DatabaseWPF.MainFactories
{
    class StudentMainFactory : MainFactory
    {
        public StudentMainFactory(MainWindow window) : base(window)
        {

        }
        public override Page Create() => new StudentPage(window);

    }
}
