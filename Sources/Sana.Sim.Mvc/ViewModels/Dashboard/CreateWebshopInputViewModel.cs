using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Mvc.ViewModels.Dashboard
{
    public class CreateWebshopInputViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "Active users count")]
        public decimal OfflineUsersCount { get; set; }

        [Display(Name = "Average order amount")]
        public decimal OfflineAverageOrderAmount { get; set; }

        public int IterationNumber { get; set; }

        public IEnumerable<Guid> Features { get; set; }

        [Display(Name="Product cost %")]
        public decimal ProductCostPercentage { get; set; }

        [Display(Name = "Offline Service cost %")]
        public decimal OfflineServiceCostPercentage { get; set; }

        [Display(Name = "Online Service cost %")]
        public decimal OnlineServiceCostPercentage { get; set; }
    }
}
