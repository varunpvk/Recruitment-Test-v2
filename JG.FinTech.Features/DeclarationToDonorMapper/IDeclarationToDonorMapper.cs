namespace JG.FinTech.Features
{
    using JG.FinTech.Models;
    using System.Threading.Tasks;

    public interface IDeclarationToDonorMapper
    {
        Task<DonorDetails> GetDonorDetailsAsync(DeclarationDetails declarationDetails);
    }
}
