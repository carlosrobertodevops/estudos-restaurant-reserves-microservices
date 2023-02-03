namespace Restaurant.Application.Mapper
{
    public sealed class DayOfWorkProfile : Profile
    {
        public DayOfWorkProfile()
        {
            CreateMap<DayOfWork, DayOfWorkViewModel>().ForMember(dvm => dvm.DayOfWeek, m => m.MapFrom(de => de.DayOfWeek))
                                                      .ForMember(dvm => dvm.OpensAt, m => m.MapFrom(ce => ce.OpensAt))
                                                      .ForMember(dvm => dvm.ClosesAt, m => m.MapFrom(ce => ce.ClosesAt))
                                                      .ReverseMap();
        }
    }
}
