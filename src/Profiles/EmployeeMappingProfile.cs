using AutoMapper;
using LegacyModern.DTOs;
using LegacyModern.Models;

namespace LegacyModern.Profiles;

public class EmployeeMappingProfile : Profile
{
    public EmployeeMappingProfile()
    {
        CreateMap<CreateEmployeeDto, Employee>();
        CreateMap<Employee, EmployeeResponseDto>()
            .ForMember(d => d.ManagerName,
                opt => opt.MapFrom(s => s.Manager != null
                    ? $"{s.Manager.FirstName} {s.Manager.LastName}"
                    : null));
    }
}
