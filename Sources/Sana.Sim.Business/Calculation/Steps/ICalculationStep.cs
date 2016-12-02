using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Calculation.Steps
{
    public interface ICalculationStep
    {
        string Name { get; }
        void Execute(CalculationContext context);
    }
}
