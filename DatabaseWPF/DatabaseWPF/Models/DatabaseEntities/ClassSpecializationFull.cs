using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    class ClassSpecializationFull : Class
    {
        public string Name { get; set; }

        public override string ToString()
            => $"{base.ToString()} {Name}";
    }
}
