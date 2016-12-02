using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities.Features;
using Sana.Sim.Business.Entities.Resources;
using Sana.Sim.EF.DbEntities;
using Sana.Sim.Business.Entities;

namespace Sana.Sim.EF
{
    public static class AutoMapperInitializer
    {
        public static void Initialize(IMapperConfigurationExpression config)
        {
            config.CreateMap<Feature, DbFeature>();
            config.CreateMap<DbFeature, Feature>();

            config.CreateMap<Project, DbProject>();
            config.CreateMap<DbProject, Project>();

            config.CreateMap<ProjectChangeSet, DbProjectChangeSet>()
                .ForMember(s => s.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            config.CreateMap<DbProjectChangeSet, ProjectChangeSet>();

            config.CreateMap<Server, DbServer>();
            config.CreateMap<DbServer, Server>();

            config.CreateMap<Developer, DbDeveloper>();
            config.CreateMap<DbDeveloper, Developer>();

            config.CreateMap<Webshop, DbWebshop>();
            config.CreateMap<DbWebshop, Webshop>();

            config.CreateMap<WebshopChangeSet, DbWebshopChangeSet>()
                .ForMember(s => s.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            config.CreateMap<DbWebshopChangeSet, WebshopChangeSet>();

            config.CreateMap<WebshopFeature, DbWebshopFeature>()
                .ForMember(s => s.Id, opt => opt.MapFrom(src => src.Id != Guid.Empty ? src.Id : Guid.NewGuid()))
                .ForMember(s => s.FeatureId, opt => opt.MapFrom(src => src.Definition.Id));
            config.CreateMap<DbWebshopFeature, WebshopFeature>()
                .ForMember(s => s.Definition, opt => opt.MapFrom(src => Framework.Features.Get(src.FeatureId)));

            config.CreateMap<ProjectDeveloper, DbProjectDeveloper>()
                .ForMember(s => s.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(s => s.DeveloperId, opt => opt.MapFrom(src => src.Definition.Id));
            config.CreateMap<DbProjectDeveloper, ProjectDeveloper>()
                .ForMember(s => s.Definition, opt => opt.MapFrom(src => Framework.Developers.Get(src.DeveloperId)));

            config.CreateMap<ProjectServer, DbProjectServer>()
                .ForMember(s => s.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(s => s.ServerId, opt => opt.MapFrom(src => src.Definition.Id));
            config.CreateMap<DbProjectServer, ProjectServer>()
                .ForMember(s => s.Definition, opt => opt.MapFrom(src => Framework.Servers.Get(src.ServerId)));
        }
    }
}
