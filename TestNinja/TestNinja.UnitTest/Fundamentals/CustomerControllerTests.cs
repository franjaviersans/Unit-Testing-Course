using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTest
{
    [TestFixture]
    class CustomerControllerTests
    {
        CustomerController _customerController;
        [SetUp]
        public void SetUp()
        {
            _customerController = new CustomerController();
        }

        [Test]
        public void GetCustomer_WhenIdZero_ReturnNotFound()
        {
            var result = _customerController.GetCustomer(0);

            Assert.That(result, Is.TypeOf<NotFound>());
            Assert.That(result, Is.InstanceOf<NotFound>());
            Assert.That(result, Is.InstanceOf<ActionResult>());
        }

        [Test]
        public void GetCustomer_WhenIdNotZero_ReturnOk()
        {
            var result = _customerController.GetCustomer(10);

            Assert.That(result, Is.TypeOf<Ok>());
        }
    }
}
