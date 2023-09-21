namespace PaymentSystemAPI.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }
        public T? Result { get; set; }
    }
}
