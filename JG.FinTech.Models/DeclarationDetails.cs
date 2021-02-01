namespace JG.FinTech.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class DeclarationDetails
    {
        [Key]
        public string DeclarationID { get; }

        [DataMember]
        public double DonationAmount { get; set; }

        public DeclarationDetails()
        {
            this.DeclarationID = Guid.NewGuid().ToString();
        }
    }
}
