using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    class ClassSpec : DatabaseObject
    {
        public int Class_ID { get; set; }

        public int Specialization_ID { get; set; }
    }
}
