using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ValidationAttributes.Attributes;

namespace ValidationAttributes.Utilities
{
    public static class Validator
    {
        /// <summary>
        /// checks all object's properties for custom attributes
        /// and if all custom attributes are valid then the whole obj is valid
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsValid(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Type objType = obj.GetType();

            PropertyInfo[] properties = objType
                .GetProperties();

            //if all properties are valid with their custom
            //attributes -> object is valid
            //if one property is not valid for one of its custom
            //attributes -> object is not valid
            foreach (PropertyInfo property in properties)
            {
                MyValidatorAttribute[] attributes = property
                    .GetCustomAttributes()
                    .Where(ca => ca is MyValidatorAttribute)
                    .Cast<MyValidatorAttribute>()
                    .ToArray();

                foreach (MyValidatorAttribute attribute in attributes)
                {
                    if (!attribute.IsValid(property.GetValue(obj)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
