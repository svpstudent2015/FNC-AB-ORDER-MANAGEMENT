using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoryFolder
{
    public class AnvandareRepository : Repository<AspNetUsers>
    {
        public AspNetUsers ShowRowByID(string id)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {
                    return db.AspNetUsers.FirstOrDefault(x => x.Id.Equals(id));


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public void SparaRedigeraAnvandare(AspNetUsers e)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {

                    AspNetUsers i = db.AspNetUsers.FirstOrDefault(x => x.Id.Equals(e.Id));

                    i.UserName = e.UserName;
                    i.PasswordHash = e.PasswordHash;
                    i.Email = e.Email;
                  
                 
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }
        }
    }
}
