using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiceWiseData;
using MiceWiseData.Commands;
using MiceWiseData.ViewModel;
using vlko.core.Base;
using vlko.core.Tools;
using vlko.core.Components;
using vlko.core.Repository;

namespace MiceWise.web.Controllers
{
    [HandleError]
    public class HomeController : BaseController
    {
        /// <summary>
        /// URL: Home/Index
        /// </summary>
        /// <returns>Action result.</returns>
        [OutputCache(Duration = 30, VaryByParam = "page")]
        public ActionResult Index(PagedModel<CelebrityView> pageModel)
        {
            pageModel.PageItems = 11;
            pageModel.PrevAsItem = true;
            pageModel.LoadData(RepositoryFactory.Command<ICelebrityCommands>().GetActive().OrderByDescending(item => item.Priority));
            return ViewWithAjax(pageModel);
        }

        /// <summary>
        /// URL: Home/Search
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public ActionResult Search(string query)
        {
            return ViewWithAjax(RepositoryFactory.Command<ICelebrityCommands>().Search(query));
        }

        /// <summary>
        /// URL: Home/Add
        /// </summary>
        public ActionResult Add()
        {
            return ViewWithAjax(new CelebrityRegisterViewModel
                                    {
                                        FileIdent = Guid.NewGuid()
                                    });
        }

        /// <summary>
        /// URL: Home/Add
        /// </summary>
        [HttpPost]
        public ActionResult Add(CelebrityRegisterViewModel model)
        {
            // if we have file upload
            if (Request.Files.Count == 1 && Request.Files[0].ContentLength > 0)
            {
                try
                {
                    ProcessFileUpload(model);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            else if (ModelState.IsValid)
            {
                using (var tran = RepositoryFactory.StartTransaction())
                {
                    MoveUploadedFile(model);
                    RepositoryFactory.Command<ICelebrityCommands>().Register(model);
                    tran.Commit();
                    return ViewWithAjax("Thanks");
                }
            }
            return ViewWithAjax(model);
        }

        private void MoveUploadedFile(CelebrityRegisterViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Picture))
            {
                var fileExtension = Path.GetExtension(model.Picture).ToLower();
                var source = Path.Combine(Upload.UserTempPath, model.FileIdent + fileExtension);
                if (System.IO.File.Exists(source))
                {
                    model.Picture = FriendlyUrlGenerator.Generate(model.Name) + DateTime.UtcNow.ToString("-yyyy-MM-dd_HH-mm-ss") + fileExtension;
                    var destination = Path.Combine(Upload.UserPath, model.Picture);
                    if (!Directory.Exists(Upload.UserPath))
                    {
                        Directory.CreateDirectory(Upload.UserPath);
                    }
                    System.IO.File.Move(source, destination);
                }
            }
        }

        private void ProcessFileUpload(CelebrityRegisterViewModel model)
        {
            var file = Request.Files[0];
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            if (file.ContentLength > Upload.MaxFileSize)
            {
                ModelState.AddModelError("Picture",
                                         string.Format("Picture bigger like {0} MB. Try upload smaller image.", Math.Round(Upload.MaxFileSize / (1024 * 1024d), 2)));
            }
            else if (!Upload.AllowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError("Picture",
                                         string.Format("Unknown image format, try to use one of '{0}'.", string.Join(", ", Upload.AllowedExtensions)));
            }
            else
            {
                Directory.CreateDirectory(Upload.UserTempPath);
                var uploadedFileName = Path.Combine(Upload.UserTempPath, model.FileIdent + fileExtension);
                using (FileStream fileWriteStream = new FileStream(uploadedFileName, FileMode.Create))
                {
                    file.InputStream.CopyTo(fileWriteStream);
                    fileWriteStream.Flush();
                    fileWriteStream.Close();
                }
                model.Picture = fileName + fileExtension;
                ModelState.Clear();
            }
        }
    }
}