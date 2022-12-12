namespace Restaurant.Application.Mapper
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactViewModel>().ForMember(cvm => cvm.Email, m => m.MapFrom(ce => ce.Email))
                                                  .ForMember(cvm => cvm.PhoneNumber, m => m.MapFrom(ce => ce.PhoneNumber)).ReverseMap();
        }
    }
}
