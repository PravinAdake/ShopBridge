using Moq;
using NUnit.Framework;
using ShopBridgeBussiness;
using System;

namespace ShopBridgeAPI.Tests
{
    [TestFixture]
    public class MastersServiceTests
    {
        private MockRepository mockRepository;



        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private MastersService CreateService()
        {
            return new MastersService();
        }

        [Test]
        public void GetInventoryDetails_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string reqtype = "Get_category_details";

            // Act
            var result = service.GetInventoryDetails(
                reqtype);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }
    }
}
