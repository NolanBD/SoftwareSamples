using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ShackUp.Data.Factory;
using ShackUp.Models.Tables;
using ShackUp.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShackUp.UI.Controllers
{
    public class ListingsController : Controller
    {
        private string GetUserID()
        {
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = userMgr.FindByName(User.Identity.Name);

            return user.Id;
        }

        public ActionResult Details(int id)
        {

            if (Request.IsAuthenticated)
            {
                ViewBag.UserId = GetUserID();
            }

            var repo = ListingRepositoryFactory.GetRepository();
            var model = repo.GetDetails(id);

            return View(model);
        }

        public ActionResult Index()
        {
            var repo = StatesRepositoryFactory.GetRepository();

            return View(repo.GetAll());
        }

        [Authorize]
        public ActionResult Add()
        {
            var model = new ListingAddViewModel();

            var statesRepo = StatesRepositoryFactory.GetRepository();
            var bathroomRepo = BathroomTypesRepositoryFactory.GetRepository();

            model.States = new SelectList(statesRepo.GetAll(), "StateID", "StateID");
            model.BathroomTypes = new SelectList(bathroomRepo.GetAll(), "BathroomTypeID", "BathroomTypeName");
            model.Listing = new Listing();

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(ListingAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = ListingRepositoryFactory.GetRepository();

                try
                {
                    model.Listing.UserID = GetUserID();
                    repo.Insert(model.Listing);
                }
                catch(Exception ex)
                {
                    throw ex;
                }

                var statesRepo = StatesRepositoryFactory.GetRepository();
                var bathroomRepo = BathroomTypesRepositoryFactory.GetRepository();

                model.States = new SelectList(statesRepo.GetAll(), "StateID", "StateID");
                model.BathroomTypes = new SelectList(bathroomRepo.GetAll(), "BathroomTypeID", "BathroomTypeName");
                model.Listing = new Listing();

                return View(model);
            }

            return RedirectToAction("Edit", new { id = model.Listing.ListingID });
        }
    }
}