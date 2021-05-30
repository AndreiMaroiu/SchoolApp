using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    public class Specialization : DatabaseObject
    {
        public Specialization() { }

        public string Name { get; set; }

        public override string ToString()
            => $"{Name}";
    }
}
