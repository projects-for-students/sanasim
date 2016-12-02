using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Entities.Features
{
    public class Feature
    {
        public Guid Id { get; set; }

        public string Tag { get; set; }

        public string Name { get; set; }

        public Guid? ParentId { get; set; }

        public string Type { get; set; }

        public decimal ImplementationRequirements { get; set; }

        public decimal ImplementationCost { get; set; }

        public decimal? Duration { get; set; }

        public decimal? OfflineUsersConversionRate { get; set; }

        public decimal? NewOnlineUsersAmount { get; set; }

        public decimal? AverageOrderAmountIncreaseRate { get; set; }

        public bool IsSystem =>
            string.Equals(Type, BusinessConstants.FeatureTypes.Webshop);

        public bool IsFunctionality =>
            string.Equals(Type, BusinessConstants.FeatureTypes.Functionality);

        public bool IsAction =>
            string.Equals(Type, BusinessConstants.FeatureTypes.Action);

        public string ImpactMessage
        {
            get
            {
                var details = new StringBuilder();

                if (IsAction)
                    details.AppendFormat("Duration: {0}", Duration);
                else
                    details.AppendFormat("TE: {0}", ImplementationRequirements);

                if (OfflineUsersConversionRate.HasValue && OfflineUsersConversionRate.Value != 0)
                    details.AppendFormat("; Converts {0}% of offline users to online", OfflineUsersConversionRate);

                if (NewOnlineUsersAmount.HasValue && NewOnlineUsersAmount.Value != 0)
                    details.AppendFormat("; Brings {0}% of new online users", NewOnlineUsersAmount);

                if (AverageOrderAmountIncreaseRate.HasValue && AverageOrderAmountIncreaseRate != 0)
                    details.AppendFormat("; Increases average order by {0}%", AverageOrderAmountIncreaseRate);

                return details.ToString();
            }
        }
    }
}
