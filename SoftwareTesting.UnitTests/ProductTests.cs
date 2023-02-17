using Moq;
using SoftwareTesting.Fundamentals;
using SoftwareTesting.Mocking;
using static SoftwareTesting.Mocking.Product;

namespace SoftwareTesting.UnitTests
{
    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void GetPrice_GoldCustomer_Apply30PercentDiscount()
        {
            var product = new Product { ListPrice = 100 };
            
            var result = product.GetPrice(new Customer { IsGold = true });

            Assert.That(result, Is.EqualTo(70));
        }
    }
}

