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
    public class InmatningarController : Controller
    {
        // GET: Inmatningar
        public ActionResult Inmatningar(string st, string kNamn, bool? exp, DateTime? inDat, DateTime? utDat)
        {
            InmatningarModel i2 = new InmatningarModel();
            i2.Status = st;
            i2.KundNamn = kNamn;
            i2.Exporterad = exp;
            i2.InDatum = inDat;
            i2.UtDatum = utDat;

            var dbInmatningar = new InmatningarRepository();            
            var dbKund = new KundRepository();
            var InmatningsListan = dbInmatningar.ShowAll();
            var KundListan = dbKund.ShowAll();



            if (InmatningsListan != null)
            {


                var lista = from k in InmatningsListan
                            join i in KundListan
                            on k.KundID equals i.ID

                            select new InmatningarModel()
                            {
                                KundNamn = i.Namn,

                                Status = k.Status,
                                Adress = k.Adress,
                                Ordernr = k.Ordernr,
                                InDatum = k.InDatum,
                                UtDatum = k.UtDatum,
                                ID = k.ID,
                                Ovrigt = k.Ovrigt,
                                Exporterad = k.Exporterad

                            };
                if (st == "Alla")
                {

                }
                else if (st != null)
                {
                    lista = lista.Where(x => x.Status == st);
                }

                if (kNamn == "Alla")
                {

                }
                else if (kNamn != null)
                {
                    lista = lista.Where(x => x.KundNamn == kNamn);
                }

                //  lista = lista.Where(x => x.Exporterad == exp);
                bool? test = exp;

                if (test == true)
                {
                    lista = lista.Where(x => x.Exporterad == exp);
                }

                else if (test == false)
                {
                    lista = lista.Where(x => x.Exporterad == exp);
                }
                else
                {

                }
                DateTime? test2 = inDat;
                if (test2 != null)
                {
                    lista = lista.Where(x => x.InDatum >= inDat);
                }

                DateTime? test3 = utDat;
                if (test3 != null)
                {
                    lista = lista.Where(x => x.UtDatum <= utDat);
                }


                foreach (InmatningarModel e in lista)
                {

                    i2.InmatningsLista.Add(e);


                }
            }

                foreach (var kund in KundListan)
                {
                    KundModel km = new KundModel();
                    km.Namn = kund.Namn;
                    km.Telefonnr = kund.Telefonnr;
                    km.Email = kund.Email;
                    km.ID = kund.ID;

                    i2.KundLista.Add(km);
                }
            

            return View(i2);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Inmatningar(InmatningarModel model)
        {
            return RedirectToAction("Inmatningar", new { st = model.Status , kNamn=model.KundNamn, exp=model.Exporterad, inDat=model.InDatum, utDat=model.UtDatum });
        }
        // Get Ny Inmätning
        public ActionResult NyInmatning()
        {
            KundRepository k = new KundRepository();
            List<Kund> klista = k.ShowAll();
            InmatningarModel imodel = new InmatningarModel();

            foreach (var kund in klista)
            {
                KundModel km = new KundModel();
                km.Namn = kund.Namn;
                km.Telefonnr = kund.Telefonnr;
                km.Email = kund.Email;
                km.ID = kund.ID;

                imodel.KundLista.Add(km);
            }

            return View(imodel);
        }

        public PartialViewResult GetInmätningar(int custId)
        {
            
            return null;
        }

        // Spara en ny inmätning
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult NyInmatning(InmatningarModel model)
        {
            if (!ModelState.IsValid)
                return View();

            else
            {
                String anvandarId = User.Identity.GetUserId();
                Inmatningar i = new Inmatningar();

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
                i.AID = anvandarId;
                i.Exporterad = false;
                var db = new InmatningarRepository();
                db.LaggTill(i);
                return RedirectToAction("Inmatningar", new {st = i.Status});


            }

        }
      
        //Get redigera inmätningar
        public ActionResult RedigeraInmatningar(int id)
        {

            var db = new InmatningarRepository();
            Inmatningar e = db.ShowRowByID(id);
            InmatningarModel model = new InmatningarModel();
            KundRepository k = new KundRepository();
            AnvandareRepository dbAnvandare = new AnvandareRepository();

            

            AspNetUsers anvandare = dbAnvandare.HamtaEnAnvandareMedId(e.AID);
            List<Kund> klista = k.ShowAll();

            foreach (var kund in klista)
            {
                KundModel km = new KundModel();
                km.Namn = kund.Namn;
                km.Telefonnr = kund.Telefonnr;
                km.Email = kund.Email;
                km.ID = kund.ID;

                model.KundLista.Add(km);
            }

            model.Ordernr = e.Ordernr;
            model.KundID = e.KundID;
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
            model.Exporterad = e.Exporterad;
            if (anvandare != null)
            {
                model.Anvandrare = anvandare.Email;
            }


            return View(model);
      
        }

        // Metod för att ändra och spara inmätningar

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RedigeraInmatningar(InmatningarModel model)
        {
            Inmatningar i = new Inmatningar();
            var db = new InmatningarRepository();

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

             db.SparaRedigeraInmatningar(i);

            return RedirectToAction("Inmatningar", new { st = i.Status });
        }

        public ActionResult TaBortInmatning(int id)
        {
            InmatningarRepository dbInmatningar = new InmatningarRepository();
            DAL.Inmatningar inm = dbInmatningar.ShowRowByID(id);
            dbInmatningar.TaBortEnInmatning(id);

            return RedirectToAction("Inmatningar", new { st = inm.Status });

        }

    }
}