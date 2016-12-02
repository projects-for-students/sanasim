using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sana.Sim.Business.Entities.Features;
using Sana.Sim.Business.Repositories;
using Sana.Sim.Mvc.ViewModels.Admin.Features;
using Sana.Sim.Business;

namespace Sana.Sim.Mvc.Controllers
{
    [Authorize]
    public class AdminActionsController : AdminEntityEditorController<Feature, ActionInputViewModel>
    {
        protected override IEntityRepository<Feature> Repository =>
            Framework.Features;

        protected override IEnumerable<Feature> GetEntities()
        {
            return base.GetEntities().Where(f => f.IsAction).ToList();
        }        
    }
}
