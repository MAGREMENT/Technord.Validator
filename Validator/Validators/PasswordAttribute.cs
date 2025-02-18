using System.Linq;
using InputValidator.Utils;

namespace Validator.Validators
{
    public class PasswordAttribute : ValidatorAttribute
    {
        private static readonly char[] _specialCharacters = { '!', '.', '?', '@', '#' }; //TODO add more ?
    
        public int MinLength { get; set; } = 8;
        public bool MustContainLowerCaseLetter { get; set; } = true;
        public bool MustContainUpperCaseLetter { get; set; } = true;
        public bool MustContainNumber { get; set; } = true;
        public bool MustContainSpecialCharacter { get; set; } = true;
    
        public override string Validate(object value)
        {
            if (!(value is string pass)) return "N'est pas un mot de passe";

            if (pass.Length < MinLength) return "Trop court";

            var ll = false;
            var ul = false;
            var n = false;
            var sc = false;

            foreach (var c in pass)
            {
                if (char.IsLetter(c))
                {
                    if (char.IsUpper(c)) ul = true;
                    else if (char.IsLower(c)) ll = true;
                }
                else if (char.IsNumber(c)) n = true;
                else if (_specialCharacters.Contains(c)) sc = true;
                else return "Charactère non authorisé : " + c;
            }

            if (!ll && MustContainLowerCaseLetter) return "Il manque une lettre minuscule";
            if (!ul && MustContainUpperCaseLetter) return "Il manque une lettre majuscule";
            if (!n && MustContainNumber) return "Il manque un chiffre";
            if (!sc && MustContainSpecialCharacter) return $"Il manque un charactère spéciale ({_specialCharacters.ToStringSequence()})";
        
            return Ok;
        }
    }
}

