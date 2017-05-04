using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.RepositoryFolder;
using FNC_AB_ORDER_MANAGEMENT.Models;

namespace FNC_AB_ORDER_MANAGEMENT.Controllers
{
    public class KundController : Controller
    {
        // GET: Kund
        public ActionResult NyKund()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult NyKund(KundModel model)
        {
            var db = new KundRepository();
            Kund k = new Kund();

            k.Namn = model.Namn;
            k.Email = model.Email;
            k.Telefonnr = model.Telefonnr;

            db.Add(k);

            return RedirectToAction("NyUtsattning", "Utsattningar");
            
        }


    }
}