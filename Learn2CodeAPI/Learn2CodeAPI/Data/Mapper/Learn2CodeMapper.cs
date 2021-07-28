﻿using AutoMapper;
using Learn2CodeAPI.Dtos.AdminDto;
using Learn2CodeAPI.Dtos.StudentDto;
using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Login.Identity;
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
            CreateMap<DegreeDto, Degree>().ReverseMap();
            CreateMap<ModuleDto, Module>().ReverseMap();
            CreateMap<CourseFolderDto, CourseFolder>().ReverseMap();
            CreateMap<CoursSubCategoryDto, CourseSubCategory>().ReverseMap();
            CreateMap<SessionContentCategoryDto, SessionContentCategory>().ReverseMap();


        }
    }
}
