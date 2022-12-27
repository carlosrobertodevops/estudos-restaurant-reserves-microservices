namespace Restaurant.Application.Mapper
{
    public sealed class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressViewModel>().ReverseMap();
        }
    }
}
