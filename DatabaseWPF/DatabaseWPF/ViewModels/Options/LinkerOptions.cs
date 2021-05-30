using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.ViewModels.Options
{
    public class LinkerOptions : UpdateOptions
    {
        public bool HaveCheckBox { get; set; }

        public int NullChecks { get; set; }

        public int ComboCount { get; set; }
    }
}
