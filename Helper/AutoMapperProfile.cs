using AutoMapper;
using PaymentSystemAPI.Dtos;
using PaymentSystemAPI.Models;

namespace PaymentSystemAPI.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMerchantDto -> Merchant
            CreateMap<CreateMerchantDto, Merchant>();

            //UpdateMerchantDto -> Merchant
            CreateMap<UpdateMerchantDto, Merchant>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore both null & empty string properties
                        if (prop == null) 
                            return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) 
                            return false;

                        return true;
                    }
                ));

            //Merchant -> CreateMerchantDto
            CreateMap<Merchant, CreateMerchantDto>();


            //CreateCustomerDto -> Customer
            CreateMap<CreateCustomerDto, Customer>();

            //UpdateCustomerDto -> Customer
            CreateMap<UpdateCustomerDto, Customer>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore both null & empty string properties
                        if (prop == null)
                            return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop))
                            return false;

                        return true;
                    }
                ));

            //Customer -> CreateCustomerDto
            CreateMap<Customer, CreateCustomerDto>();

        }
    }
}
