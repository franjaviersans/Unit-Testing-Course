using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTest.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private EmployeeController employeeController;
        private Mock<IEmployeeService> employeeService;

        [SetUp]
        public void SetUp()
        {
            employeeService = new Mock<IEmployeeService>();
            employeeController = new EmployeeController(employeeService.Object);
        }

        [Test]
        public void DeleteEmployee_WhenCalled_ReturnsRedirectResult()
        {
            var result = employeeController.DeleteEmployee(1);
            Assert.That(result, Is.TypeOf<RedirectResult>());
        }

        [Test]
        public void DeleteEmployee_WhenCalled_DeletesEmployeeFromDB()
        {
            var result = employeeController.DeleteEmployee(1);

            employeeService.Verify(fr => fr.DeleteAndSave(1));
        }
    }
}
