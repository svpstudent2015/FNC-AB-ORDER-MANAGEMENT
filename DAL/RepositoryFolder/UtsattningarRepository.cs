using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Security.Cryptography;
using System.Xml;
using DAL;

namespace DAL.RepositoryFolder
{
    public class UtsattningarRepository : Repository<Utsattningar>
    {
        public Utsattningar ShowRowByID(int id)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {
                    return db.Utsattningar.FirstOrDefault(x => x.ID == id);


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public void SparaRedigeraUtsattningar(Utsattningar e)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {

                    Utsattningar i = db.Utsattningar.FirstOrDefault(x => x.ID == e.ID);

                    i.Ordernr = e.Ordernr;
                    i.KundID = e.KundID;
                    i.Telefonnr = e.Telefonnr;
                    i.Ort = e.Ort;
                    i.Adress = e.Adress;
                    i.InDatum = e.InDatum;
                    i.UtDatum = e.UtDatum;
                    i.StyckPris = e.StyckPris;
                    i.Langd = e.Langd;
                    i.Timmar = e.Timmar;
                    i.Fakturerad = e.Fakturerad;
                    i.Ovrigt = e.Ovrigt;
                    i.Status = e.Status;
                    i.ID = e.ID;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }
        }

        public void TaBortEnUtsattning(int id)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {
                    Utsattningar utsattningAttTaBort = db.Utsattningar.Where(x => x.ID==id)
                        .FirstOrDefault();
                    db.Entry(utsattningAttTaBort).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
        }

    }
}
