namespace Validator.Validators
{
    public class PositiveAttribute : ValidatorAttribute
    {
        public string Error { private get; set; } = "Doit être positif";

        public bool CanBeNull { get; set; }
    
        public override string Validate(object value)
        {
            if (CanBeNull && value is null) return Ok;

            switch (value)
            {
                case int n : return n >= 0 ? Ok : Error;
                case double d : return d >= 0 ? Ok : Error;
                case long l : return l >= 0 ? Ok : Error;
                case float f :
                    return f >= 0 ? Ok : Error;
                default: return WrongType;
            }
        }
    }
}

