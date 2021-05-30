using System;
using System.Collections.Generic;
using System.Reflection;

namespace DatabaseWPF.Models.DataAccesLayer
{
    public static class ClassSetter
    {
        public static void SetProperty(object obj, PropertyInfo property, object value)
        {
            if (value.Equals(DBNull.Value))
            {
                property.SetValue(obj, null);
            }
            else
            {
                property.SetValue(obj, value);
            }
        }
    }
}
