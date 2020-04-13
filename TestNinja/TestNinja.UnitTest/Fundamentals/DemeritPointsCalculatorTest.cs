using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTest.Fundamentlas
{
    [TestFixture]
    class DemeritPointsCalculatorTest
    {
        DemeritPointsCalculator _demeritPC;

        [SetUp]
        public void SetUp()
        {    
            _demeritPC = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(350)]
        [TestCase(-1)]
        public void CalculateDemeritPOints_NotValidRange_ThrowsException(int speed)
        {
            Assert.That(() =>
            {
                _demeritPC.CalculateDemeritPoints(speed);
            }, Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void CalculateDemeritPOints_LessThanSpeedLimit_ReturnsZero()
        {
            var result = _demeritPC.CalculateDemeritPoints(50);
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateDemeritPOints_SpeedValid_ReturnsDemeritPoints()
        {
            var result = _demeritPC.CalculateDemeritPoints(90);
            Assert.That(result, Is.EqualTo(5));
        }
    }
}
