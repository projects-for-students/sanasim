using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Entities
{
    public class WebshopChangeSet
    {
        public int IterationNumber { get; set; }
        
        public decimal UserCount => 
            NewUsersCount + ConvertedUsersCount;
        
        public decimal ConvertedUsersCount { get; set; }

        public decimal NewUsersCount { get; set; }

        public decimal AverageOrderAmountIncreaseRate { get; set; }

        public decimal Income { get; set; }
        
        public decimal GetProductCost(Webshop webshop) =>
            Income * webshop.ProductCostPercentage / 100;

        public decimal GetServiceCost(Webshop webshop) =>
            Income * webshop.OnlineServiceCostPercentage / 100;

        public decimal GetCost(Webshop webshop) =>
            GetProductCost(webshop) + GetServiceCost(webshop);
    }
}
