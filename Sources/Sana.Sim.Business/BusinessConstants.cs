using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business
{
    public static class BusinessConstants
    {
        public static class FeatureTypes
        {
            public static readonly string Action = "Action";

            public static readonly string Functionality = "Functionality";

            public static readonly string Webshop = "Webshop";
        }

        public static class WebshopFeatureId
        {
            public static readonly Guid Standard = new Guid("a4835c63-36e3-4c1b-b8df-b6fa83c24f75");
        }

        public static class DeveloperId
        {
            public static readonly Guid OneDay = new Guid("88C4E9FD-9B74-4E8A-A51E-5DE23E39799E");
        }

        public static class ServerId
        {
            public static readonly Guid D1V2 = new Guid("3CCA1AAF-C4C1-497B-8223-9C3DF8FBB0A3");
        }

        public static readonly decimal DefaultDeveloperCapacity = 60;

        public static readonly decimal PricePerHour = 125;

        public static readonly decimal RequiredServerCapacityPerUser = 0.001m;
    }
}
