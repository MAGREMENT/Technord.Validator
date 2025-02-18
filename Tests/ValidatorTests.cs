using NUnit.Framework;
using Validator.Validators;
using Validator;
using RangeAttribute = Validator.Validators.RangeAttribute;

namespace Tests
{
    [TestFixture]
    public class ValidatorTests
    {
        [Test]
        public void TestProperties()
        {
            var val = new TestInput(-1, "");
            Assert.That(Validate.Properties(val), Has.Count.EqualTo(2));
        
            val = new TestInput(1, "");
            Assert.That(Validate.Properties(val), Has.Count.EqualTo(1));
        
            val = new TestInput(-1, "Oui");
            Assert.That(Validate.Properties(val), Has.Count.EqualTo(1));
        
            val = new TestInput(0, "a");
            Assert.That(Validate.Properties(val), Has.Count.EqualTo(0));
        }

        [Test]
        public void TestObject()
        {
            const int n = -3;
            var result = Validate.Object(n, "nombre", new PositiveAttribute(), new RangeAttribute(3, 7));
        
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("nombre"));
        }

    }
    public class TestInput
    {
        public TestInput(int number, string @string)
        {
            Number = number;
            String = @string;
        }


        [Positive] 
        public int Number { get; }

        [NonEmpty] 
        public string String { get; }
    }

    
}