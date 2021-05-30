using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    public class Class : DatabaseObject
    {
        public Class()
        {

        }

        public Class(int ID, int year, string yearChar)
            => (this.ID, Year, YearChar) = (ID, year, yearChar);

        public int Year { get; set; }

        public string YearChar { get; set; }

        public override string ToString()
            => $"{Year}{YearChar}";
    }
}
