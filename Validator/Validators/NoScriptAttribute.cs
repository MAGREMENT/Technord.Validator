namespace Validator.Validators
{
    public class NoScriptAttribute : ValidatorAttribute
    {
        public string Error { private get; set; } = "Ne peut pas contenir de balise de script";
            
        public override string Validate(object value)
        {
            if (!(value is string s)) return "Invalide";

            return s.Contains("<script") ? Error : Ok ;
        }
    }
}

