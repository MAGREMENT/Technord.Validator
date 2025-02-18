namespace Validator.Validators
{
    /// <summary>
    /// Validateur d'email suivant les règles de synthaxe proposées ici :
    ///     https://en.wikipedia.org/wiki/Email_address
    ///     https://datatracker.ietf.org/doc/html/rfc3696#section-3
    /// </summary>
    public class EmailAttribute : ValidatorAttribute
    {
        private const int MaxLocalPartSize = 64;
        private const int MaxDomainPartSize = 255;
        private const int MaxLabelSize = 63;

        public override string Validate(object value)
        {
            if (!(value is string s)) return "N'est pas un e-email";
            return IsValid(s);
        }

        public string IsValid(string email)
        {
            if (email.Length > MaxLocalPartSize + MaxDomainPartSize)
            {
                return "E-mail trop long";
            }

            var lastIndexOfAt = email.LastIndexOf('@');
            if (lastIndexOfAt == -1)
            {
                return "Il manque un '@'";
            }

            var localPart = email.Substring(0, lastIndexOfAt);
            var domainPart = email.Substring(lastIndexOfAt + 1);

            if (localPart.Length == 0 || localPart.Length > MaxLocalPartSize)
            {
                return "Taille de la partie locale de l'adresse e-mail non conforme";
            }

            if (domainPart.Length == 0 || domainPart.Length > MaxLocalPartSize)
            {
                return "Taille de la partie domain de l'adresse e-mail non conforme";
            }

            //Local part validation
            var isQuoted = false;

            for (int i = 0; i < localPart.Length; i++)
            {
                var c = localPart[i];
                if (IsLocalCharacter(c, isQuoted))
                {
                    continue;
                }

                switch (c)
                {
                    case '.' :
                        if (isQuoted)
                        {
                            continue;
                        }

                        if (i == 0 || i == localPart.Length - 1
                                   || (i > 0 && localPart[i - 1] == '.'))
                        {
                            return "Un point n'est pas autorisé ici";
                        }

                        break;
                    case '\\' :
                        if (!isQuoted)
                        {
                            return "Un backslash ne peut se trouver que entre guillemets";
                        }

                        if (i < localPart.Length - 1 && localPart[i + 1] != '\\' && localPart[i + 1] != '"'
                            && localPart[i + 1] != ' ' && localPart[i + 1] != '\t')
                        {
                            return "Un backslash doit être suivi d'un autre backslash, de guillemets, d'un espace ou d'une tabulation";
                        }

                        i++;
                        break;
                    case '"' :
                        if (isQuoted)
                        {
                            if (i != localPart.Length - 1 && localPart[i + 1] != '.')
                            {
                                return "Guillemets non authorisés ici";
                            }

                            isQuoted = false;
                        }
                        else
                        {
                            if (i != 0 && localPart[i - 1] != '.')
                            {
                                return "Guillemets non authorisés ici";
                            }

                            isQuoted = true;
                        }

                        break;
                    default: return $"Charactère invalide pour la partie local de l'addresse e-amil : {c}";
                }
            }

            if (isQuoted)
            {
                return "Guillements non fermés dans la partie locale";
            }

            //Domain part validation
            bool isIp;
            if (domainPart[0] == '[' && domainPart[domainPart.Length - 1] == ']')
            {
                isIp = true;
                domainPart = domainPart.Substring(1, domainPart.Length - 1);
            }
            else
            {
                isIp = false;
            }

            if (isIp)
            {
                if (domainPart.StartsWith("IPv6"))
                {
                    domainPart = domainPart.Substring(4);
                }

                foreach (var c in domainPart)
                {
                    if (!IsIpCharacter(c))
                    {
                        return $"Charactère invalide pour la partie domain de l'addresse e-amil : {c}";
                    }
                }
            }
            else
            {
                var labels = domainPart.Split('.');
                if (labels.Length == 0)
                {
                    return "Partie domain de l'addresse ne contient aucun label";
                }

                foreach (var label in labels)
                {
                    if (label.Length == 0)
                    {
                        return "Label vide";
                    }

                    if (label.Length > MaxLabelSize)
                    {
                        return $"Label trop long : {label}";
                    }

                    for (int i = 0; i < label.Length; i++)
                    {
                        var c = label[i];
                        if (!IsDomainCharacter(c) && (c != '-' || i == 0 || i == label.Length - 1))
                        {
                            return $"Charactère invalide pour la partie domain de l'addresse e-amil : {c}";
                        }
                    }
                }
            }

            return Ok;
        }

        private static bool IsDomainCharacter(char c) => (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' & c <='9');

        private static bool IsIpCharacter(char c) => (c >= '0' && c <= '9') || c == '.' || c == ':' || (c >= 'a' && c <= 'f');

        private static bool IsLocalCharacter(char c, bool isQuoted)
        {
            if (IsDomainCharacter(c))
            {
                return true;
            }

            switch (c)
            {
                case '!' :
                case '#' :
                case '$' :
                case '%' :
                case '&' :
                case '\'' :
                case '*' :
                case '+' :
                case '-' :
                case '/' :
                case '=' :
                case '?' :
                case '^' :
                case '_' :
                case '`' :
                case '{' :
                case '|' :
                case '}' :
                case '~' : return true;

                case ' ' :
                case '\t' :
                case '(' :
                case ')' :
                case ',' :
                case ';' :
                case ':' :
                case '<' :
                case '>' :
                case '@' :
                case '[' :
                case ']' : return isQuoted;
            }

            return false;
        }
    }
}


