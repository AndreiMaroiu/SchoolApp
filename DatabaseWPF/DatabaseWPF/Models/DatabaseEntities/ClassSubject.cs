using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    public class ClassSubject : DatabaseObject
    {
        public int Class_ID { get; set; }

        public int Subject_ID { get; set; }

        public override string ToString()
            => $"{ID} {Class_ID} {Subject_ID}";
    }
}
