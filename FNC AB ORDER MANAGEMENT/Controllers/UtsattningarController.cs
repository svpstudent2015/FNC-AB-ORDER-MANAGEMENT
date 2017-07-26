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
using iTextSharp.text.pdf;
using System.IO;

namespace FNC_AB_ORDER_MANAGEMENT.Controllers
{
    public class UtsattningarController : Controller
    {
        // GET: Utsattningar
        //Andropas med önskad satus på utsättningar.
        //Retunerar vyn utsattning tillsammans med en modell innehållandes
        //en lista av utsattningar 
        public ActionResult Utsattningar(string sta, string kNamn, bool? exp, DateTime? inDat, DateTime? utDat)
        {
            UtsattningarModel i2 = new UtsattningarModel();
            i2.Status = sta;
            i2.KundNamn = kNamn;
            i2.Exporterad = exp;
            i2.InDatum = inDat;
            i2.UtDatum = utDat;

            var dbUtsattningar = new UtsattningarRepository();
            var dbKund = new KundRepository();
            var utsattningsLista = dbUtsattningar.ShowAll();
            var kundLista = dbKund.ShowAll();

            if (utsattningsLista != null)
            {

                var lista = from u in utsattningsLista
                    join k in kundLista
                    on u.KundID equals k.ID

                    select new UtsattningarModel()
                    {
                        KundNamn = k.Namn,
                        Status = u.Status,
                        Adress = u.Adress,
                        Ordernr = u.Ordernr,
                        InDatum = u.InDatum,
                        ID = u.ID,
                        Ovrigt = u.Ovrigt,
                        Exporterad = u.Exporterad

                    };

                if (sta == "Alla")
                {

                }
                else if (sta != null)
                {
                    lista = lista.Where(x => x.Status == sta);
                }

                if (kNamn == "Alla")
                {

                }
                else if (kNamn != null)
                {
                    lista = lista.Where(x => x.KundNamn == kNamn);
                }

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

                foreach (UtsattningarModel uM in lista)
                {

                    i2.UtsattningsLista.Add(uM);

                }
            }

            foreach (var kund in kundLista)
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
        public ActionResult Utsattningar(UtsattningarModel model)
        {
            return RedirectToAction("Utsattningar", new { sta = model.Status, kNamn = model.KundNamn, exp = model.Exporterad, inDat = model.InDatum, utDat = model.UtDatum });
        }

        //Retunerar vyn NyUtsattning med en modell inhållandes en
        //lista av Kunder
        public ActionResult NyUtsattning()
        {
            KundRepository dbKund = new KundRepository();
            List<Kund>kLista = dbKund.ShowAll();
            UtsattningarModel uModel = new UtsattningarModel();
           


            foreach (var kund in kLista )
            {
                KundModel km= new KundModel();
                km.Namn = kund.Namn;
                km.Telefonnr = kund.Telefonnr;
                km.Email = kund.Email;
                km.ID = kund.ID;

                uModel.KundLista.Add(km);
            }
            

            return View(uModel);
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
               
                Utsattningar u = new Utsattningar();

                u.Ordernr = model.Ordernr;
                u.KundID = model.KundID;
                u.Telefonnr = model.Telefonnr;
                u.Ort = model.Ort;
                u.Adress = model.Adress;
                u.InDatum = model.InDatum;
                u.UtDatum = model.UtDatum;
                u.StyckPris = model.StyckPris;
                u.Langd = model.Langd;
                u.Timmar = model.Timmar;
                u.Fakturerad = model.Fakturerad;
                u.Ovrigt = model.Ovrigt;
                u.Status = model.Status; 
                u.GPS = model.GPS;
                u.AID = anvandarId;
                u.Exporterad = false;

                var dbUtsattningar = new UtsattningarRepository();
                dbUtsattningar.LaggTill(u);
                return RedirectToAction("Utsattningar", new { sta = u.Status });

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

        public ActionResult pdf(string bestNr, string kund, string ort, string adress, DateTime? inDat, DateTime? utDat, bool? etab, decimal? m, int? inmId)
        {
            Document document = new Document();
            UtsattningarRepository dbUtsattningar = new UtsattningarRepository();

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

                PdfPCell cell = new PdfPCell(new Phrase("Utsättning"));

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

            dbUtsattningar.AndraEnUtsattningTillExp(inmId);

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


            UtsattningarRepository dbUtsattningar = new UtsattningarRepository();
            KundRepository dbKund = new KundRepository();
            List<Kund> KundListan = dbKund.ShowAll();
            List<Utsattningar>  UtsattningsListan = dbUtsattningar.SattUtsattningarTillExpOchHamtaLista(list);

            var lista = from k in UtsattningsListan
                        join i in KundListan
                        on k.KundID equals i.ID

                        select new UtsattningarModel()
                        {
                            KundNamn = i.Namn,


                            Adress = k.Adress,
                            Ordernr = k.Ordernr,
                            InDatum = k.InDatum,
                            UtDatum = k.UtDatum,
                            Ort = k.Ort,
                            StyckPris = k.StyckPris,
                            Langd = k.Langd




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

                PdfPCell cell = new PdfPCell(new Phrase("Utsättningar"));

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

                foreach (var utsatt in lista)
                {

                    table.AddCell(new PdfPCell(new Phrase(utsatt.Ordernr, arial)));

                    //table.AddCell(kund);

                    table.AddCell(new PdfPCell(new Phrase(utsatt.KundNamn, arial)));

                    //table.AddCell(ort);

                    table.AddCell(new PdfPCell(new Phrase(utsatt.Ort, arial)));

                    //table.AddCell(adress);

                    table.AddCell(new PdfPCell(new Phrase(utsatt.Adress, arial)));

                    //table.AddCell(inDat.Value.ToShortDateString());
                    if (utsatt.InDatum != null)
                    {
                        table.AddCell(new PdfPCell(new Phrase(utsatt.InDatum.Value.ToShortDateString(), arial)));
                    }
                    else
                    {
                        table.AddCell("");
                    }

                    //table.AddCell(utDat.Value.ToShortDateString());
                    if (utsatt.UtDatum != null)
                    {
                        table.AddCell(new PdfPCell(new Phrase(utsatt.UtDatum.Value.ToShortDateString(), arial)));
                    }
                    else
                    {
                        table.AddCell("");
                    }

                    //table.AddCell(etab.ToString());
                    if (utsatt.StyckPris == true)
                    {
                        table.AddCell(new PdfPCell(new Phrase("Ja", arial)));
                    }
                    else
                    {
                        table.AddCell(new PdfPCell(new Phrase("Nej", arial)));
                    }
                    //table.AddCell(m.ToString());

                    table.AddCell(new PdfPCell(new Phrase(utsatt.Langd.ToString(), arial)));

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