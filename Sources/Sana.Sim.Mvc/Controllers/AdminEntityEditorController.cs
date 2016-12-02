using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sana.Sim.Business.Repositories;
using Sana.Sim.Mvc.ViewModels.Admin;

namespace Sana.Sim.Mvc.Controllers
{
    [Authorize]
    public abstract class AdminEntityEditorController<TEntity, TInputModel> : Controller
    {
        protected abstract IEntityRepository<TEntity> Repository { get; }

        protected virtual IEnumerable<TEntity> GetEntities()
        {
            return Repository.Get();
        }
        
        [HttpGet]
        public virtual IActionResult Index()
        {
            var model = new EntityListViewModel<TEntity>
            {
                Entities = GetEntities()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Editor");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TInputModel model)
        {
            if (!ModelState.IsValid)
                return View("Editor", model);

            var entity = Mapper.Map<TEntity>(model);
            Repository.Create(entity);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var model = Mapper.Map<TInputModel>(Repository.Get(id));

            return View("Editor", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TInputModel model)
        {
            if (!ModelState.IsValid)
                return View("Editor", model);

            var entity = Mapper.Map<TEntity>(model);
            Repository.Update(entity);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            Repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
