namespace JG.FinTech.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    [DataContract]
    public struct DonorResponse
    {
        [DataMember, Key]
        public string DonorID { get; set; }

        /// <summary>
        /// 25
        /// </summary>
        [DataMember]
        public double GiftAidAmount { get; set; }

        [JsonIgnore]
        public double DonationAmount { get; set; }
    }
}
