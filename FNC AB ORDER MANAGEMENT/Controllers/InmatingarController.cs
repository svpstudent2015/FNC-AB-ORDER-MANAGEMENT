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
        public ActionResult Inmatningar(string st)
        {
            InmatningarModel i2 = new InmatningarModel();
            
            var dbInmatningar = new InmatningarRepository();            
            var dbKund = new KundRepository();
            var InmatningsListan = dbInmatningar.ShowAll();
            var KundListan = dbKund.ShowAll();

            if (InmatningsListan != null)
            {


                var lista = from k in InmatningsListan
                    join i in KundListan
                    on k.KundID equals i.ID
                            where k.Status == st
                    select new InmatningarModel()
                    {
                        KundNamn = i.Namn,
                        Status = k.Status,
                        Adress = k.Adress,
                        Ordernr = k.Ordernr,
                        InDatum = k.InDatum,
                        ID = k.ID,
                        Ovrigt = k.Ovrigt

                    };

                foreach (InmatningarModel e in lista)
                {
                    //  InmatningarModel i = new InmatningarModel();

                    //  if (e.InDatum != null)
                    //  {
                    //      i.InDatum = (DateTime)e.InDatum;

                    //  }
                    //  if(e.UtDatum != null)
                    //  {
                    //      i.UtDatum = (DateTime)e.UtDatum;
                    //  }

                    //  i.ID = e.ID;
                    //  i.AID = e.AID;

                    //  i.Ordernr = e.Ordernr;
                    ////  i.Kund = e.Kund;
                    //  i.Telefonnr = e.Telefonnr;
                    //  i.Ort = e.Ort;
                    //  i.Adress = e.Adress;                           
                    //  //i.Fakturerad = (bool)e.Fakturerad;
                    //  i.Ovrigt = e.Ovrigt;
                    //  i.Status = e.Status;

                    i2.InmatningsLista.Add(e);


                }
            }

            return View(i2);
        }
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

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult NyInmatning(InmatningarModel model)
        {
            if (!ModelState.IsValid)
                return View();

            else
            {
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

                var db = new InmatningarRepository();
                db.Add(i);
                return RedirectToAction("Inmatningar", new {st = i.Status});


            }

        }
      
        // Metod för att visa en inmätning med hjälp av ID
        public ActionResult RedigeraInmatningar(int id)
        {

            var db = new InmatningarRepository();
            Inmatningar e = db.ShowRowByID(id);
            InmatningarModel model = new InmatningarModel();
            KundRepository k = new KundRepository();
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

            return View(model);
        //    return RedirectToAction("RedigeraInmatningar", "Inmatningar", model);
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

            return RedirectToAction("Inmatningar", "Inmatningar");
        }

        public ActionResult TaBortInmatning(int id)
        {
            InmatningarRepository rep = new InmatningarRepository();
            rep.TaBortEnInmatning(id);

            return RedirectToAction("Inmatningar", "Inmatningar");

        }

    }
}