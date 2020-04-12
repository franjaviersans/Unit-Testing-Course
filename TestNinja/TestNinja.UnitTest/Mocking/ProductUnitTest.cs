
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTest.Mocking
{
    [TestFixture]
    class ProductUnitTest
    {
        [Test]
        public void GetPrice_GoldCostumer_30PercentDiscoint(){

            Customer customer = new Customer() { IsGold =  true };
            var product = new Product () {  ListPrice = 100 };

            var resultPrice = product.GetPrice(customer);

            Assert.That(resultPrice, Is.EqualTo( 70 ));
        }

        [Test]
        public void GetPrice_NotGoldCostumer_NormalPrice()
        {

            Customer customer = new Customer() { IsGold = false};
            var product = new Product() { ListPrice = 100 };

            var resultPrice = product.GetPrice(customer);

            Assert.That(resultPrice, Is.EqualTo(100));
        }
    }
}
