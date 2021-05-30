using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.DatabaseEntities
{
    class User : DatabaseObject
    {
        public User() { }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public int Person_ID { get; set; }

        public override string ToString()
            => $"{Username.Trim()} {Role.Trim()} {Person_ID}";
    }
}
