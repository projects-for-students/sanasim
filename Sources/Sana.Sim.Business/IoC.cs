using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Sana.Sim.Business
{
    public static class IoC
    {
        [ThreadStatic]
        private static IServiceProvider serviceProvider;

        [ThreadStatic]
        private static ISession session;

        public static void Init(IServiceProvider applicationServices, ISession session)
        {
            serviceProvider = applicationServices;
            IoC.session = session;
        }

        public static ISession Session =>
            session;


        public static T Resolve<T>() =>
            (T)serviceProvider.GetService(typeof(T));
    }
}
