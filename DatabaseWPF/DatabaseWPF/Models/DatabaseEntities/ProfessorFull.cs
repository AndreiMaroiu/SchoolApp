using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    public class ProfessorFull : Person
    {
        public string SubjectName { get; set; }

        public int? Class_ID { get; set; }

        private string GetProfType()
            => (Class_ID is not null) ? "Diriginte" : null;

        public override string ToString()
            => $"{base.ToString()} {SubjectName.Trim()} {GetProfType()}"; 
    }
}
