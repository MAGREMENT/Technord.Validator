namespace Validator.Validators
{
    public class RangeAttribute : ValidatorAttribute
    {
        public string Error { private get; set; } = "Doit être compris entre {0} et {1}";

        private readonly int _min;
        private readonly int _max;

        public RangeAttribute(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public override string Validate(object value)
        {
            var error = string.Format(Error, _min, _max);
            switch (value)
            {
                case int n : return n >= _min && n <= _max ? Ok : error;
                case double d : return d >= _min && d <= _max ? Ok : error;
                case long l : return l >= _min && l <= _max ? Ok : error;
                case float f : return f >= _min && f <= _max ? Ok : error;
                default : return WrongType;
            }
        }
    }
}

