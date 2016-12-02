using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities;

namespace Sana.Sim.Mvc.ViewModels.Dashboard
{
    public class ReportViewModel
    {
        public IEnumerable<WebshopReportViewModel> WebshopReports { get; set; }
    }

    public class WebshopReportViewModel
    {
        public string WebshopName { get; set; }

        public bool WentLive { get; set; }

        public IEnumerable<string> FinishedFeatures { get; set; }

        public IEnumerable<string> RunningActions { get; set; }

        public string InProgressFeature { get; set; }

        public decimal ConvertedUsersCountDifference { get; set; }

        public decimal NewUsersCountDifference { get; set; }

        public decimal AverageOrderAmountDifference { get; set; }

        public bool IsEmpty()
        {
            if (WentLive)
                return false;

            if (FinishedFeatures.Any())
                return false;

            if (RunningActions.Any())
                return false;

            if (!string.IsNullOrEmpty(InProgressFeature))
                return false;

            if (ConvertedUsersCountDifference > 0)
                return false;

            if (NewUsersCountDifference > 0)
                return false;

            if (AverageOrderAmountDifference > 0)
                return false;

            return true;
        }
    }
}
