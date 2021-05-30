using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    class ClassSubjectFull : Class
    {
        public string SubjectName { get; set; }

        public override string ToString()
            => $"{base.ToString()} {SubjectName.Trim()}";
    }
}
