using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DAL.RepositoryFolder
{
    public class InmatningarRepository : Repository<Inmatningar>
    {
        public Inmatningar ShowRowByID(int id)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {
                    return db.Inmatningar.FirstOrDefault(x => x.ID == id);


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public void SparaRedigeraInmatningar(Inmatningar e)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {

                    Inmatningar i = db.Inmatningar.FirstOrDefault(x => x.ID == e.ID);

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
            catch(Exception ex)
            {
                Console.WriteLine(ex);

            }
        }

        public void TaBortEnInmatning(int id)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {
                    Inmatningar inmatningAttTaBort = db.Inmatningar.Where(x => x.ID == id)
                        .FirstOrDefault();
                    db.Entry(inmatningAttTaBort).State = System.Data.Entity.EntityState.Deleted;
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
