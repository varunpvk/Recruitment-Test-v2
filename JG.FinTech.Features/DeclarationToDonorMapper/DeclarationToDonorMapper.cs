namespace JG.FinTech.Features
{
    using JG.FinTech.Features.GiftAidCalculator;
    using JG.FinTech.Models;
    using System;
    using System.Threading.Tasks;

    public class DeclarationToDonorMapper : IDeclarationToDonorMapper
    {
        private readonly IGiftAidCalculator giftAidCalculator;
        
        public DeclarationToDonorMapper(IGiftAidCalculator giftAidCalculator)
        {
            this.giftAidCalculator = giftAidCalculator;
        }

        public Task<DonorDetails> GetDonorDetailsAsync(DeclarationDetails declarationDetails)
        {
            if (declarationDetails == null)
                throw new ArgumentNullException("declarationDetails", "Invalid Declaration");

            return this.GetDonorDetails(declarationDetails);
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
