namespace JG.FinTech.Features.GiftAidCalculator
{
    using JG.FinTech.Models;
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    public class GiftAidCalculator : IGiftAidCalculator
    {
        private const char currencyPrecisionIdentifier = '.';

        public static readonly GiftAid minGiftAid = new GiftAid(2.00d);
        public static readonly GiftAid maxGiftAid = new GiftAid(100000.00d);

        public Task<double> CalculateGiftAidAsync(GiftAid giftAid)
        {
            if (giftAid.Equals(default))
                throw new ArgumentNullException("giftAid");

            if (IsInValidDenomination(giftAid))
                throw new System.Exception($"Invalid Denomination, Accepted Range £{minGiftAid.DenominationAmount} - £{maxGiftAid.DenominationAmount}");

            return Task.FromResult(GetGiftAidCalculation(giftAid));
        }

        private bool IsInValidDenomination(GiftAid giftAid) => (giftAid < minGiftAid) || (giftAid > maxGiftAid);

        private double GetTaxRateDifference(double taxRate) => 100 - taxRate;

        private double GetTaxRateCalculation(double taxRate) => taxRate / GetTaxRateDifference(taxRate);

        private double GetGiftAidCalculation(GiftAid giftAid) => giftAid.DenominationAmount * GetTaxRateCalculation(giftAid.TaxRate);

        //Remove this method, if the input parameter type is not string.
        private bool IsPrecisionFound(double amount)
        {
            var result = (Regex.Match(amount.ToString(), "^\\d*.\\d*$")).Value.Split(currencyPrecisionIdentifier)[1];
            return !string.IsNullOrEmpty(result);
        }
    }
}
