using Restaurant.Core.Validator;
using RestaurantEntity = Restaurant.Core.Entities.Restaurant;

namespace Restaurant.Application.Mapper
{
    public sealed class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<CreateRestaurantCommand, RestaurantEntity>()
                .ForMember(re => re.Id, option => option.Ignore())
                .ForMember(re => re.CreatedAt, option => option.Ignore())
                .ConstructUsing(crc => new RestaurantEntity(crc.Restaurant.Name,
                                                           crc.Restaurant.Document,
                                                           crc.Restaurant.Description,
                                                           new User(crc.Restaurant.User.FirstName, crc.Restaurant.User.LastName),
                                                           new Address(crc.Restaurant.Address.FullAddress,
                                                                       crc.Restaurant.Address.PostalCode,
                                                                       crc.Restaurant.Address.Number ?? 0,
                                                                       crc.Restaurant.Address.State,
                                                                       crc.Restaurant.Address.Street,
                                                                       crc.Restaurant.Address.Country,
                                                                       crc.Restaurant.Address.Neighborhood,
                                                                       crc.Restaurant.Address.Zone,
                                                                       crc.Restaurant.Address.City),
                                                           crc.Restaurant.TotalTables,
                                                           new RestaurantValidator(),
                                                           crc.CorrelationId)
                ).AfterMap((crc, re) => re.Update(daysOfWork: crc.Restaurant.DaysOfWork.Select(d => new DayOfWork(d.DayOfWeek, d.OpensAt, d.ClosesAt, re, crc.CorrelationId)).ToList(),
                                                   contacts: crc.Restaurant.Contacts.Select(c => new Contact(c.PhoneNumber, c.Email, re, crc.CorrelationId)).ToList()));

            CreateMap<RestaurantEntity, RestaurantViewModel>().ForMember(rvm => rvm.Id, m => m.MapFrom(re => re.Id))
                                                              .ForMember(rvm => rvm.Document, m => m.MapFrom(re => re.Document))
                                                              .ForMember(rvm => rvm.Description, m => m.MapFrom(re => re.Description))
                                                              .ForMember(rvm => rvm.TotalTables, m => m.MapFrom(re => re.TotalTables))
                                                              .ForMember(rvm => rvm.User, m => m.MapFrom(re => new UserViewModel
                                                              {
                                                                  FirstName = re.User.FirstName,
                                                                  LastName = re.User.LastName,
                                                                  Username = string.Empty,
                                                                  Password = string.Empty,
                                                                  AccessToken = null
                                                              }))
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