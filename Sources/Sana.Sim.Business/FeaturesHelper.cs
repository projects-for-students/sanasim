using Sana.Sim.Business.Entities.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business
{
    public class FeaturesHelper
    {
        private readonly IEnumerable<Feature> features;

        public FeaturesHelper()
        {
            this.features = Framework.Features.Get();
        }

        public IEnumerable<Feature> Features
            => features;

        private Feature Find(Guid id)
        {
            return features.Single(f => f.Id == id);
        }

        public List<Feature> GetParentFeatures(Guid id)
        {
            var feature = Find(id);
            var list = new List<Feature>();

            while (feature.ParentId.HasValue)
            {
                feature = Find(feature.ParentId.Value);
                list.Add(feature);
            }

            return list;
        }

        public bool IsChildFor(Guid id, Guid? parentId)
        {
            if (!parentId.HasValue)
                return false;

            if (id == parentId)
                return true;

            var parentFeatures = GetParentFeatures(id);
            return parentFeatures.Any(f => f.Id == parentId);
        }

        public IEnumerable<Feature> GetPossibleParentFeatures(Guid? id)
        {
            if (!id.HasValue)
                return features;

            var list = features.Where(f => !IsChildFor(f.Id, id)).ToList();
            return list;
        }
    }
}
