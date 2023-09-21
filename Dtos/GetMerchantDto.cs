using System.ComponentModel.DataAnnotations;

namespace PaymentSystemAPI.Dtos
{
    public class GetMerchantDto
    {
        public int Id { get; set; }
        public string? BusinessID { get; set; }
        public string? BusinessName { get; set; }       
        public string? ContactName { get; set; }     
        public string? ContactSurname { get; set; }       
        public DateTime? DateOfEstablishment { get; set; }       
        public string? MerchantNumber { get; set; } //unique identifier
        public int? AverageTransactionVolume { get; set; }
    }
}
