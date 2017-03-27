using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DAL.RepositoryFolder
{
    public class InmatningarRepository : Repository<INMATNINGAR>
    {
        public INMATNINGAR ShowRowByID(int id)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {
                    return db.INMATNINGAR.FirstOrDefault(x => x.ID == id);


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public void SparaRedigeraInmatningar(INMATNINGAR e)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {

                    INMATNINGAR i = db.INMATNINGAR.FirstOrDefault(x => x.ID == e.ID);

                    i.Ordernr = e.Ordernr;
                    i.Kund = e.Kund;
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

    }
}
