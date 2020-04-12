using NUnit.Framework;
using System;
using TestNinja.Fundamentals;


namespace TestNinja.UnitTest
{
    [TestFixture]
    class ErrorLoggerTests
    {
        ErrorLogger _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = new ErrorLogger();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_ErrorIsNullOrEmpty_ThrowException(string errorMessage)
        {
            
            Assert.That( 
                () =>_logger.Log(errorMessage), 
                Throws.ArgumentNullException);
        }

        [Test]
        public void Log_ErrorIsNotEmpty_ThrowException()
        {
            _logger.Log("This is an error");

            Assert.That(_logger.LastError, Is.EqualTo("This is an error"));
        }


        [Test]
        public void Log_ValidError_ErrorLogEvent()
        {
            var id = Guid.Empty;
            _logger.ErrorLogged += (sender, args) => { id = args;  };
            _logger.Log("Error");

            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}
