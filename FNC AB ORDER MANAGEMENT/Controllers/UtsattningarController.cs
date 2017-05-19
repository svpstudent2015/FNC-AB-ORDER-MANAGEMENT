using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FNC_AB_ORDER_MANAGEMENT.Models;
using DAL.RepositoryFolder;
using DAL;
using Microsoft.AspNet.Identity;

namespace FNC_AB_ORDER_MANAGEMENT.Controllers
{
    public class UtsattningarController : Controller
    {
        // GET: Utsattningar
        //Andropas med önskad satus på utsättningar.
        //Retunerar vyn utsattning tillsammans med en modellen innehållandes
        //en lista av utsattningar
        public ActionResult Utsattningar(string sta)
        {
            UtsattningarModel uModel = new UtsattningarModel();
            uModel.Status = sta;
            var dbUtsattningar = new UtsattningarRepository();
            var dbKund = new KundRepository();
            var utsattningsLista = dbUtsattningar.ShowAll();
            var kundLista = dbKund.ShowAll();

            var nyUtsattningsLista = from u in utsattningsLista
                join k in kundLista
                on u.KundID equals k.ID
                where u.Status == sta
                select new UtsattningarModel()
                {
                    KundNamn = k.Namn,
                    Status = u.Status,
                    Adress = u.Adress,
                    Ordernr = u.Ordernr,
                    InDatum = u.InDatum,
                    ID = u.ID,
                    Ovrigt = u.Ovrigt

        };

            foreach (UtsattningarModel uM in nyUtsattningsLista)
            {
               
                uModel.UtsattningsLista.Add(uM);

            }


            return View(uModel);
        }
        //Retunerar vyn NyUtsattning med en modell inhållandes en
        //lista av Kunder
        public ActionResult NyUtsattning()
        {
            KundRepository dbKund = new KundRepository();
            List<Kund>klista = dbKund.ShowAll();
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
                return View(model);

            else
            {
                String anvandarId = User.Identity.GetUserId();
               
                
                Utsattningar i = new Utsattningar();

                i.Ordernr = model.Ordernr;
                i.KundID = model.KundID;
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
                i.AID = anvandarId;

                var dbUtsattningar = new UtsattningarRepository();
                dbUtsattningar.Add(i);
                return RedirectToAction("Utsattningar", new { sta = i.Status });

            }

        }

        // Metod för att visa en inmätning med hjälp av ID
        public ActionResult RedigerUtsattningar(int id)
        {

            var dbUtsattningar = new UtsattningarRepository();
            Utsattningar e = dbUtsattningar.HamtaEnUsattning(id);
            UtsattningarModel uModel = new UtsattningarModel();
            KundRepository dbKund = new KundRepository();
            AnvandareRepository dbAnvandare = new AnvandareRepository();

            List<Kund> klista = dbKund.ShowAll();

            AspNetUsers anvandare = dbAnvandare.HamtaEnAnvandareMedId(e.AID);


            foreach (var kund in klista)
            {
                KundModel km = new KundModel();
                km.Namn = kund.Namn;
                km.Telefonnr = kund.Telefonnr;
                km.Email = kund.Email;
                km.ID = kund.ID;

                uModel.KundLista.Add(km);
            }

            uModel.Ordernr = e.Ordernr;
            uModel.KundID = e.KundID;
            uModel.Telefonnr = e.Telefonnr;
            uModel.Ort = e.Ort;
            uModel.Adress = e.Adress;
            uModel.InDatum = e.InDatum;
            uModel.UtDatum = e.UtDatum;
            uModel.StyckPris = e.StyckPris;
            uModel.Langd = e.Langd;
            uModel.Timmar = e.Timmar;
            uModel.Fakturerad = e.Fakturerad;
            uModel.Ovrigt = e.Ovrigt;
            uModel.Status = e.Status;
            uModel.ID = e.ID;
            uModel.GPS = e.GPS;

            if (anvandare !=null) {
                uModel.Anvandrare = anvandare.Email;
            }
            



            return View(uModel);
            
        }

        // Metod för att ändra och sparaa utsättningar
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RedigerUtsattningar(UtsattningarModel model)
        {
            Utsattningar i = new Utsattningar();
            var dbUtsattningar = new UtsattningarRepository();

            i.Ordernr = model.Ordernr;
            i.KundID = model.KundID;
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

            dbUtsattningar.SparaRedigeraUtsattningar(i);

            return RedirectToAction("Utsattningar", new { sta = i.Status });
        }

        public ActionResult TaBortUtsattning(int id)
        {

            UtsattningarRepository dbUtsattningar = new UtsattningarRepository();
            DAL.Utsattningar uts = dbUtsattningar.HamtaEnUsattning(id);
            dbUtsattningar.TaBortEnUtsattning(id);

           
            return RedirectToAction("Utsattningar", new { sta = uts.Status });

        }
    }
}