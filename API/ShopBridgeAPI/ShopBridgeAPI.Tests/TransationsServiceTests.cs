using Moq;
using NUnit.Framework;
using ShopBridgeBussiness;
using ShopBridgeModel;

namespace ShopBridgeAPI.Tests
{
    [TestFixture]
    public class TransationsServiceTests
    {
        private MockRepository mockRepository;



        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private TransationsService CreateService()
        {
            return new TransationsService();
        }

        [Test]
        public void PostInventoryDetails_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Insert_Inventory_Request request = null;

            // Act
            var result = service.PostInventoryDetails(
                request);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }
    }
}
