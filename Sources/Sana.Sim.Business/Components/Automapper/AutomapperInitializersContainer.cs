using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Sana.Sim.Business.Components.Automapper
{
    public class AutomapperInitializersContainer
    {
        private List<Action<IMapperConfigurationExpression>> initializers;

        public AutomapperInitializersContainer()
        {
            this.initializers = new List<Action<IMapperConfigurationExpression>>();
        }

        public void Register(Action<IMapperConfigurationExpression> config)
        {
            this.initializers.Add(config);
        }

        public Action<IMapperConfigurationExpression> Build()
        {
            return config => this.initializers.ForEach(initializer => initializer(config));
        }
    }
}
