namespace JG.FinTech.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public struct GiftAidResponse
    {
        [DataMember]
        public string DonorID { get; set; }
        
        [DataMember]
        public double GiftAidAmount { get; set; }

        [DataMember]
        public double DonationAmount { get; set; }
    }
}
