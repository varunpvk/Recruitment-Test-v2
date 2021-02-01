namespace JG.FinTech.Domain
{
    using JG.FinTech.Models;
    using System;
    using System.Threading.Tasks;

    public interface IGiftAidRepository
    {
        Task<bool> AddDonorDetails(DonorDetails donorDetails);
        Task<DonorDetails> FindDonorBy(string donorID);
        Task<bool> UpdateDonor(DonorDetails donorDetails);
        Task<bool> DeleteDonor(string donorID);

    }
}
