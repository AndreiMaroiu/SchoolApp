using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    public class SimpleNote : DatabaseObject
    {
        public SimpleNote() { }

        public int Score { get; set; }

        public DateTime Date { get; set; }

        public bool IsThesis { get; set; }

        public int SemesterNumber { get; set; }

        public int SemesterYear { get; set; }

        protected string ThesisString()
            => (IsThesis) ? "Teza" : "Nota";

        public override string ToString()
            => $"{ThesisString()}: {Score} in Data: {Date.ToString("dd/MM/yyyy")} Semestrul: {SemesterNumber} {SemesterYear}";
    }
}
