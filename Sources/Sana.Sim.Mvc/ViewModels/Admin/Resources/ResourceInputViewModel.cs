using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities.Features;

namespace Sana.Sim.Mvc.ViewModels.Admin.Resources
{
    public abstract class ResourceInputViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Capacity { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Cost { get; set; }
    }
}