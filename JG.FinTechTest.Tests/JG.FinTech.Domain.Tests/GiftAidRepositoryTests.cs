namespace JG.FinTechTest.Tests.JG.FinTech.Domain.Tests
{
    using global::JG.FinTech.Domain;
    using global::JG.FinTech.Models;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class GiftAidRepositoryTests
    {
        private Mock<DonorContext> donorContextMock;
        private GiftAidRepository giftAidRepository;
        private DonorDetails donorDetails;

        [SetUp]
        public void Setup()
        {
            donorContextMock = new Mock<DonorContext>();
            giftAidRepository = new GiftAidRepository(donorContextMock.Object);
        }

        [Test]
        public async Task Given_ValidDonorDetails_When_AddDonorDetailsCalled_Then_DonorCreatedSuccessfully()
        {
            //arrange
            donorDetails = new DonorDetails
            {
                DonorID = Guid.NewGuid().ToString(),
                DonationAmount = 1000d,
                GiftAid = 200d,
                Name = "varun",
                PostCode = "4460-210"
            };

            //act
            var result = await this.giftAidRepository.AddDonorDetails(donorDetails).ConfigureAwait(false);

            //assert
            Assert.IsTrue(result);
            donorContextMock.Verify(o => o.AddAsync(It.IsAny<DonorDetails>(), It.IsAny<CancellationToken>()), Times.Once);
            donorContextMock.Verify(o => o.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Given_DeafultDonorDetails_When_AddDonorDetailsCalled_ThrowsException()
        {
            //arrange
            donorContextMock.Setup(o => o.AddAsync(It.IsAny<DonorDetails>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("Invalid Donor Details"));

            //act & assert
            Assert.ThrowsAsync<Exception>(async delegate { await this.giftAidRepository.AddDonorDetails(donorDetails).ConfigureAwait(false); });
        }
    }
}
