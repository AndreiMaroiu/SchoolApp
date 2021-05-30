using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    public class Subject : DatabaseObject
    {
        public Subject()
        {

        }

        public string SubjectName { get; set; }

        public override string ToString()
            => $"{SubjectName.Trim()}";
    }
}
