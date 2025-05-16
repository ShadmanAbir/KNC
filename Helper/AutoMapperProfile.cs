using AutoMapper;
using KNC.Models;
using KNC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KNC.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentsViewModel>().ReverseMap();

        }
    }
}