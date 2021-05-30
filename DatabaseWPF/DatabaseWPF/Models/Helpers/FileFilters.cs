using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.Helpers
{
    public static class FileFilters
    {
        public const string Custom = "PDF files (*.pdf)|*.pdf|Text files (*.txt)|*.txt|All files (*.*)|*.*";

        public const string All = "All files (*.*)|*.*";

        public static string GetFilter(string extension)
        {
            return $"{extension.Remove(0, 1)} files (*{extension})|(*{extension}|{All}";
        }
    }
}
