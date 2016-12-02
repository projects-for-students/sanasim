using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Sana.Sim.Business.Entities.Features;
using Sana.Sim.Mvc.ViewModels.Admin.Features;
using Sana.Sim.Mvc.ViewModels.Dashboard;
using Sana.Sim.Business.Entities;
using System.Linq.Expressions;
using Sana.Sim.Business;
using Sana.Sim.Business.Entities.Resources;
using Sana.Sim.Mvc.ViewModels.Admin.Resources;

namespace Sana.Sim.Mvc
{
    public static class AutoMapperInitializer
    {
        public static void Initialize(IMapperConfigurationExpression config)
        {
            config.CreateMap<Feature, FeatureInputViewModel>();
            config.CreateMap<FeatureInputViewModel, Feature>()
                .ForMember(f => f.Id, opt => opt.MapFrom(src => src.Id.HasValue ? src.Id.Value : Guid.NewGuid()))
                .ForMember(f => f.Type, opt => opt.MapFrom(src => BusinessConstants.FeatureTypes.Functionality));

            config.CreateMap<Feature, ActionInputViewModel>();
            config.CreateMap<ActionInputViewModel, Feature>()
                .ForMember(f => f.Id, opt => opt.MapFrom(src => src.Id.HasValue ? src.Id.Value : Guid.NewGuid()))
                .ForMember(f => f.Type, opt => opt.MapFrom(src => BusinessConstants.FeatureTypes.Action));

            config.CreateMap<CreateProjectInputViewModel, Project>()
                .ForMember(w => w.Developers, opt => opt.MapFrom(CreateInitialProjectDevelopers()))
                .ForMember(w => w.Servers, opt => opt.MapFrom(CreateInitialProjectServers()));

            config.CreateMap<CreateWebshopInputViewModel, Webshop>()
                .ForMember(w => w.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(w => w.Features, opt => opt.MapFrom(CreateIncludedWebshopFeatures()));

            config.CreateMap<Developer, DeveloperInputViewModel>();
            config.CreateMap<DeveloperInputViewModel, Developer>()
                .ForMember(f => f.Id, opt => opt.MapFrom(src => src.Id.HasValue ? src.Id.Value : Guid.NewGuid()));

            config.CreateMap<Server, ServerInputViewModel>();
            config.CreateMap<ServerInputViewModel, Server>()
                .ForMember(f => f.Id, opt => opt.MapFrom(src => src.Id.HasValue ? src.Id.Value : Guid.NewGuid()));

            config.CreateMap<ProjectChangeSet, ProjectChangeSet>();
            config.CreateMap<WebshopChangeSet, WebshopChangeSet>();
        }

        private static Expression<Func<CreateWebshopInputViewModel, IEnumerable<WebshopFeature>>> CreateIncludedWebshopFeatures()
        {
            return src =>
                src.Features.Select(id => new WebshopFeature()
                {
                    Definition = Framework.Features.Get(id),
                    RemainingImplementationRequirements = Framework.Features.Get(id).ImplementationRequirements,
                    AddedOnIteration = Framework.Context.IterationNumber
                });
        }

        private static Expression<Func<CreateProjectInputViewModel, ProjectDeveloper[]>> CreateInitialProjectDevelopers()
        {
            return src => new ProjectDeveloper[]
            {
                /*
                new ProjectDeveloper
                {
                    Definition = Framework.Developers.Get(BusinessConstants.DeveloperId.OneDay)
                }
                */
            };
        }

        private static Expression<Func<CreateProjectInputViewModel, ProjectServer[]>> CreateInitialProjectServers()
        {
            return src => new ProjectServer[]
            {
                new ProjectServer
                {
                    Definition = Framework.Servers.Get(BusinessConstants.ServerId.D1V2)
                }
            };
        }
    }
}