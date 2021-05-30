using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.ViewModels.Options
{
    public class UpdateOptions
    {
        public string Name { get; set; }

        public string ProcedureName { get; set; }

        public List<string> ProcedureParams { get; set; }

        public List<string> LabelNames { get; set; }
    }
}
