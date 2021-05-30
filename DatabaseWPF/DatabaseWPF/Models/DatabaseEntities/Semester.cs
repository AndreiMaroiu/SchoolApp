using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    class Semester : DatabaseObject
    {
        public int SemesterNumber { get; set; }

        public int SemesterYear { get; set; }

        public override string ToString()
            => $"Semestul: {SemesterNumber} An: {SemesterYear}";
    }
}
