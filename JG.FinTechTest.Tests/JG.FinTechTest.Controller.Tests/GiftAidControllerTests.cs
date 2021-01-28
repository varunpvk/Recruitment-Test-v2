namespace JG.FinTechTest.Tests.JG.FinTechTest.Controller.Tests
{
    using global::JG.FinTech.Features.GiftAidCalculator;
    using global::JG.FinTech.Models;
    using global::JG.FinTechTest.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;
    using System.Net;
    using System.Threading.Tasks;

    public class GiftAidControllerTests
    {
        private Mock<IGiftAidCalculator> giftAidCalculatorMock;
        private GiftAidController giftAidController;

        [SetUp]
        public void Setup()
        {
            giftAidCalculatorMock = new Mock<IGiftAidCalculator>();
            giftAidController = new GiftAidController(giftAidCalculatorMock.Object);
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
