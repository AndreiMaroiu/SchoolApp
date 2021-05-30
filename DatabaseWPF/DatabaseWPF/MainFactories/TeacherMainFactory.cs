using DatabaseWPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DatabaseWPF.MainFactories
{
    class TeacherMainFactory : MainFactory
    {
        public TeacherMainFactory(MainWindow window) : base(window)
        {

        }

        public override Page Create() => new TeacherPage(window);
    }
}
