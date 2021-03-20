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

        public static bool operator ==(GiftAid obj1, GiftAid obj2) => Equals(obj1, obj2);

        public static bool operator !=(GiftAid obj1, GiftAid obj2) => Equals(obj1, obj2);

        public static bool operator >(GiftAid obj1, GiftAid obj2) => obj1.DenominationAmount > obj2.DenominationAmount;

        public static bool operator <(GiftAid obj1, GiftAid obj2) => obj1.DenominationAmount < obj2.DenominationAmount;
    }
}
