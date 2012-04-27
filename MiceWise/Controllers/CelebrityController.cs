using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiceWiseData.Commands;
using MiceWiseData.Roots;
using vlko.core.Base;
using vlko.core.Components;
using vlko.core.RavenDB.Repository;
using vlko.core.Repository;

namespace MiceWise.Controllers
{
    [Authorize]
    public class CelebrityController : BaseController
    {
        /// <summary>
        /// URL: Celebrity/Index
        /// </summary>
        public ActionResult Index(PagedModel<Celebrity> pageModel)
        {
            pageModel.LoadData(RepositoryFactory.Command<ICelebrityCommands>().GetAll().OrderByDescending(item => item.Name));
            return ViewWithAjax(pageModel);
        }


        /// <summary>
        /// URL: Celebrity/Create
        /// </summary>
        public ActionResult Create()
        {
            ViewBag.Devices = RepositoryFactory.Command<IDeviceCommands>().GetAll().OrderBy(item => item.Name).ToArray();
            return ViewWithAjax(new Celebrity());
        }

        /// <summary>
        /// URL: Celebrity/Create
        /// </summary>
        /// <param name="model">The model.</param>
        [HttpPost]
        public ActionResult Create(Celebrity model)
        {
            if (ModelState.IsValid)
            {
                using (var tran = RepositoryFactory.StartTransaction())
                {
                    RepositoryFactory.Command<ICelebrityCommands>().Create(model);
                    tran.Commit();
                    SessionFactory.WaitForStaleIndexes();
                    return RedirectToActionWithAjax("Index");
                }
            }
            ViewBag.Devices = RepositoryFactory.Command<IDeviceCommands>().GetAll().OrderBy(item => item.Name).ToArray();
            return ViewWithAjax(model);
        }

        /// <summary>
        /// URL: Celebrity/Edit
        /// </summary>
        /// <param name="id">The id.</param>
        public ActionResult Edit(string id)
        {
            var model = RepositoryFactory.Command<ICelebrityCommands>().FindByPk("celebrities/" + id);
            ViewBag.Devices = RepositoryFactory.Command<IDeviceCommands>().GetAll().OrderBy(item => item.Name).ToArray();
            return ViewWithAjax(model);
        }

        /// <summary>
        /// URL: Celebrity/Edit
        /// </summary>
        /// <param name="model">The model.</param>
        [HttpPost]
        public ActionResult Edit(Celebrity model)
        {
            if (ModelState.IsValid)
            {
                using (var tran = RepositoryFactory.StartTransaction())
                {
                    model.Id = "celebrities/" + model.Id;
                    RepositoryFactory.Command<ICelebrityCommands>().Update(model);
                    tran.Commit();
                    SessionFactory.WaitForStaleIndexes();
                    return RedirectToActionWithAjax("Index");
                }
            }
            ViewBag.Devices = RepositoryFactory.Command<IDeviceCommands>().GetAll().OrderBy(item => item.Name).ToArray();
            return ViewWithAjax(model);
        }

        /// <summary>
        /// URL: Celebrity/Delete
        /// </summary>
        /// <param name="id">The id.</param>
        public ActionResult Delete(string id)
        {
            var model = RepositoryFactory.Command<ICelebrityCommands>().FindByPk("celebrities/" + id);
            return ViewWithAjax(model);
        }

        /// <summary>
        /// URL: Celebrity/Delete
        /// </summary>
        /// <param name="model">The model.</param>
        [HttpPost]
        public ActionResult Delete(Celebrity model)
        {
            using (var tran = RepositoryFactory.StartTransaction())
            {
                var itemToDelete = RepositoryFactory.Command<ICelebrityCommands>().FindByPk("celebrities/" + model.Id);
                RepositoryFactory.Command<ICelebrityCommands>().Delete(itemToDelete);
                tran.Commit();
                SessionFactory.WaitForStaleIndexes();
                return RedirectToActionWithAjax("Index");
            }
            return ViewWithAjax(model);
        }
    }
}
