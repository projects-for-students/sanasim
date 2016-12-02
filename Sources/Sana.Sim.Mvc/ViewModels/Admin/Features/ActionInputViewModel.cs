using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities.Features;

namespace Sana.Sim.Mvc.ViewModels.Admin.Features
{
    public class ActionInputViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Tag { get; set; }

        [Required]
        public decimal ImplementationCost { get; set; }

        [Required]
        public decimal Duration { get; set; }

        public decimal? OfflineUsersConversionRate { get; set; }

        public decimal? NewOnlineUsersAmount { get; set; }

        public decimal? AverageOrderAmountIncreaseRate { get; set; }
    }
}