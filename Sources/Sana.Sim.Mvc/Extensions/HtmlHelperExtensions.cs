using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Sana.Sim.Mvc.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent FormatPrice(this IHtmlHelper html, decimal value)
        {
            return new StringHtmlContent(Math.Truncate(value).ToString("0"));
        }
    }
}
