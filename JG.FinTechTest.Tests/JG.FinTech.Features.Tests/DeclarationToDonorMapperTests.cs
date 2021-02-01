namespace JG.FinTechTest.Tests.JG.FinTech.Features.Tests
{
    using global::JG.FinTech.Features;
    using global::JG.FinTech.Features.GiftAidCalculator;
    using global::JG.FinTech.Models;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;

    public class DeclarationToDonorMapperTests 
    {
        private Mock<IGiftAidCalculator> giftAidCalculatorMock;
        private DeclarationDetails declarationDetails;
        private DeclarationToDonorMapper declarationToDonorMapper;

        [SetUp]
        public void Setup()
        {
            giftAidCalculatorMock = new Mock<IGiftAidCalculator>();
            declarationToDonorMapper = new DeclarationToDonorMapper(giftAidCalculatorMock.Object);
        }

        [Test]
        public async Task Given_ValidDeclarationDetails_When_GetDonorDetailsAsyncCalled_Returns_DonorDetails()
        {
            //arrange
            declarationDetails = new DeclarationDetails()
            {
                DonationAmount = 1000d
            };
            giftAidCalculatorMock.Setup(o => o.CalculateGiftAidAsync(It.IsAny<GiftAid>())).ReturnsAsync(20d);

            //act
            var result = await this.declarationToDonorMapper.GetDonorDetailsAsync(declarationDetails).ConfigureAwait(false);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(20d, result.GiftAid);
            Assert.AreEqual(1000d, result.DonationAmount);
            Assert.IsNotEmpty(result.DonorID);
            Assert.IsNull(result.Name);
            Assert.IsNull(result.PostCode);
        }

        [Test]
        public void Given_InValidDeclarationDetails_When_GetDonorDetailsAsyncCalled_Throws_Exception()
        {
            //arrange
            declarationDetails = new DeclarationDetails()
            {
                DonationAmount = 1000000d
            };
            giftAidCalculatorMock.Setup(o => o.CalculateGiftAidAsync(It.IsAny<GiftAid>())).ThrowsAsync(new Exception("Invalid Denomination"));

            //act && assert
            Assert.ThrowsAsync<Exception>(async delegate { await declarationToDonorMapper.GetDonorDetailsAsync(declarationDetails).ConfigureAwait(false); });
        }

        [Test]
        public void Given_DefaultDeclarationDetails_When_GetDonorDetailsAsyncCalled_Throws_Exception()
        {
            //arrange
            giftAidCalculatorMock.Setup(o => o.CalculateGiftAidAsync(It.IsAny<GiftAid>())).ThrowsAsync(new Exception("Invalid Denomination"));

            //act && assert
            Assert.ThrowsAsync<ArgumentNullException>(async delegate { await declarationToDonorMapper.GetDonorDetailsAsync(default).ConfigureAwait(false); });
        }
    }
}
