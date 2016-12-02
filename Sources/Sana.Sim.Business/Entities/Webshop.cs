using Sana.Sim.Business.Entities.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Entities
{
    public class Webshop
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public string Name { get; set; }

        public IEnumerable<WebshopFeature> Features { get; set; }

        public IEnumerable<WebshopChangeSet> Changes { get; set; } = new List<WebshopChangeSet>();

        public decimal OfflineUsersCount { get; set; }

        public decimal OfflineAverageOrderAmount { get; set; }

        public decimal ProductCostPercentage { get; set; }

        public decimal OfflineServiceCostPercentage { get; set; }

        public decimal OnlineServiceCostPercentage { get; set; }

        public WebshopChangeSet LatestChangeSet =>
            Changes.LastOrDefault();

        public WebshopFeature WebshopFeature =>
            Features.Single(f => f.Definition.Type == BusinessConstants.FeatureTypes.Webshop);

        public bool IsOnLine =>
            WebshopFeature.Implemented;

        public decimal ExpectedIncome =>
            UserCount * AverageOrderAmount;

        public decimal ExpectedCost =>
            ExpectedProductCost + ExpectedServiceCost;

        public decimal ExpectedProductCost =>
            LatestChangeSet?.GetProductCost(this) ?? 0m;

        public decimal ExpectedServiceCost =>
            LatestChangeSet?.GetServiceCost(this) ?? 0m;

        public decimal ExpectedProfit =>
            ExpectedIncome - ExpectedCost;

        public decimal TotalIncome =>
            Changes.Sum(s => s.Income);

        public decimal TotalCost =>
            Changes.Sum(c => c.GetCost(this));

        public decimal ConvertedUsersCount =>
            Changes.Sum(s => s.ConvertedUsersCount);

        public decimal NewUsersCount =>
            Changes.Sum(s => s.NewUsersCount);

        public decimal UserCount =>
            Changes.Sum(s => s.UserCount);

        public decimal AverageOrderAmount
        {
            get
            {
                var amount = OfflineAverageOrderAmount;

                foreach (var state in Changes)
                {
                    amount *= state.AverageOrderAmountIncreaseRate + 1;
                }

                return amount;
            }
        }
    }
}
