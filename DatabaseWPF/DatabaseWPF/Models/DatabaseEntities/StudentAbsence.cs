using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    class StudentAbsence : AbsenceFull
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override string ToString()
            => $"{FirstName.Trim()} {LastName.Trim()} {base.ToString()}";
    }
}
