using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using PicPop.Models;
using PicPop.BusinessLogic;
using PicPop.BusinessLogic.Models;
using PicPop.BusinessLogic.Utils;

namespace PicPop.Controllers
{
    public class ImageController : Controller
    {
        // GET: Images
        public ActionResult Index(int? id, int? page)
        {
            PicPopImagesHelper imagesHelper = new PicPopImagesHelper();
            List<PicPopImage> lst = imagesHelper.GetShop(User.Identity.GetUserId(), id);

            IEnumerable<ImagesDetailsViewModels> lstImages = lst.Select(x => new ImagesDetailsViewModels()
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Amount = x.Amount,
                Blob = CloudStorageManagerHelper.GetUrlInfo(
                        x.BlobFiles.First(y => y.Container.Equals((int) BlobFileType.ImagesWaterMark))),
                IsMine = true
            }).AsEnumerable();

            
            ImageSearchViewModel model = new ImageSearchViewModel()
            {
                Categories = PicPop.Helper.CategoriesHelper.GetCategoriesSelectedItems(),
                List = lstImages.ToPagedList(page??1, 12),
                Id = id
            };

            return View(model);
        }


        // GET: Images/Details/5
        public ActionResult Details(int id)
        {
            var imageHelper = new PicPopImagesHelper();
            PicPopImage image = imageHelper.Details(id);

            // Check if the image exist and if the image is relationship with the user
            if (image == null) //|| !image.UserId.Equals(User.Identity.GetUserId()))
                return RedirectToAction("Index");

            string userId = User.Identity.GetUserId();
            int containerId = (int) BlobFileType.ImagesWaterMark;
            if (!string.IsNullOrEmpty(userId) &&
                (image.UserId.Equals(userId) ||
                 (image.TransactionItems != null && image.TransactionItems.Any(y=>y.Transaction.UserId.Equals(userId)))))
                containerId = (int) BlobFileType.ImagesOriginal;

            ImagesDetailsViewModels model = new ImagesDetailsViewModels()
            {
                Id = image.Id.ToString(),
                Name = image.Name,
                Amount = image.Amount,
                //Blob = CloudStorageManagerHelper.GetUrlInfo(BlobFileType.ImagesOriginal, image.BlobFiles.First().BlobKey),
                Blob = CloudStorageManagerHelper.GetUrlInfo(image.BlobFiles.First(y => y.Container.Equals(containerId))),
                Category = image.CategoryId.HasValue ? image.Category.Name : "None",
                IsMine = image.UserId.Equals(User.Identity.GetUserId())
            };

            return View(model);
        }


        [Authorize]
        public ActionResult MyImages()
        {
            PicPopImagesHelper images = new PicPopImagesHelper();
            List<PicPopImage> lst = images.GetMyImagesList(User.Identity.GetUserId());

            IEnumerable<ImagesDetailsViewModels> model = lst.Select(x => new ImagesDetailsViewModels()
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Amount = x.Amount,
                Blob = CloudStorageManagerHelper.GetUrlInfo(x.BlobFiles.FirstOrDefault(y=>y.Container.Equals((int)BlobFileType.ImagesOriginal))),
                IsMine = true
            }).AsEnumerable();
            return View(model);
        }

        // GET: Images/Create
        [Authorize]
        public ActionResult Create()
        {
            var model = new ImagesCreateViewModels()
            {
                Categories = PicPop.Helper.CategoriesHelper.GetCategoriesSelectedItems()
            };

            return View(model);
        }


        // POST: Images/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(ImagesCreateViewModels images)
        {
            
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                int categoryId;
                if (!int.TryParse(images.CategoryId, out categoryId))
                    categoryId = 0;
                
                var lstBlobs = new PicPop.Helper.PicPopImagesHelper().CreateImage(userId, images.File);
                
                PicPopImage image = new PicPopImage
                {
                    Name = images.Name,
                    Amount = images.Amount,
                    DtAdded = DateTime.Now,
                    UserId = userId,
                    BlobFiles = lstBlobs,
                    CategoryId = categoryId == 0 ? (int?) null : categoryId,
                    CreatedBy = userId
                };

                var imageHelper = new PicPopImagesHelper();
                imageHelper.Add(image);

                return RedirectToAction("MyImages");
            }

            images.Categories = PicPop.Helper.CategoriesHelper.GetCategoriesSelectedItems();
            return View(images);
        }

        // GET: Images/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var imageHelper = new PicPopImagesHelper();
            PicPopImage image = imageHelper.Details(id);

            // Check if the image exist and if the image is relationship with the user
            if (image == null || !image.UserId.Equals(User.Identity.GetUserId()))
                return RedirectToAction("Index");

            ImagesEditViewModels model = new ImagesEditViewModels()
            {
                Name = image.Name,
                Amount = image.Amount,
                CategoryId = image.CategoryId.ToString(),
                Categories = PicPop.Helper.CategoriesHelper.GetCategoriesSelectedItems(),
                //Blob = CloudStorageManagerHelper.GetUrlInfo(BlobFileType.ImagesOriginal, image.BlobFiles.First().BlobKey)
                Blob = CloudStorageManagerHelper.GetUrlInfo(image.BlobFiles.First())
            };

            return View(model);
        }

        // POST: Images/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(int id, ImagesEditViewModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int categoryId;
                    var imageHelper = new PicPopImagesHelper();
                    PicPopImage image = imageHelper.FindById(id);

                    // Check if the image exist and if the image is relationship with the user
                    if (image == null || !image.UserId.Equals(User.Identity.GetUserId()))
                        return RedirectToAction("Index");

                    image.Name = model.Name;
                    image.Amount = model.Amount;
                    image.DtModified = DateTime.Now;
                    image.UserId = User.Identity.GetUserId();

                    if (!int.TryParse(model.CategoryId, out categoryId))
                        categoryId = 0;
                    image.CategoryId = categoryId == 0 ? (int?) null : categoryId;

                    imageHelper.Update(image);

                    return RedirectToAction("MyImages");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }



        //// GET: Images/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Images/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}