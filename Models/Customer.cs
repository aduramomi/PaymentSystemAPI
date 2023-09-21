using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentSystemAPI.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? NationalID { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Surname { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [MaxLength(50)]
        public string? CustomerNumber { get; set; } //unique identifier. though this is not specified

        [MaxLength(1000)]
        public string? TransactionHistory { get; set; }
    }
}
