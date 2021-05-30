using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    class Note : DatabaseObject
    {
        public int Score { get; set; }

        public int Subject_ID { get; set; }

        public int Student_ID { get; set; }

        public DateTime Date { get; set; }

        public bool IsThesis { get; set; }

        public int Semester_ID { get; set; }

        public override string ToString()
            => $"Nota: {Score} in Data: {Date.ToString("DD/MM/YYYY")}";
    }
}
