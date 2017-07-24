using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FNC_AB_ORDER_MANAGEMENT.Models;
using DAL.RepositoryFolder;
using DAL;
using Microsoft.AspNet.Identity;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;

using System.Globalization;

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
            Kund tempKund = k.getKund(e.KundID);
            

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
            model.KundNamn = tempKund.Namn;
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
      
        public ActionResult pdf(string bestNr,string kund, string ort, string adress, DateTime? inDat, DateTime? utDat, bool? etab, decimal? m , int? inmId)
        {
            Document document = new Document();
            InmatningarRepository dbInmatningar = new InmatningarRepository();
            
            MemoryStream stream = new MemoryStream();

            try
            {
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.CloseStream = false;

                document.Open();
                document.Add(new Paragraph("Hello World"));
                document.Add(new Paragraph(DateTime.Now.ToShortDateString()));
                document.Add(new Chunk("\n"));
                PdfPTable table = new PdfPTable(8);
                table.WidthPercentage = 100;
                Font arial = FontFactory.GetFont("Arial", 11);
                //Font fontH1 = new Font(Helvetica, 16, Font.NORMAL);

                PdfPCell cell = new PdfPCell(new Phrase("Inmätning"));

                cell.Colspan = 8;

                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

                table.AddCell(cell);

                table.AddCell("Best Nr");

                table.AddCell("Kund");

                table.AddCell("Ort");

                table.AddCell("Adress");

                table.AddCell("In");

                table.AddCell("Ut");

                table.AddCell("Etab");

                table.AddCell("Meter");

                //table.AddCell("Fakturerad");

                //table.AddCell(bestNr);

                table.AddCell(new PdfPCell(new Phrase(bestNr, arial)));

                //table.AddCell(kund);

                table.AddCell(new PdfPCell(new Phrase(kund, arial)));

                //table.AddCell(ort);

                table.AddCell(new PdfPCell(new Phrase(ort, arial)));

                //table.AddCell(adress);

                table.AddCell(new PdfPCell(new Phrase(adress, arial)));

                //table.AddCell(inDat.Value.ToShortDateString());
                if (inDat != null)
                {
                    table.AddCell(new PdfPCell(new Phrase(inDat.Value.ToShortDateString(), arial)));
                }
                else
                {
                    table.AddCell("");
                }

                //table.AddCell(utDat.Value.ToShortDateString());
                if (utDat != null)
                {
                    table.AddCell(new PdfPCell(new Phrase(utDat.Value.ToShortDateString(), arial)));
                }
                else
                {
                    table.AddCell("");
                }

                //table.AddCell(etab.ToString());
                if (etab == true)
                {
                    table.AddCell(new PdfPCell(new Phrase("Ja", arial)));
                }
                else
                {
                    table.AddCell(new PdfPCell(new Phrase("Nej", arial)));
                }
                //table.AddCell(m.ToString());

                table.AddCell(new PdfPCell(new Phrase(m.ToString(), arial)));

                //table.AddCell(DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture));

                document.Add(table);

            }
            catch (DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }

            document.Close();

            stream.Flush(); //Always catches me out
            stream.Position = 0; //Not sure if this is required

            dbInmatningar.AndraEnInmatningTillExp(inmId);
            
            return File(stream, "application/pdf", "DownloadName.pdf");
            
            
        }

        public ActionResult tempPDF(List<int> userId)
        {

            var hej = "hej";

                //var list = Session[userId] new List<int>();
                //list.Add(userId);
                Session[hej] = userId;

            return Json(new { success = true, hej }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FleraPdfer()
        {
            var list = Session["hej"] as List<int>;

            //List<int> userId = new List<int>();
            //userId.Add(10);
            //userId.Add(11);
            //userId.Add(12);


            InmatningarRepository dbInmatningar = new InmatningarRepository();
            KundRepository dbKund = new KundRepository();
            List<Kund> KundListan = dbKund.ShowAll();
            List<Inmatningar> InmatningsListan = dbInmatningar.SattInmatningTillExpOchHamtaLista(list);

            var lista = from k in InmatningsListan
                        join i in KundListan
                        on k.KundID equals i.ID

                        select new InmatningarModel()
                        {
                            KundNamn = i.Namn,

                           
                            Adress = k.Adress,
                            Ordernr = k.Ordernr,
                            InDatum = k.InDatum,
                            UtDatum = k.UtDatum,
                            Ort = k.Ort,
                            StyckPris = k.StyckPris,
                            Langd =k.Langd
                            
                           
                        

                        };

            Document document = new Document();
           

            MemoryStream stream = new MemoryStream();

            try
            {
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.CloseStream = false;

                document.Open();
                document.Add(new Paragraph("Hello World"));
                document.Add(new Paragraph(DateTime.Now.ToShortDateString()));
                document.Add(new Chunk("\n"));
                PdfPTable table = new PdfPTable(8);
                table.WidthPercentage = 100;
                Font arial = FontFactory.GetFont("Arial", 11);
                //Font fontH1 = new Font(Helvetica, 16, Font.NORMAL);

                PdfPCell cell = new PdfPCell(new Phrase("Inmätning"));

                cell.Colspan = 8;

                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

                table.AddCell(cell);

                table.AddCell("Best Nr");

                table.AddCell("Kund");

                table.AddCell("Ort");

                table.AddCell("Adress");

                table.AddCell("In");

                table.AddCell("Ut");

                table.AddCell("Etab");

                table.AddCell("Meter");

                //table.AddCell("Fakturerad");

                //table.AddCell(bestNr);

                foreach (var inmatning in lista)
                {

                    table.AddCell(new PdfPCell(new Phrase(inmatning.Ordernr, arial)));

                    //table.AddCell(kund);

                    table.AddCell(new PdfPCell(new Phrase(inmatning.KundNamn, arial)));

                    //table.AddCell(ort);

                    table.AddCell(new PdfPCell(new Phrase(inmatning.Ort, arial)));

                    //table.AddCell(adress);

                    table.AddCell(new PdfPCell(new Phrase(inmatning.Adress, arial)));

                    //table.AddCell(inDat.Value.ToShortDateString());
                    if (inmatning.InDatum != null)
                    {
                        table.AddCell(new PdfPCell(new Phrase(inmatning.InDatum.Value.ToShortDateString(), arial)));
                    }
                    else
                    {
                        table.AddCell("");
                    }

                    //table.AddCell(utDat.Value.ToShortDateString());
                    if (inmatning.UtDatum != null)
                    {
                        table.AddCell(new PdfPCell(new Phrase(inmatning.UtDatum.Value.ToShortDateString(), arial)));
                    }
                    else
                    {
                        table.AddCell("");
                    }

                    //table.AddCell(etab.ToString());
                    if (inmatning.StyckPris == true)
                    {
                        table.AddCell(new PdfPCell(new Phrase("Ja", arial)));
                    }
                    else
                    {
                        table.AddCell(new PdfPCell(new Phrase("Nej", arial)));
                    }
                    //table.AddCell(m.ToString());

                    table.AddCell(new PdfPCell(new Phrase(inmatning.Langd.ToString(), arial)));

                    //table.AddCell(DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture));

                }

                document.Add(table);

            }
            catch (DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }

            document.Close();

            stream.Flush(); //Always catches me out
            stream.Position = 0; //Not sure if this is required

           

            return File(stream, "application/pdf", "DownloadName.pdf");



            
        }

    }
}