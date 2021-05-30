using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    public class Person : DatabaseObject
    {
        public Person() { }

        public Person(int id, string firstName, string lastName)
            => (ID, FirstName, LastName) = (id, firstName, lastName);

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public override string ToString()
            => $"{FirstName.Trim()} {LastName.Trim()}";
    }
}
