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
        private static readonly Type[] types;
        private static readonly Dictionary<string, Type> typesDictionary;

        static Types()
        {
            types = Assembly.GetExecutingAssembly().GetTypes();

            typesDictionary = new();
            foreach(Type type in types)
            {
                typesDictionary[type.Name] = type;
            }
        }

        public static Type[] All => types;

        public static Type GetType(string name)
        {
            typesDictionary.TryGetValue(name, out var type);
            return type;
        }
    }
}
