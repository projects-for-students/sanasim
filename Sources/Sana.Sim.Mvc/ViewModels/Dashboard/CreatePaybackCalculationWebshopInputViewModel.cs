using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Mvc.ViewModels.Dashboard
{
    public class CreatePaybackCalculationWebshopInputViewModel : CreateWebshopInputViewModel
    {
        private decimal weeksCount = 52;

        [Required]
        public decimal TotalSales { get; set; }

        [Required]
        public decimal AverageOrderValue { get; set; }

        public decimal GetOfflineUsersCount()
        {
            var value = this.TotalSales / this.AverageOrderValue / weeksCount;

            return Math.Round(value, MidpointRounding.AwayFromZero);
        }
    }
}