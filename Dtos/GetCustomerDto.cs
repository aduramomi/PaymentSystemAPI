using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentSystemAPI.Dtos
{
    public class GetCustomerDto
    {       
        public int Id { get; set; }      
        public string? NationalID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }      
        public DateTime? DateOfBirth { get; set; }      
        public string? CustomerNumber { get; set; } //unique identifier. though this is not specified
        public string? TransactionHistory { get; set; }
    }
}
