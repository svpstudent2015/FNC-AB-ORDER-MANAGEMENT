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
    public class UtsattningarController : Controller
    {
        // GET: Utsattningar
        public ActionResult Utsattningar()
        {
            UtsattningarModel i2 = new UtsattningarModel();

            var db = new UtsattningarRepository();
            var listAll = db.ShowAll();

            foreach (Utsattningar e in listAll)
            {
                UtsattningarModel i = new UtsattningarModel();

                if (e.InDatum != null)
                {
                    i.InDatum = (DateTime)e.InDatum;

                }
                if (e.UtDatum != null)
                {
                    i.UtDatum = (DateTime)e.UtDatum;
                }

                i.ID = e.ID;
                i.AID = e.AID;

                i.Ordernr = e.Ordernr;
           //     i.Kund = e.Kund;
                i.Telefonnr = e.Telefonnr;
                i.Ort = e.Ort;
                i.Adress = e.Adress;
                //i.Fakturerad = (bool)e.Fakturerad;
                i.Ovrigt = e.Ovrigt;
                i.Status = e.Status;

                i2.UtsattningsLista.Add(i);


            }


            return View(i2);
        }
        public ActionResult NyUtsattning()
        {
            KundRepository k = new KundRepository();
            List<Kund>klista = k.ShowAll();
            UtsattningarModel umodel = new UtsattningarModel();


            foreach (var kund in klista )
            {
                KundModel km= new KundModel();
                km.Namn = kund.Namn;
                km.Telefonnr = kund.Telefonnr;
                km.Email = kund.Email;
                km.ID = kund.ID;

                umodel.KundLista.Add(km);
            }
            

            return View(umodel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult NyUtsattning(UtsattningarModel model)
        {
            if (!ModelState.IsValid)
                return View();

            else
            {
                Utsattningar i = new Utsattningar();

                i.Ordernr = model.Ordernr;
            //    i.Kund = model.Kund;
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
                i.GPS = model.GPS;

                var db = new UtsattningarRepository();
                db.Add(i);
                return RedirectToAction("Utsattningar", "Utsattningar");


            }

        }

        // Metod för att visa en inmätning med hjälp av ID
        public ActionResult RedigeraUtsattningar(int id)
        {

            var db = new UtsattningarRepository();
            Utsattningar e = db.ShowRowByID(id);
            UtsattningarModel model = new UtsattningarModel();

            model.Ordernr = e.Ordernr;
         //   model.Kund = e.Kund;
            model.Telefonnr = e.Telefonnr;
            model.Ort = e.Ort;
            model.Adress = e.Adress;
            model.InDatum = e.InDatum;
            model.UtDatum = e.UtDatum;
            model.StyckPris = e.StyckPris;
            model.Langd = e.Langd;
            model.Timmar = e.Timmar;
            model.Fakturerad = e.Fakturerad;
            model.Ovrigt = e.Ovrigt;
            model.Status = e.Status;
            model.ID = e.ID;
            model.GPS = e.GPS;
           

            return View(model);
            //    return RedirectToAction("RedigeraInmatningar", "Inmatningar", model);
        }

        // Metod för att ändra och spara inmätningar

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RedigeraUtsattningar(UtsattningarModel model)
        {
            Utsattningar i = new Utsattningar();
            var db = new UtsattningarRepository();

            i.Ordernr = model.Ordernr;
        //    i.Kund = model.Kund;
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
            i.ID = model.ID; 
            i.GPS = model.GPS;

            db.SparaRedigeraUtsattningar(i);

            return RedirectToAction("Utsattningar", "Utsattningar");
        }
    }
}