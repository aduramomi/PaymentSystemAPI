using PaymentSystemAPI.Dtos;
using PaymentSystemAPI.Models;

namespace PaymentSystemAPI.IServices
{
    public interface IMerchantService
    {
        Task<IEnumerable<Merchant>> GetAll();
        Task<Merchant?> GetMerchantByMerchantNumber(string merchantNumber);
        Task<Merchant?> GetMerchantById(int id);
        Task<Merchant?> GetMerchant(int id, string merchantNumberToBeUpdated);
        Task CreateMerchant(CreateMerchantDto model);
        Task UpdateMerchant(int id, UpdateMerchantDto model);
        Task DeleteMerchant(int id);
    }
}
