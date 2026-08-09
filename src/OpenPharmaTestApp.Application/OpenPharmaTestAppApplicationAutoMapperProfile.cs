using AutoMapper;
using OpenPharmaTestApp.TaskListCustomers;
using OpenPharmaTestApp.TasksList;

namespace OpenPharmaTestApp;

public class OpenPharmaTestAppApplicationAutoMapperProfile : Profile
{
    public OpenPharmaTestAppApplicationAutoMapperProfile()
    {
        CreateMap<TaskList, TaskListDto>(MemberList.Source)
            .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(d => d.CretedById, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(d => d.CreationTime, opt => opt.MapFrom(src => src.CreationTime));

        CreateMap<Customer, CustomerDto>(MemberList.Source)
           .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(d => d.CreationTime, opt => opt.MapFrom(src => src.CreationTime));
    }
}
