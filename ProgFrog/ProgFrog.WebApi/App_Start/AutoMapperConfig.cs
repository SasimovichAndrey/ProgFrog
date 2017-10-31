using AutoMapper;
using ProgFrog.Interface.Model;
using ProgFrog.WebApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgFrog.WebApi.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<ProgrammingTask, ProgrammingTaskViewModel>()
                    .ForMember(m => m.Id, opt => opt.MapFrom(pt => pt.Identifier.StringPresentation));

                cfg.CreateMap<ProgrammingTaskViewModel, ProgrammingTask>()
                    .ForMember(m => m.Identifier, opt => opt.MapFrom(pt => new GuidIdentifier(pt.Id)));
            });
        }
    }
}