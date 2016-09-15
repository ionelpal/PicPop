using PicPop.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PicPop.Models;

namespace PicPop.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profiles
        public ActionResult Index()
        {
            var user = User.Identity.GetUser(true);
            var profile = user.Profile;

            if (profile == null)
            {
                return RedirectToAction("Create", "Profile");
            }

            
            ProfilesDetailsViewModels model = new ProfilesDetailsViewModels()
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Address = profile.Address,
                DoB = profile.DOB,
                Gender = (GenderType)Enum.ToObject(typeof(GenderType), profile.GenderId),
                Telephone = profile.Telephone
            };
            
            
            return View(model);
        }

        // GET: Profiles/Details/5
        public ActionResult Details()
        {
            var user = User.Identity.GetUser(true);

            var profile = user.Profile;
            if (profile == null)
            {
                return RedirectToAction("Create", "Profile");
            }
            ProfilesDetailsViewModels model = new ProfilesDetailsViewModels()
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Address = profile.Address,
                DoB = profile.DOB,
                Gender = (GenderType)Enum.ToObject(typeof(GenderType), profile.GenderId),
                Telephone = profile.Telephone
            };
            var profiles = new ProfilesHelper();
            profiles.Update(profile);
            return View(model);
        }

        // GET: Profiles/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Profiles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProfilesEditViewModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    var profiles = new ProfilesHelper();
                    var profile = new Profile()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Address = model.Address,
                        UserId = User.Identity.GetUserId(),
                        DOB = model.DoB,
                        GenderId = (int)model.Gender,
                        Telephone = model.Telephone
                    };

                    profiles.Add(profile);
                    return RedirectToAction("Details");
                }
                return View(model);
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Profiles/Edit/5
        public ActionResult Edit()
        {
            var user = User.Identity.GetUser(true);
            var profile = user.Profile;
            if (profile == null)
            {
                return RedirectToAction("Create", "Profile");
            }
            ProfilesEditViewModels model = new ProfilesEditViewModels()
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Address = profile.Address,
                DoB = profile.DOB,
                Gender = (GenderType)Enum.ToObject(typeof(GenderType), profile.GenderId),
                Telephone = profile.Telephone

            };
            return View(model);
        }

        // POST: Profiles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfilesEditViewModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var profiles = new ProfilesHelper();

                    var profile = new Profile()
                    {
                        UserId = User.Identity.GetUserId(),
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Address = model.Address,
                        DOB = model.DoB,
                        GenderId = (int)model.Gender,
                        Telephone = model.Telephone
                    };
                    profiles.Update(profile);

                    return RedirectToAction("Details");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        //// GET: Profiles/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Profiles/Delete/5
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
