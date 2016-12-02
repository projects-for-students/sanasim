using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sana.Sim.Business.Entities.Resources;

namespace Sana.Sim.Mvc.Extensions
{
    public static class ServerExtensions
    {
        public static SelectListItem ToSelectListItem(this Server entity) =>
             new SelectListItem
             {
                 Value = entity.Id.ToString(),
                 Text = $"{entity.Name} (Cost: {entity.Cost}, Capacity: {entity.Capacity})"
             };

        public static IEnumerable<SelectListItem> ToSelectList(this IEnumerable<Server> entities) =>
            entities.Select(ToSelectListItem).ToList();
    }
}
