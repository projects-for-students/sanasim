using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sana.Sim.Business.Entities.Features;

namespace Sana.Sim.Mvc.Extensions
{
    public static class FeatureExtensions
    {
        public static SelectListItem ToSelectListItem(this Feature feature) =>
            new SelectListItem
            {
                Value = feature.Id.ToString(),
                Text = feature.Name
            };

        public static IEnumerable<SelectListItem> ToSelectList(this IEnumerable<Feature> features) =>
            features.Select(ToSelectListItem).ToList();
    }
}
