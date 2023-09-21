using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaymentSystemAPI.Dtos;
using PaymentSystemAPI.IServices;
using PaymentSystemAPI.Models;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;

namespace PaymentSystemAPI.Services
{
    public class MerchantService : IMerchantService
    {
        private PaymentSystemDbContext _context;
        private readonly IMapper _mapper;

        public MerchantService(PaymentSystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Merchant>> GetAll()
        {
            return await _context.Merchants.ToListAsync();
        }

        public async Task<Merchant?> GetMerchantByMerchantNumber(string merchantNumber)
        {
            return await GetMerchant(merchantNumber);
        }

        public async Task<Merchant?> GetMerchantById(int id)
        {
            return await GetMerchant(id);
        }

        public async Task<Merchant?> GetMerchant(int id, string merchantNumberToBeUpdated)
        {
            var merchant = await _context.Merchants.Where(m => m.Id != id && m.MerchantNumber.ToUpper() == merchantNumberToBeUpdated.ToUpper()).SingleOrDefaultAsync();

            return merchant;
        }

        public async Task CreateMerchant(CreateMerchantDto model)
        {           
            var merchant = _mapper.Map<Merchant>(model);

            await _context.Merchants.AddAsync(merchant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMerchant(int id, UpdateMerchantDto model)
        {
            var merchant = await GetMerchant(id);
           
            _mapper.Map(model, merchant);
            _context.Merchants.Update(merchant);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteMerchant(int id)
        {
            var merchant = await GetMerchant(id);
           
            _context.Merchants.Remove(merchant);

            await _context.SaveChangesAsync();
        }

        private async Task<Merchant?> GetMerchant(string merchantNumber)
        {
            if (string.IsNullOrEmpty(merchantNumber))
                return null;

            var merchant = await _context.Merchants.SingleOrDefaultAsync(m => m.MerchantNumber.ToUpper() == merchantNumber.ToUpper());

            return merchant;
        }

        private async Task<Merchant?> GetMerchant(int id)
        {
            var merchant = await _context.Merchants.FindAsync(id);

            return merchant;
        }


    }
}
