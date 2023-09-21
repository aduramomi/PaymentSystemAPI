using System.ComponentModel.DataAnnotations;

namespace PaymentSystemAPI.Dtos
{
    public class CreateMerchantDto
    {
        [Required]
        [MaxLength(50)]
        public string? BusinessID { get; set; }

        [Required]
        [MaxLength(100)]
        public string? BusinessName { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ContactName { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ContactSurname { get; set; }

        [Required]
        public DateTime? DateOfEstablishment { get; set; }

        [Required]
        [MaxLength(50)]
        public string? MerchantNumber { get; set; } //unique identifier

        public int? AverageTransactionVolume { get; set; }
    }
}
