using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities.Features;

namespace Sana.Sim.Mvc.ViewModels.Admin.Features
{
    public class FeatureInputViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Tag { get; set; }

        public Guid? ParentId { get; set; }

        [Required]
        public decimal ImplementationRequirements { get; set; }

        public decimal? OfflineUsersConversionRate { get; set; }

        public decimal? NewOnlineUsersAmount { get; set; }

        public decimal? AverageOrderAmountIncreaseRate { get; set; }
    }
}