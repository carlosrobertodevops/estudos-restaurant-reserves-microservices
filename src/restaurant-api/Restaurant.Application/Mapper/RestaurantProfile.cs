using Restaurant.Core.Validator;
using RestaurantEntity = Restaurant.Core.Entities.Restaurant;

namespace Restaurant.Application.Mapper
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<RestaurantViewModel, RestaurantEntity>()
                .ConstructUsing(rvm =>new RestaurantEntity(rvm.Name,
                                                           rvm.Document,
                                                           rvm.Description,
                                                           new Address(rvm.Address.FullAddress,
                                                                       rvm.Address.PostalCode,
                                                                       rvm.Address.Number,
                                                                       rvm.Address.State,
                                                                       rvm.Address.Street,
                                                                       rvm.Address.Country,
                                                                       rvm.Address.Neighborhood,
                                                                       rvm.Address.Zone,
                                                                       rvm.Address.City),
                                                           rvm.TotalTables,
                                                           new RestaurantValidator())
                ).AfterMap((rvm, re) => re.Update(daysOfWork: rvm.DaysOfWork.Select(d => new DayOfWork(d.DayOfWeek, d.OpensAt, d.ClosesAt, re)).ToList(),
                                                   contacts: rvm.Contacts.Select(c => new Contact(c.PhoneNumber, c.Email, re)).ToList()));

            CreateMap<RestaurantEntity, RestaurantViewModel>().ForMember(rvm => rvm.Id, m => m.MapFrom(re => re.Id))
                                                              .ForMember(rvm => rvm.Document, m => m.MapFrom(re => re.Document))
                                                              .ForMember(rvm => rvm.Description, m => m.MapFrom(re => re.Description))
                                                              .ForMember(rvm => rvm.TotalTables, m => m.MapFrom(re => re.TotalTables))
                                                              .ForMember(rvm => rvm.Address, m => m.MapFrom(re => new AddressViewModel
                                                              {
                                                                  FullAddress = re.Address.FullAddress,
                                                                  PostalCode = re.Address.PostalCode,
                                                                  Number = re.Address.Number,
                                                                  State = re.Address.State,
                                                                  Street = re.Address.Street,
                                                                  Country = re.Address.Country,
                                                                  Neighborhood = re.Address.Neighborhood,
                                                                  Zone = re.Address.Zone,
                                                                  City = re.Address.City
                                                              }))
                                                              .ForMember(rvm => rvm.CreatedAt, m => m.MapFrom(re => re.CreatedAt))
                                                              .ForMember(rvm => rvm.DaysOfWork, m => m.MapFrom(re => re.DaysOfWork.Select(d => new DayOfWorkViewModel
                                                              {
                                                                  DayOfWeek = d.DayOfWeek,
                                                                  OpensAt = d.OpensAt,
                                                                  ClosesAt = d.ClosesAt
                                                              }).ToList()))
                                                              .ForMember(rvm => rvm.Contacts, m => m.MapFrom(re => re.Contacts.Select(c => new ContactViewModel
                                                              {
                                                                  PhoneNumber = c.PhoneNumber,
                                                                  Email = c.Email
                                                              }).ToList()));
        }
    }
}