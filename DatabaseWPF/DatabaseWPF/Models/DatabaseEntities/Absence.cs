using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    class Absence : DatabaseObject
    {
        public DateTime Date { get; set; }

        public int Subject_ID { get; set; }

        public bool Motivated { get; set; }

        public int Student_ID { get; set; }

        public int Semester_ID { get; set; }

        public override string ToString()
         => $"{Date} {Motivated} {Subject_ID} {Student_ID}";
    }
}
