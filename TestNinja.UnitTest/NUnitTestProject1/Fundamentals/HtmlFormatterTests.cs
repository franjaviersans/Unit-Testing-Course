using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTest
{
    [TestFixture]
    class HtmlFormatterTests
    {

        [Test]
        public void FormatAsBold_WhenCalled_ShouldEnclosedStringWithBoldElements()
        {
            var htmlFormatter = new HtmlFormatter();

            var result = htmlFormatter.FormatAsBold("This is a test");

            // Specific
            Assert.That(result, Is.EqualTo("<strong>This is a test</strong>"));

            //General
            Assert.That(result, Does.StartWith("<strong>"));
            Assert.That(result, Does.EndWith("</strong>"));
            Assert.That(result, Does.Contain("This is a test"));

        }
    }
}
