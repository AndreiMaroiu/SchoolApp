using DatabaseWPF.Models.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.ViewModels.ForBindings
{
    public class Counter : DatabaseObject
    {
        public Counter(int count = 0)
            => Count = count;

        public int Count { get; set; }

        public static implicit operator string (Counter counter) => counter.ToString();

        public override string ToString()
            => $"Total: {Count}";
    }
}
