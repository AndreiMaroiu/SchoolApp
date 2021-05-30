using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DatabaseWPF.Models.Helpers
{
    public static class SettingsHelper
    {
        private static readonly int maxAbsences = int.Parse(ConfigurationManager.AppSettings["maxAbsences"]);

        public static int MaxAbsences => maxAbsences;
    }
}
