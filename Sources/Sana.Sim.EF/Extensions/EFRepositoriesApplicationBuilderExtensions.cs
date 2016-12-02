using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using AutoMapper;
using Sana.Sim.EF;
using Sana.Sim.Business;
using Sana.Sim.Business.Components.Automapper;

namespace Microsoft.AspNetCore.Builder
{
    public static class EFRepositoriesApplicationBuilderExtensions
    {
        public static void UseEFRepositories(this IApplicationBuilder app)
        {
            IoC.Resolve<AutomapperInitializersContainer>().Register(AutoMapperInitializer.Initialize);
        }
    }
}
