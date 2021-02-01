namespace JG.FinTech.Domain
{
    using JG.FinTech.Models;
    using System;
    using System.Threading.Tasks;

    public class GiftAidRepository : IGiftAidRepository
    {
        private readonly DonorContext donorContext;
        public GiftAidRepository(DonorContext donorContext)
        {
            this.donorContext = donorContext;
        }
        public async Task<bool> AddDonorDetails(DonorDetails donorDetails)
        {
            try
            {
                await this.donorContext.DonorDetails.AddAsync(donorDetails).ConfigureAwait(false);
                await this.donorContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> DeleteDonor(string donorID)
        {
            throw new NotImplementedException();
        }

        public Task<DonorDetails> FindDonorBy(string donorID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDonor(DonorDetails donorDetails)
        {
            throw new NotImplementedException();
        }
    }
}
