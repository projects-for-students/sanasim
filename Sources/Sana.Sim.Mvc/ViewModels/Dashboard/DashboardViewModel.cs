using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities;

namespace Sana.Sim.Mvc.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public IEnumerable<Webshop> Webshops { get; set; }

        public ReportViewModel Report { get; set; }
    }
}
