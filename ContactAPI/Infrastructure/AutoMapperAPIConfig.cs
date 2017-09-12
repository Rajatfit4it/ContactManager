using AutoMapper;
using DAL.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViewModel;

namespace ContactAPI.Infrastructure
{
    public class AutoMapperAPIConfig
    {
        public static void ConfigureMappings()
        {
            Mapper.Initialize(e =>
            {
                e.CreateMap<ContactVM, Contact>().ReverseMap();
            });
        }
    }
}