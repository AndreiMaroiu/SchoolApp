using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    class ProfessorClass : DatabaseObject
    {
        public int Professor_ID { get; set; }

        public int Class_ID { get; set; }
    }
}
