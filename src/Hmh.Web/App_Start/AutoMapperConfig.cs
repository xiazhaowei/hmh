using AutoMapper;
using Hmh.Core.Identity.Dtos;
using Hmh.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hmh.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void MapperRegister()
        {
            //自动映射配置
            Mapper.CreateMap<RegisterViewModel, UserInputDto>();           
        }
    }
}