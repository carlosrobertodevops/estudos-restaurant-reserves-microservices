namespace Restaurant.Application.Mapper
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressViewModel>().ReverseMap();
        }
    }
}
