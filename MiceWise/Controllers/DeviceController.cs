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
using vlko.core.ValidationAtribute;

namespace MiceWise.Controllers
{
    [Authorize]
    public class DeviceController : BaseController
    {
        /// <summary>
        /// URL: Devices/Index
        /// </summary>
        public ActionResult Index(PagedModel<Device> pageModel)
        {
            pageModel.LoadData(RepositoryFactory.Command<IDeviceCommands>().GetAll());
            return ViewWithAjax(pageModel);
        }


        /// <summary>
        /// URL: Devices/Create
        /// </summary>
        public ActionResult Create()
        {
            return ViewWithAjax(new Device());
        }

        /// <summary>
        /// URL: Devices/Create
        /// </summary>
        /// <param name="model">The model.</param>
        [HttpPost]
        public ActionResult Create(Device model)
        {
            if (ModelState.IsValid)
            {
                using (var tran = RepositoryFactory.StartTransaction())
                {
                    RepositoryFactory.Command<IDeviceCommands>().Create(model);
                    tran.Commit();
                    SessionFactory.WaitForStaleIndexes();
                    return RedirectToActionWithAjax("Index");
                }
            }
            return ViewWithAjax(model);
        }

        /// <summary>
        /// URL: Device/Edit
        /// </summary>
        /// <param name="id">The id.</param>
        public ActionResult Edit(string id)
        {
            var model = RepositoryFactory.Command<IDeviceCommands>().FindByPk("devices/" + id);
            return ViewWithAjax(model);
        }

        /// <summary>
        /// URL: Devices/Edit
        /// </summary>
        /// <param name="model">The model.</param>
        [HttpPost]
        public ActionResult Edit(Device model)
        {
            if (ModelState.IsValid)
            {
                using (var tran = RepositoryFactory.StartTransaction())
                {
                    model.Id = "devices/" + model.Id;
                    RepositoryFactory.Command<IDeviceCommands>().Update(model);
                    tran.Commit();
                    SessionFactory.WaitForStaleIndexes();
                    return RedirectToActionWithAjax("Index");
                }
            }
            return ViewWithAjax(model);
        }

        /// <summary>
        /// URL: Device/Delete
        /// </summary>
        /// <param name="id">The id.</param>
        public ActionResult Delete(string id)
        {
            var model = RepositoryFactory.Command<IDeviceCommands>().FindByPk("devices/" + id);
            return ViewWithAjax(model);
        }

        /// <summary>
        /// URL: Devices/Delete
        /// </summary>
        /// <param name="model">The model.</param>
        [HttpPost]
        public ActionResult Delete(Device model)
        {
            if (ModelState.IsValid)
            {
                using (var tran = RepositoryFactory.StartTransaction())
                {
                    var itemToDelete = RepositoryFactory.Command<IDeviceCommands>().FindByPk("devices/" + model.Id);
                    RepositoryFactory.Command<IDeviceCommands>().Delete(itemToDelete);
                    tran.Commit();
                    SessionFactory.WaitForStaleIndexes();
                    return RedirectToActionWithAjax("Index");
                }
            }
            return ViewWithAjax(model);
        }
    }
}
