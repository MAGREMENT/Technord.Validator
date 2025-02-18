
using NUnit.Framework;
using Validator.Validators;

namespace Tests
{
    [TestFixture]
    public class EmailAttributeTests
    {
        /// <summary>
        /// https://en.wikipedia.org/wiki/Email_address#cite_note-rfc5321-8
        /// </summary>
        [Test]
        public void WikipediaEmailBatchTest()
        {
            var attribute = new EmailAttribute();
            Assert.That(attribute.IsValid("simple@example.com"), Is.Null);
            Assert.That(attribute.IsValid("very.common@example.com"), Is.Null);
            Assert.That(attribute.IsValid("FirstName.LastName@EasierReading.org"), Is.Null);
            Assert.That(attribute.IsValid("x@example.com"), Is.Null);
            Assert.That(attribute.IsValid("long.email-address-with-hyphens@and.subdomains.example.com"), Is.Null);
            Assert.That(attribute.IsValid("user.name+tag+sorting@example.com"), Is.Null);
            Assert.That(attribute.IsValid("name/surname@example.com"), Is.Null);
            Assert.That(attribute.IsValid("admin@example"), Is.Null);
            Assert.That(attribute.IsValid("\" \"@example.org"), Is.Null);
            Assert.That(attribute.IsValid("\"john..doe\"@example.org"), Is.Null);
            Assert.That(attribute.IsValid("mailhost!username@example.org"), Is.Null);
            Assert.That(attribute.IsValid("\"very.(),:;<>[]\\\".VERY.\\\"very@\\\\ \\\"very\\\".unusual\"@strange.example.com"), Is.Null);
            Assert.That(attribute.IsValid("user%example.com@example.org"), Is.Null);
            Assert.That(attribute.IsValid("user-@example.org"), Is.Null);
            Assert.That(attribute.IsValid("postmaster@[123.123.123.123]"), Is.Null);
            Assert.That(attribute.IsValid("postmaster@[IPv6:2001:0db8:85a3:0000:0000:8a2e:0370:7334]"), Is.Null);
            Assert.That(attribute.IsValid("_test@[IPv6:2001:0db8:85a3:0000:0000:8a2e:0370:7334]"), Is.Null);

            Assert.That(attribute.IsValid("abc.example.com"), Is.Not.Null);
            Assert.That(attribute.IsValid("a@b@c@example.com"), Is.Not.Null);
            Assert.That(attribute.IsValid("a\"b(c)d,e:f;g<h>i[j\\k]l@example.com"), Is.Not.Null);
            Assert.That(attribute.IsValid("just\"not\"right@example.com"), Is.Not.Null);
            Assert.That(attribute.IsValid("this is\"not\\allowed@example.com"), Is.Not.Null);
            Assert.That(attribute.IsValid("this\\ still\"not\\\\allowed@example.com"), Is.Not.Null);
            Assert.That(attribute.IsValid("1234567890123456789012345678901234567890123456789012345678901234+x@example.com"), Is.Not.Null);
            Assert.That(attribute.IsValid("i.like.underscores@but_they_are_not_allowed_in_this_part"), Is.Not.Null);
        }

        /// <summary>
        /// https://gist.github.com/cjaoude/fd9910626629b53c4d25
        /// </summary>
        [Test]
        public void GithubEmailBatchTest()
        {
            var attribute = new EmailAttribute();
            Assert.That(attribute.IsValid("email@example.com"), Is.Null);
            Assert.That(attribute.IsValid("firstname.lastname@example.com"), Is.Null);
            Assert.That(attribute.IsValid("email@subdomain.example.com"), Is.Null);
            Assert.That(attribute.IsValid("firstname+lastname@example.com"), Is.Null);
            Assert.That(attribute.IsValid("email@123.123.123.123"), Is.Null);
            Assert.That(attribute.IsValid("email@[123.123.123.123]"), Is.Null);
            Assert.That(attribute.IsValid("\"email\"@example.com"), Is.Null);
            Assert.That(attribute.IsValid("1234567890@example.com"), Is.Null);
            Assert.That(attribute.IsValid("email@example-one.com"), Is.Null);
            Assert.That(attribute.IsValid("_______@example.com"), Is.Null);
            Assert.That(attribute.IsValid("email@example.name"), Is.Null);
            Assert.That(attribute.IsValid("email@example.museum"), Is.Null);
            Assert.That(attribute.IsValid("email@example.co.jp"), Is.Null);
            Assert.That(attribute.IsValid("firstname-lastname@example.com"), Is.Null);
            Assert.That(attribute.IsValid("much.\"more\\ unusual\"@example.com"), Is.Null);
            Assert.That(attribute.IsValid("very.unusual.\"@\".unusual.com@example.com"), Is.Null);
            Assert.That(attribute.IsValid("very.\"(),:;<>[]\".VERY.\"very@\\\"very\".unusual@strange.example.com"), Is.Null);

            Assert.That(attribute.IsValid("plainaddress"), Is.Not.Null);
            Assert.That(attribute.IsValid("#@%^%#$@#$@#.com"), Is.Not.Null);
            Assert.That(attribute.IsValid("@example.com"), Is.Not.Null);
            Assert.That(attribute.IsValid("Joe Smith <email@example.com>"), Is.Not.Null);
            Assert.That(attribute.IsValid("email.example.com"), Is.Not.Null);
            Assert.That(attribute.IsValid("email@example@example.com"), Is.Not.Null);
            Assert.That(attribute.IsValid(".email@example.com"), Is.Not.Null);
            Assert.That(attribute.IsValid("email.@example.com"), Is.Not.Null);
            Assert.That(attribute.IsValid("email..email@example.com"), Is.Not.Null);
            Assert.That(attribute.IsValid("あいうえお@example.com"), Is.Not.Null);
            Assert.That(attribute.IsValid("email@example.com (Joe Smith)"), Is.Not.Null);
            Assert.That(attribute.IsValid("email@-example.com"), Is.Not.Null);
            Assert.That(attribute.IsValid("email@example..com"), Is.Not.Null);
            Assert.That(attribute.IsValid("\"(),:;<>[\\]@example.com"), Is.Not.Null);
            Assert.That(attribute.IsValid("just\"not\"right@example.com"), Is.Not.Null);
            Assert.That(attribute.IsValid("this\\ is\"really\"not\\allowed@example.com"), Is.Not.Null);
        }
    }
}