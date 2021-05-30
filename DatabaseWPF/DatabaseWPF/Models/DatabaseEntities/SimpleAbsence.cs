using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    class SimpleAbsence : DatabaseObject
    {
        public DateTime Date { get; set; }

        public bool Motivated { get; set; }

        public int SemesterNumber { get; set; }

        public int SemesterYear { get; set; }

        protected string MotivatedString()
            => (Motivated) ? "motivata" : "nemotivata";

        public override string ToString()
            => $"Absenta {MotivatedString()} in Data: {Date.ToString("dd/MM/yyyy")} " +
            $"Semestrul: {SemesterNumber} {SemesterYear}";
    }
}
