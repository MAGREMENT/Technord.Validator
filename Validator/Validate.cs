using System;
using System.Collections.Generic;

namespace Validator
{
    public static class Validate
    {
        public static IList<ValidationError> Properties<T>(T toValidate)
        {
            var result = new List<ValidationError>();
            var type = typeof(T);
            var props = type.GetProperties();

            foreach (var prop in props)
            {
                var value = prop.GetValue(toValidate, new object[0]);
                foreach (var attribute in Attribute.GetCustomAttributes(prop))
                {
                    if (!(attribute is ValidatorAttribute validator)) continue;

                    var s = validator.Validate(value);
                    if(s != null) result.Add(new ValidationError(prop.Name, s));
                }
            }

            return result;
        }

        public static IList<ValidationError> Object(object o, string name, params ValidatorAttribute[] validators)
        {
            var result = new List<ValidationError>();
            foreach (var validator in validators)
            {
                var s = validator.Validate(o);
                if(s != null) result.Add(new ValidationError(name, s));
            }

            return result;
        }
    }

    public class ValidationError
    {
        public ValidationError(string name, string message)
        {
            Name = name;
            Message = message;
        }

        public string Name { get; set; }
        public string Message { get; set; }
    }

    [AttributeUsage(AttributeTargets.All)]
    public abstract class ValidatorAttribute : Attribute
    {
        protected const string Ok = null;
        protected const string WrongType = "Type non valable";
    
        public abstract string Validate(object value);
    }
}

