using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sana.Sim.Business.Entities.Resources;
using Sana.Sim.Business.Repositories;
using Sana.Sim.Mvc.ViewModels.Admin.Resources;

namespace Sana.Sim.Mvc.Controllers
{
    [Authorize]
    public class AdminServersController : AdminEntityEditorController<Server, ServerInputViewModel>
    {
        protected override IEntityRepository<Server> Repository =>
            Framework.Servers;
    }
}
