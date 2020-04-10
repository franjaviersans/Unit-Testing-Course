using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTest
{
    public class MathTests
    {
        private Fundamentals.Math _math;
        [TestInitialize]
        public void SetUp()
        {
            _math = new Fundamentals.Math();
        }

        [TestMethod]
        public void Add_AddTwoNumbers_Total()
        {
            

            var result = _math.Add(1, 2);

            Assert.AreEqual(result, 3);
        }

        [TestMethod]
        public void Max_FirstGraterThanSecond_FirstValue()
        {
            var a = 2;
            var b = 1;

            var result = _math.Max(a, b);

            Assert.AreEqual(result, a);
        }

        [TestMethod]
        public void Max_SecondGraterThanFirst_SecondValue()
        {
            var a = 1;
            var b = 2;

            var result = _math.Max(a, b);

            Assert.AreEqual(result, b);
        }

        [TestMethod]
        public void Max_ArgumentsAreEqual_ReturnAnyArgument()
        {
            var a = 1;
            var b = 1;

            var result = _math.Max(a, b);

            Assert.AreEqual(result, b);
        }
    }
}
