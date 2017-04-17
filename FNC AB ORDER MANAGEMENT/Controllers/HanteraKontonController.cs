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
using System.Web.Http;
using DAL;
using FNC_AB_ORDER_MANAGEMENT.Models;
using DAL.RepositoryFolder;
namespace FNC_AB_ORDER_MANAGEMENT.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "Admin")]
    public class HanteraKontonController : Controller
    {
        public ActionResult HanteraKonton()
        {
            return View();
        }

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

        public ActionResult RedigeraAnvandare(String id)
        {

            var db = new AnvandareRepository();
            AspNetUsers e = db.ShowRowByID(id);
            AnvandareModel model = new AnvandareModel();

            model.Email = e.Email;
            model.UserName = e.UserName;
         

            return View(model);
            //    return RedirectToAction("RedigeraInmatningar", "Inmatningar", model);
        }

    }


    }
