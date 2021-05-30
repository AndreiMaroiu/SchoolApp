using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    public class NoteSubject : SimpleNote
    {
        public NoteSubject() { }

        public string SubjectName { get; set; }

        public override string ToString()
            => $"{ThesisString()}: {Score} la Materia: {SubjectName.Trim()} in Data: {Date.ToString("dd/MM/yyyy")} Semestrul: {SemesterNumber} {SemesterYear}";
    }
}
