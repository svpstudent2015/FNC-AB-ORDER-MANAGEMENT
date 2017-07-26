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

    [System.Web.Mvc.Authorize]
    public class KundController : Controller
    {
        // GET: Kund
        public ActionResult Kund()
        {
            KundModel i2 = new KundModel();
           
            var dbKund = new KundRepository();
            
            var lista = dbKund.ShowAll();
            
            foreach (Kund e in lista)
            {
                KundModel i = new KundModel();              
              
                i.Email = e.Email;               
                i.Telefonnr = e.Telefonnr;
                i.Namn = e.Namn;
                i.ID = e.ID;

                i2.KundLista.Add(i);

            }

            return View(i2);
        }
        
        // Skapa en ny kund
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
            
            db.LaggTill(k);

            return RedirectToAction("NyUtsattning", "Utsattningar");
            
        }

        // Get redigera kund
        public ActionResult RedigeraKund(int id)
        {

            KundRepository db = new KundRepository();
            Kund e = db.getKund(id);
            KundModel model = new KundModel();
                      
            model.Email = e.Email;
            model.ID = e.ID;
            model.Telefonnr = e.Telefonnr;
            model.Namn = e.Namn;
          
            return View(model);
            //    return RedirectToAction("RedigeraInmatningar", "Inmatningar", model);
        }

        // Spara redigerad kund
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RedigeraKund(KundModel model)
        {
            Kund i = new Kund();
            var db = new KundRepository();

            i.Namn = model.Namn;
            i.Email = model.Email;
            i.Telefonnr = model.Telefonnr;
            i.ID = model.ID;                      

            db.SparaRedigeraKunder(i);

            return RedirectToAction("Kund", "Kund");
        }
        public ActionResult TaBortKund(int id)
        {
            KundRepository rep = new KundRepository();
            rep.TaBortEnKund(id);

            return RedirectToAction("Kund", "Kund");

        }
    }


}
