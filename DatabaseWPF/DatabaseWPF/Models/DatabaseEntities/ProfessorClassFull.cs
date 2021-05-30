using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    class ProfessorClassFull : DatabaseObject
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Year { get; set; }

        public string YearChar { get; set; }

        public override string ToString()
            => $"{FirstName.Trim()} {LastName.Trim()} {Year} {YearChar}";
    }
}
