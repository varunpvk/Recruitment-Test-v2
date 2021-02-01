namespace JG.FinTechTest.Tests.JG.FinTechTest.Controller.Tests
{
    using global::JG.FinTech.Domain;
    using global::JG.FinTech.Features;
    using global::JG.FinTech.Features.GiftAidCalculator;
    using global::JG.FinTech.Models;
    using global::JG.FinTechTest.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using NUnit.Framework;
    using System.Net;
    using System.Threading.Tasks;

    public class GiftAidControllerTests
    {
        private Mock<IGiftAidCalculator> giftAidCalculatorMock;
        private Mock<IConfiguration> configMock;
        private Mock<IGiftAidRepository> giftAidRepositoryMock;
        private Mock<IDeclarationToDonorMapper> declarationToDonorMapperMock;
        private GiftAidController giftAidController;

        [SetUp]
        public void Setup()
        {
            giftAidCalculatorMock = new Mock<IGiftAidCalculator>();
            configMock = new Mock<IConfiguration>();
            giftAidRepositoryMock = new Mock<IGiftAidRepository>();
            declarationToDonorMapperMock = new Mock<IDeclarationToDonorMapper>();
            giftAidController = new GiftAidController(giftAidCalculatorMock.Object, giftAidRepositoryMock.Object, declarationToDonorMapperMock.Object, configMock.Object);
        }

        [Test]
        public async Task Given_ValidAmount_When_GetGiftAidAmount_Returns_Valid_GiftAidResponse()
        {
            //arrange
            giftAidCalculatorMock.Setup(o => o.CalculateGiftAidAsync(It.IsAny<GiftAid>())).ReturnsAsync(127.5d);

            //act
            var result = await this.giftAidController.GetGiftAidAmount(1000d).ConfigureAwait(false);
            var giftAidResponse = result.Value;

            //assert
            Assert.IsNotNull(giftAidResponse);
            Assert.AreEqual(127.5d, giftAidResponse.GiftAidAmount);
            Assert.AreEqual(1000d, giftAidResponse.DonationAmount);
        }

        [Test]
        public async Task Given_MinimumAmount_When_GetGiftAidAmount_Returns_Valid_GiftAidResponse()
        {
            //arrange
            giftAidCalculatorMock.Setup(o => o.CalculateGiftAidAsync(It.IsAny<GiftAid>())).ReturnsAsync(5d);

            //act
            var result = await this.giftAidController.GetGiftAidAmount(20d).ConfigureAwait(false);
            var giftAidResponse = result.Value;

            //assert
            Assert.IsNotNull(giftAidResponse);
            Assert.AreEqual(5d, giftAidResponse.GiftAidAmount);
            Assert.AreEqual(20d, giftAidResponse.DonationAmount);
        }

        [Test]
        public async Task Given_MaximumAmount_When_GetGiftAidAmount_Returns_Valid_GiftAidResponse()
        {
            //arrange
            giftAidCalculatorMock.Setup(o => o.CalculateGiftAidAsync(It.IsAny<GiftAid>())).ReturnsAsync(5d);

            //act
            var result = await this.giftAidController.GetGiftAidAmount(100000.00d).ConfigureAwait(false);
            var giftAidResponse = result.Value;

            //assert
            Assert.IsNotNull(giftAidResponse);
            Assert.AreEqual(5d, giftAidResponse.GiftAidAmount);
            Assert.AreEqual(100000.00d, giftAidResponse.DonationAmount);
        }

        [Test]
        public async Task Given_Amount_LesserThan_MinimumAmount_When_GetGiftAidAmount_Returns_Valid_GiftAidResponse()
        {
            //arrange
            giftAidCalculatorMock.Setup(o => o.CalculateGiftAidAsync(It.IsAny<GiftAid>())).Throws(new System.Exception("Invalid Donation Value"));

            //act
            var result = await this.giftAidController.GetGiftAidAmount(19.9999d).ConfigureAwait(false);
            var giftAidResponse = result.Value;

            //assert
            Assert.AreEqual(default(GiftAidResponse), giftAidResponse);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result.Result);
            Assert.AreEqual("Invalid Donation Value", ((BadRequestObjectResult)result.Result).Value);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestObjectResult)result.Result).StatusCode);
        }

        [Test]
        public async Task Given_Amount_GreaterThan_MaximumAmount_When_GetGiftAidAmount_Returns_Valid_GiftAidResponse()
        {
            //arrange
            giftAidCalculatorMock.Setup(o => o.CalculateGiftAidAsync(It.IsAny<GiftAid>())).Throws(new System.Exception("Invalid Donation Value"));

            //act
            var result = await this.giftAidController.GetGiftAidAmount(100000.01d).ConfigureAwait(false);
            var giftAidResponse = result.Value;

            //assert
            Assert.AreEqual(default(GiftAidResponse), giftAidResponse);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result.Result);
            Assert.AreEqual("Invalid Donation Value", ((BadRequestObjectResult)result.Result).Value);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestObjectResult)result.Result).StatusCode);
        }

        [Test]
        public async Task Given_DefaultDonation_When_GetGiftAidAmount_Returns_BadRequestResponse400()
        {
            //arrange
            giftAidCalculatorMock.Setup(o => o.CalculateGiftAidAsync(It.IsAny<GiftAid>())).Throws(new System.Exception("Invalid Donation Value"));

            //act
            var result = await this.giftAidController.GetGiftAidAmount(default(double)).ConfigureAwait(false);
            var giftAidResponse = result.Value;

            //assert
            Assert.AreEqual(default(GiftAidResponse), giftAidResponse);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result.Result);
            Assert.AreEqual("Invalid Donation Value", ((BadRequestObjectResult)result.Result).Value);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestObjectResult)result.Result).StatusCode);
        }

        [Test]
        public async Task Given_InvalidDonation_When_GetGiftAidAmount_Returns_BadRequestResponse400()
        {
            //arrange
            giftAidCalculatorMock.Setup(o => o.CalculateGiftAidAsync(It.IsAny<GiftAid>())).Throws(new System.Exception("Invalid Donation Value"));

            //act
            var result = await this.giftAidController.GetGiftAidAmount(-15d).ConfigureAwait(false);
            var giftAidResponse = result.Value;

            //assert
            Assert.AreEqual(default(GiftAidResponse), giftAidResponse);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result.Result);
            Assert.AreEqual("Invalid Donation Value", ((BadRequestObjectResult)result.Result).Value);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestObjectResult)result.Result).StatusCode);
        }
    }
}
