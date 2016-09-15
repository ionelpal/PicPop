using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PicPop.BusinessLogic;
using PicPop.Models;

namespace PicPop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        
        // GET: Categories
        public ActionResult Index()
        {
            CategoriesHelper categoryHelper = new CategoriesHelper();

            List<CategoriesIndexModel> model = categoryHelper.GetData().Select(x => new CategoriesIndexModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();


            return View(model);
        }

        // GET: Categories/Details/5
        public ActionResult Details(int id)
        {
            CategoriesHelper categoryHelper = new CategoriesHelper();
            Category category = categoryHelper.FindById(id);
            CategoriesEditViewModel model = new CategoriesEditViewModel()
            {
                Name = category.Name
            };

            return View(model);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoriesCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    CategoriesHelper categoryHelper = new CategoriesHelper();

                    if (!categoryHelper.Exits(model.Name))
                    {
                        Category category = new Category()
                        {
                            Name = model.Name.Trim()
                        };
                        categoryHelper.Add(category);
                    }
                    else
                    {
                        //TODO: Look the alerts

                    }


                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int id)
        {
            if (id > 0)
            {
                CategoriesHelper categoryHelper = new CategoriesHelper();
                Category category = categoryHelper.FindById(id);
                CategoriesEditViewModel model = new CategoriesEditViewModel()
                {
                    Id = id,
                    Name = category.Name
                };
                return View(model);
            }

            return RedirectToAction("Index");

        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoriesEditViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    CategoriesHelper categoryHelper = new CategoriesHelper();
                    Category category = categoryHelper.FindById(id);
                    category.Name = model.Name.Trim();
                    
                        categoryHelper.Update(category);
                    

                    return RedirectToAction("Index");
                }
                return View(model);

            }
            catch
            {
                return View();
            }
        }

        //// GET: Categories/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Categories/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
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

        private List<SelectListItem> GetCategoriesSelectedItems()
        {
            var category = new CategoriesHelper();
            var lst = category.GetListItem();

            return lst.Select(x => new SelectListItem()
            {
                Value = x.Value,
                Text = x.Text
            }).ToList();
        }
    }
}
