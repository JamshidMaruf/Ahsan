using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.Companies;
using Ahsan.Service.DTOs.Issues;
using Ahsan.Service.DTOs.Positions;
using Ahsan.Service.DTOs.Users;
using AutoMapper;

namespace Ahsan.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //User
        CreateMap<User, UserForCreationDto>().ReverseMap();
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();
        CreateMap<UserForCreationDto, UserForUpdateDto>().ReverseMap();

        //Position
        CreateMap<Position, PositionForCreationDto>().ReverseMap();
        CreateMap<Position, PositionForResultDto>().ReverseMap();

        //Company
        CreateMap<Company, CompanyForCreationDto>().ReverseMap();
        CreateMap<Company, CompanyForResultDto>().ReverseMap();

        //IssueCategory
        CreateMap<IssueCategory, IssueCategoryForCreationDto>().ReverseMap();
        CreateMap<IssueCategory, IssueCategoryForResultDto>().ReverseMap();
    }
}
