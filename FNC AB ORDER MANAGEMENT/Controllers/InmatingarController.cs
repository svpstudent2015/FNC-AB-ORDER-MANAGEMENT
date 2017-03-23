using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FNC_AB_ORDER_MANAGEMENT.Models;
using DAL.RepositoryFolder;
using DAL;

namespace FNC_AB_ORDER_MANAGEMENT.Controllers
{
    public class InmatningarController : Controller
    {
        // GET: Inmatningar
        public ActionResult Inmatningar()
        {
            InmatningarModel i2 = new InmatningarModel();
            
            var db = new InmatningarRepository();
            var listAll = db.ShowAll();

            foreach(INMATNINGAR e in listAll)
            {
                InmatningarModel i = new InmatningarModel();

                //DateTime UpdatedDate = e.InDatum.UpdatedDate.Get
                // DateTime.TryParse(reader[e.InDatum]),out date1);

                i.Ordernr = e.Ordernr;
                i.Kund = e.Kund;
                i.Telefonnr = e.Telefonnr;
                i.Ort = e.Ort;
                i.Adress = e.Adress;
                //i.InDatum = (DateTime) e.InDatum;
                //i.UtDatum = (DateTime) e.UtDatum;
                //i.StyckPris = (bool) e.StyckPris;
                //i.Langd = (decimal) e.Langd;
                //i.Timmar = (decimal) e.Timmar;
                //i.Fakturerad = (bool)e.Fakturerad;
                i.Ovrigt = e.Ovrigt;
                i.Status = e.Status;

                i2.InmatningsLista.Add(i);


            }


            return View(i2);
        }
        public ActionResult NyInmatning()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult NyInmatning(InmatningarModel model)
        {
            if (!ModelState.IsValid)
                return View();

            else
            {
                INMATNINGAR i = new INMATNINGAR();

                i.Ordernr = model.Ordernr;
                i.Kund = model.Kund;
                i.Telefonnr = model.Telefonnr;
                i.Ort = model.Ort;
                i.Adress = model.Adress;
                i.InDatum = model.InDatum;
                i.UtDatum = model.UtDatum;
                i.StyckPris = model.StyckPris;
                i.Langd = model.Langd;
                i.Timmar = model.Timmar;
                i.Fakturerad = model.Fakturerad;
                i.Ovrigt = model.Ovrigt;
                i.Status = model.Status;

                var db = new InmatningarRepository();
                db.Add(i);
                return RedirectToAction("Inmatningar", "Inmatningar");


            }

        }
      
     
    }
}