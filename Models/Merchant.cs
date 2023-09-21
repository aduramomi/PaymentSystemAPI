using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentSystemAPI.Models
{
    public class Merchant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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
