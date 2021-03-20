namespace JG.FinTech.Features
{
    using JG.FinTech.Domain;
    using JG.FinTech.Features.GiftAidCalculator;
    using JG.FinTech.Models;
    using System;
    using System.Threading.Tasks;

    public class DeclarationToDonorMapper : IDeclarationToDonorMapper
    {
        private readonly IGiftAidCalculator giftAidCalculator;
        private readonly IGiftAidRepository giftAidRepository;
        
        public DeclarationToDonorMapper(IGiftAidCalculator giftAidCalculator, IGiftAidRepository giftAidRepository)
        {
            this.giftAidCalculator = giftAidCalculator;
            this.giftAidRepository = giftAidRepository;
        }

        public Task<DonorDetails> GetDonorDetailsAsync(DeclarationDetails declarationDetails)
        {
            if (declarationDetails == null)
                throw new ArgumentNullException("declarationDetails", "Invalid Declaration");

            return this.GetDonorDetails(declarationDetails);
        }

        public Task<DonorDetails> GetDonorDetailsByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id", "id cannot be empty");

            return this.GetDonorDetailsById(id);
        }

        private async Task<DonorDetails> GetDonorDetailsById(string id)
        {
            var donorDetails = await this.giftAidRepository.FindDonorBy(id).ConfigureAwait(false);

            return donorDetails;
        }

        private async Task<DonorDetails> GetDonorDetails(DeclarationDetails declarationDetails)
        {
            var donorDetails = new DonorDetails();
            var giftAid = await this.giftAidCalculator.CalculateGiftAidAsync(new GiftAid(declarationDetails.DonationAmount)).ConfigureAwait(false);
            donorDetails.GiftAid = giftAid;
            donorDetails.DonationAmount = declarationDetails.DonationAmount;
            donorDetails.DonorID = declarationDetails.DeclarationID;

            return donorDetails;
        }
    }
}
