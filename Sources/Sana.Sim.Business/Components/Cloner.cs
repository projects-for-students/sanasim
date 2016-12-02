using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Sana.Sim.Business.Components
{
    public static class Cloner
    {
        public static T Clone<T>(T entity) =>
            Mapper.Map<T>(entity);
    }
}
