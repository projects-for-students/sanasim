using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business;
using Sana.Sim.Business.Repositories;

namespace Sana.Sim
{
    public static class Framework
    {
        public static ExecutionContext Context =>
            IoC.Resolve<ExecutionContext>();

        public static IFeaturesRepository Features =>
            IoC.Resolve<IFeaturesRepository>();

        public static IProjectsRepository Projects =>
            IoC.Resolve<IProjectsRepository>();

        public static IWebshopsRepository Webshops =>
            IoC.Resolve<IWebshopsRepository>();

        public static IDevelopersRepository Developers =>
            IoC.Resolve<IDevelopersRepository>();

        public static IServersRepository Servers =>
            IoC.Resolve<IServersRepository>();
    }
}
