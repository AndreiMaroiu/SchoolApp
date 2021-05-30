using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.Helpers
{
    class Types
    {
        private static readonly Type[] types = Assembly.GetExecutingAssembly().GetTypes();

        public static Type[] All => types;

        public static Type GetType(string name)
        {
            foreach(var type in types)
            {
                if(type.Name == name)
                {
                    return type;
                }
            }

            return null;
        }
    }
}
