namespace JG.FinTech.Models
{
    using System;
    public readonly struct GiftAid : IEquatable<GiftAid>
    {
        public GiftAid(double denominationAmount)
        {
            this.DenominationAmount = denominationAmount;
        }

        public double DenominationAmount { get; }

        public double TaxRate { get { return 20d; } }

        public bool Equals(GiftAid other) =>
            other.DenominationAmount == this.DenominationAmount;

        public override bool Equals(object obj) => (obj != null) && Equals(obj);

        public override int GetHashCode() => this.DenominationAmount.GetHashCode();
    }
}
