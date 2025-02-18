using System;

namespace Validator.Validators
{
    public class MaxLengthAttribute : ValidatorAttribute
    {
        public string Error { private get; set; } = "Trop long";

        private readonly int _max;

        public MaxLengthAttribute(int max)
        {
            _max = max;
        }

        public override string Validate(object value)
        {
            switch (value)
            {
                case Array a :
                    return a.Length <= _max ? Ok : Error;
                case string s :
                    return s.Length <= _max ? Ok : Error;
                default : return WrongType;
            }
        }
    }
}

