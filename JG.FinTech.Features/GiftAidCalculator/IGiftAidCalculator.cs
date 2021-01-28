namespace JG.FinTech.Features.GiftAidCalculator
{
    using JG.FinTech.Models;
    using System.Threading.Tasks;
    public interface IGiftAidCalculator
    {
        Task<double> CalculateGiftAidAsync(GiftAid giftAid);
    }
}
