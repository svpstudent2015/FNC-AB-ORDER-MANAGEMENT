using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoryFolder
{
    public class KundRepository : Repository<Kund>
    {
        public Kund getKund(int id)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {
                    return db.Kund.FirstOrDefault(x => x.ID == id);
                }
            

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public void SparaRedigeraKunder(Kund e)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {

                    Kund i = db.Kund.FirstOrDefault(x => x.ID == e.ID);

                    i.Namn = e.Namn;
                    i.Email = e.Email;
                    i.Telefonnr = e.Telefonnr;
                   
                    

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }
        }
        public void TaBortEnKund(int id)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {
                    Kund kundAttTaBort = db.Kund.Where(x => x.ID == id)
                        .FirstOrDefault();
                    db.Entry(kundAttTaBort).State = System.Data.Entity.EntityState.Deleted;
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
