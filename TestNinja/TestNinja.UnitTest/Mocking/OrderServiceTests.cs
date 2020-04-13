using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTest.Mocking
{
    [TestFixture]
    class OrderServiceTests
    {
        [Test]
        public void PlaceOrder_WhenCalled_StoreObject()
        {
            var storage = new Mock<IStorage>();
            var orderService = new OrderService(storage.Object);

            var order = new Order();
            orderService.PlaceOrder(order);

            storage.Verify(ob => ob.Store(order));
        }
        
    }
}
