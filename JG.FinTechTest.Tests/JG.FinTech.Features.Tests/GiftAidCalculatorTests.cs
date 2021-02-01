namespace JG.FinTech.Features.Tests
{
    using JG.FinTech.Features.GiftAidCalculator;
    using JG.FinTech.Models;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;

    public class GiftAidCalculatorTests
    {
        private IGiftAidCalculator giftAidCalculator;
        private GiftAid giftAid;

        [SetUp]
        public void Setup()
        {
            giftAidCalculator = new GiftAidCalculator();
        }

        [Test]
        public async Task Given_ValidDonation_When_CalculateGiftAidAsyncCalled_Returns_GiftAidCalculationWithTaxRate()
        {
            //arrange
            giftAid = new GiftAid(3500.75d);

            //act
            var result = await giftAidCalculator.CalculateGiftAidAsync(giftAid).ConfigureAwait(false);

            //assert
            Assert.AreEqual(875.1875d, result);
        }

        [Test]
        public async Task Given_MinimumAmount_When_CalculateGiftAidCalled_Returns_GiftAidCalculationWithTaxRate()
        {
            //arrange
            giftAid = GiftAidCalculator.minGiftAid;

            //act
            var result = await giftAidCalculator.CalculateGiftAidAsync(giftAid).ConfigureAwait(false);

            //assert
            Assert.AreEqual(0.5d, result);
        }

        [Test]
        public async Task Given_MaximumAmount_When_CalculateGiftAidCalled_Returns_GiftAidCalculationWithTaxRate()
        {
            //arrange
            giftAid = GiftAidCalculator.maxGiftAid;

            //act
            var result = await giftAidCalculator.CalculateGiftAidAsync(giftAid).ConfigureAwait(false);

            //assert
            Assert.AreEqual(25000.00d, result);
        }

        [Test]
        public void Given_Amount_LessThan_MinimumGiftAid_When_CalculateGiftAidCalled_Throws_Exception()
        {
            //arrange
            giftAid = new GiftAid(1.9999);

            //act
            var exception = Assert.ThrowsAsync<Exception>(() => giftAidCalculator.CalculateGiftAidAsync(giftAid), "Provide InValid Denomination - either 0 or a negative value");

            //assert
            Assert.AreEqual($"Invalid Denomination, Accepted Range £{GiftAidCalculator.minGiftAid.DenominationAmount} - £{GiftAidCalculator.maxGiftAid.DenominationAmount}", exception.Message);
        }

        [Test]
        public void Given_Amount_GreaterThan_MaximumGiftAid_When_CalculateGiftAidCalled_Throws_Exception()
        {
            //arrange
            giftAid = new GiftAid(100000.1234d);

            //act
            var exception = Assert.ThrowsAsync<Exception>(() => giftAidCalculator.CalculateGiftAidAsync(giftAid), "Provide InValid Denomination - either 0 or a negative value");

            //assert
            Assert.AreEqual($"Invalid Denomination, Accepted Range £{GiftAidCalculator.minGiftAid.DenominationAmount} - £{GiftAidCalculator.maxGiftAid.DenominationAmount}", exception.Message);
        }

        [Test]
        public void Given_DefaultGiftAid_When_CalculateGiftAidAsyncCalled_Throws_Exception()
        {
            //act & assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => giftAidCalculator.CalculateGiftAidAsync(default), "Provide InValid Denomination - either 0 or a negative value");
        }


        [Test]
        public void Given_DefaultDonation_When_CalculateGiftAidAsyncCalled_Throws_Exception()
        {
            //arrange
            giftAid = new GiftAid(default(double));

            //act & assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => giftAidCalculator.CalculateGiftAidAsync(giftAid), "Provide InValid Denomination - either 0 or a negative value");
        }

        [Test]
        public void Given_InValidDonation_When_CalculateGiftAidAsyncCalled_Throws_Exception()
        {
            //arrange
            giftAid = new GiftAid(-15d);

            //act
            var exception = Assert.ThrowsAsync<Exception>(() => giftAidCalculator.CalculateGiftAidAsync(giftAid), "Provide InValid Denomination - either 0 or a negative value");

            //assert
            Assert.AreEqual($"Invalid Denomination, Accepted Range £{GiftAidCalculator.minGiftAid.DenominationAmount} - £{GiftAidCalculator.maxGiftAid.DenominationAmount}", exception.Message);
        }
    }
}