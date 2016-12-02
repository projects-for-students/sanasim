using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Calculation
{
    public class CalculationException : Exception
    {
        public CalculationException(string stepName, string message) : base(message)
        {
            this.StepName = stepName;
        }

        public string StepName { get; }
    }
}
