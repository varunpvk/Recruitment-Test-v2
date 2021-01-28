namespace JG.FinTech.Features.GiftAidCalculator
{
    using JG.FinTech.Models;
    using System.Threading.Tasks;

    public class GiftAidCalculator : IGiftAidCalculator
    {
        public async Task<double> CalculateGiftAidAsync(GiftAid giftAid)
        {
            if (giftAid.Equals(default) || IsInValidDenomination(giftAid))
                throw new System.Exception($"Invalid Denomination");

            return await Task.FromResult(GetGiftAidCalculation(giftAid)).ConfigureAwait(false);
        }

        private bool IsInValidDenomination(GiftAid giftAid) => giftAid.DenominationAmount <= 0;

        private double GetTaxRateDifference(double taxRate) => 100 - taxRate;

        private double GetTaxRateCalculation(double taxRate) => taxRate / GetTaxRateDifference(taxRate);

        private double GetGiftAidCalculation(GiftAid giftAid) => giftAid.DenominationAmount * GetTaxRateCalculation(giftAid.TaxRate);
    }
}
