using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sana.Sim.Business.Repositories;
using Sana.Sim.EF.DbAccess;
using Sana.Sim.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EFRepositoriesServiceCollectionExtensions
    {
        public static void AddEFRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(optionsAction => optionsAction.UseSqlServer(connectionString));
            services.AddScoped<DataContextFactory>();

            services.AddScoped<IFeaturesRepository, FeaturesRepository>();
            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            services.AddScoped<IWebshopsRepository, WebshopsRepository>();
            services.AddScoped<IDevelopersRepository, DevelopersRepository>();
            services.AddScoped<IServersRepository, ServersRepository>();
        }
    }
}
