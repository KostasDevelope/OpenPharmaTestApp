using AutoMapper;
using OpenPharmaTestApp.TaskListCustomers;
using OpenPharmaTestApp.TasksList;

namespace OpenPharmaTestApp;

public class OpenPharmaTestAppApplicationAutoMapperProfile : Profile
{
    public OpenPharmaTestAppApplicationAutoMapperProfile()
    {
        CreateMap<TaskList, TaskListDto>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CretedById, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => src.CreationTime));
    }
}
