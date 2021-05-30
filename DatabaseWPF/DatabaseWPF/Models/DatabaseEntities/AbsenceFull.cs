using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    class AbsenceFull : SimpleAbsence
    {
        public string SubjectName { get; set; }

        public override string ToString()
            => $"Absenta {MotivatedString()} la {SubjectName.Trim()} " +
            $"in Data: {Date.ToString("dd/MM/yyyy")} Semestrul: {SemesterNumber} {SemesterYear}";
    }
}
