using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    public class Student : DatabaseObject
    {
        public Student()
        {

        }

        public int Person_ID { get; set; }

        public int Class_ID { get; set; }

        public override string ToString()
            => $"Student: {Person_ID} {Class_ID}";
    }
}
