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
    }
}
