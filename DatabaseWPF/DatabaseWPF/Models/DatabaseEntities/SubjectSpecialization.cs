using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    class SubjectSpecialization : DatabaseObject
    {
        public int Subject_ID { get; set; }

        public int Specialization_ID { get; set; }

        public bool HaveThesis { get; set; }

        protected string ThesisString()
            => (HaveThesis) ? "Are teza" : "Nu are teza";

        public override string ToString()
            => $"{Subject_ID} {Specialization_ID} {ThesisString()}";
    }
}
