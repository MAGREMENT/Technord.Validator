using System;

namespace Validator.Validators
{
    public class NonEmptyAttribute : ValidatorAttribute
    {
        public string Error { private get; set; } = "Ne peut pas être vide";
    
        public override string Validate(object value)
        {
            switch (value)
            {
                case Array a : return a.Length > 0 ? Ok : Error;
                case string s : return s.Length > 0 ? Ok : Error;
                default : return WrongType;
            }
        }
    }
}

