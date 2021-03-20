namespace JG.FinTech.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class DonorDetailsResponse
    {
        [DataMember, Key]
        public string DonorID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string PostCode { get; set; }
        [DataMember]
        public double DonationAmount { get; set; }
        [DataMember]
        public double GiftAid { get; set; }
    }
}
