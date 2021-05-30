using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    class SubjectSpecializationFull : Subject
    {
        public string Name { get; set; }

        public bool HaveThesis { get; set; }

        protected string ThesisString()
            => (HaveThesis) ? "Are teza" : "Nu are teza";

        public override string ToString()
            => $"{SubjectName.Trim()} {Name.Trim()} {ThesisString()}";
    }
}
