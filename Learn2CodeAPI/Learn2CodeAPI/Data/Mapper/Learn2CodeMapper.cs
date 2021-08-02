using AutoMapper;
using Learn2CodeAPI.Dtos.AdminDto;
using Learn2CodeAPI.Dtos.StudentDto;
using Learn2CodeAPI.Dtos.TutorDto;
using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Login.Identity;
using Learn2CodeAPI.Models.Student;
using Learn2CodeAPI.Models.Tutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Data.Mapper
{
    public class Learn2CodeMapper :Profile
    {
        public Learn2CodeMapper()
        {
            CreateMap<University, UniversityDto>().ReverseMap();
            CreateMap<RegistrationDto, AppUser>().ReverseMap();
            CreateMap<AppUser,CreateTutorDto>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<DegreeDto, Degree>().ReverseMap();
            CreateMap<ModuleDto, Module>().ReverseMap();
            CreateMap<CourseFolderDto, CourseFolder>().ReverseMap();
            CreateMap<CoursSubCategoryDto, CourseSubCategory>().ReverseMap();
            CreateMap<SessionContentCategoryDto, SessionContentCategory>().ReverseMap();
            CreateMap<TutorDto, Tutor>().ReverseMap();
            CreateMap<SentMessageDto, Message>().ReverseMap();
            CreateMap<SubscriptionDto, Subscription>().ReverseMap();

            //CreateMap<(Student, Message), SentMessageDto>().ReverseMap();

        }
    }
}
