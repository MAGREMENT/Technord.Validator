using System.Text.RegularExpressions;

namespace Validator.Validators
{
    public class RegexAttribute : ValidatorAttribute
    {
        public string Error { private get; set; } = "Format non valide";

        private readonly string _pattern;

        public RegexAttribute(string pattern)
        {
            _pattern = pattern;
        }

        public override string Validate(object value)
        {
            if (!(value is string s)) return WrongType;

            return Regex.IsMatch(s, _pattern) ? Ok : Error;
        }
    }
}

