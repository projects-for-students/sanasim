using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities.Resources;

namespace Sana.Sim.Business.Entities
{
    public class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ProjectDeveloper> Developers { get; set; }

        public IEnumerable<ProjectServer> Servers { get; set; }

        public IEnumerable<Webshop> Webshops { get; set; }

        public IEnumerable<ProjectChangeSet> Changes { get; set; } = new List<ProjectChangeSet>();

        public decimal Budget { get; set; }

        public ProjectChangeSet LatestChangeSet =>
            Changes.LastOrDefault();

        public decimal ExpectedIncome =>
            Webshops.Sum(w => w.ExpectedIncome);

        public decimal DevelopersCost =>
            Developers.Sum(d => d.Definition.Cost);

        public decimal ExpectedCost =>
             DevelopersCost + Webshops.Sum(w => w.ExpectedCost);

        public decimal ExpectedProfit =>
            ExpectedIncome - ExpectedCost;

        public decimal TotalIncome =>
            Webshops.Sum(w => w.TotalIncome);

        public decimal TotalCost =>
            Changes.Sum(w => w.Cost) + Webshops.Sum(w => w.TotalCost);

        public decimal TotalProfit =>
            TotalIncome - TotalCost;

        public decimal Cash =>
            Budget + TotalProfit;
    }
}
