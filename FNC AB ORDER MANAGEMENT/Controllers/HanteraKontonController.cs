using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Globalization;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Web.Http;
using DAL;
using FNC_AB_ORDER_MANAGEMENT.Models;
using DAL.RepositoryFolder;
using Microsoft.AspNet.Identity.EntityFramework;





namespace FNC_AB_ORDER_MANAGEMENT.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "Admin")]
    public class HanteraKontonController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        public HanteraKontonController()
        {
            
        }
        public HanteraKontonController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // Get Hantera Konton
        public ActionResult HanteraKonton()
        {
            return View();
        
        }
        
        // Get användare
        public ActionResult VisaAnvandare()
        {

            AnvandareModel i2 = new AnvandareModel();

            var db = new AnvandareRepository();
            var listAll = db.ShowAll();

            foreach (AspNetUsers e in listAll)
            {
                AnvandareModel i = new AnvandareModel();
             
                i.UserName = e.UserName;
                i.PasswordHash = e.PasswordHash;

                i.Email = e.Email;
                i.Id = e.Id;

                i2.AnvandareLista.Add(i);
            }
            return View(i2);
        }

        // Get redigera användare
        public ActionResult RedigeraAnvandare(String id)
        {

            var db = new AnvandareRepository();
            AspNetUsers e = db.HamtaEnAnvandareMedId(id);

            AnvandareModel model = new AnvandareModel();

            bool admin=db.ShowUserRoll(e);

            if (admin==true)
            {
                model.Roll = true;
            }

            model.Email = e.Email;
            model.UserName = e.UserName;
            model.Id = e.Id;

            return View(model);
            //    return RedirectToAction("RedigeraInmatningar", "Inmatningar", model);
        }

        // Spara redigerad användare
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RedigeraAnvandare(AnvandareModel model)
        {
            AspNetUsers i = new AspNetUsers();
            var db = new AnvandareRepository();
            bool admin;

            i.Email = model.Email;
            admin = model.Roll;
            
            db.SparaRedigeraAnvandare(i,admin);

            return RedirectToAction("VisaAnvandare", "HanteraKonton");
        }

        public ActionResult TaBortAnvandare(string id)
        {
            AnvandareRepository rep = new AnvandareRepository();
            rep.TaBortEnAnvandare(id);
            
            return RedirectToAction("VisaAnvandare", "HanteraKonton");

        }


        //[ChildActionOnly]
        public ActionResult PartialPasswordChange(string id)
        {
            AdminChangePasswordModel acpm=new AdminChangePasswordModel();
            acpm.Id = id;
            //stuff you need and then return the partial view 
            //I recommend using "" quotes for a partial view
            return PartialView("changePassword2",acpm);
        }

        // Metod för att byta en annan användares lösenord
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> changePassword2(AdminChangePasswordModel usermodel)
        {

            if (!ModelState.IsValid)
            {
                
                return PartialView("changePassword2", usermodel);
            }

            ApplicationUser user = await UserManager.FindByIdAsync(usermodel.Id);
            if (user == null)
            {
                return View();
            }
            user.PasswordHash = UserManager.PasswordHasher.HashPassword(usermodel.NewPassword);
            var result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                //throw exception......
            }
            return RedirectToAction("RedigeraAnvandare/" + usermodel.Id, "HanteraKonton");
        }
        //ApplicationDbContext context = new ApplicationDbContext();

        //       internal void AddUserToRole(string userName, string roleName)
        //       {
        //           var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

        //           try
        //           {
        //               var user = UserManager.FindByName(userName);
        //               UserManager.AddToRole(user.Id, roleName);
        //               context.SaveChanges();
        //           }
        //           catch
        //           {
        //               throw;
        //           }
        //       }
    }


}






    
