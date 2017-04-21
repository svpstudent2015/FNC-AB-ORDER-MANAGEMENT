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
        public bool ShowUserRoll(AspNetUsers u)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {
                    bool admin = false;
                    var user = db.AspNetUsers.FirstOrDefault(x => x.Id.Equals(u.Id));


                   var roller = user.AspNetRoles.ToList();
                   

                    if (roller.Count > 0)
                    {

                        var roll = roller[0];

                        if (roll.Name.Equals("Admin"))
                        {
                            admin = true;

                        }
                    }
                    return admin;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public void SparaRedigeraAnvandare(AspNetUsers e,bool admin)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {

                    AspNetUsers i = db.AspNetUsers.FirstOrDefault(x => x.Email.Equals(e.Email));

                    
                    i.Email = e.Email;


                    var roller = i.AspNetRoles.ToList();

                    //är admin vill ta bort admin
                    if (roller.Count > 0 && admin == false)
                    {

                       // var roll = roller[0];

                       i.AspNetRoles.Clear();
                    }

                    if ( roller.Count < 1 && admin==true)
                    {
                      //  AspNetRoles role = new AspNetRoles();

                     //   var UserManager = new UserManager

                        var r = db.AspNetRoles.Where(x => x.Id == "1").FirstOrDefault();

                       i.AspNetRoles.Add(r);

                       // i.AspNetRoles.Add(role);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }
        }

        public void TaBortEnAnvandare(string id)
        {
            try
            {
            using (var db = new FNCOrderHanteringEntitiesConnections())
            {
                AspNetUsers anvandareAttTaBort = db.AspNetUsers.Where(x => x.Id.Equals(id))
                    .FirstOrDefault();
                db.Entry(anvandareAttTaBort).State = System.Data.Entity.EntityState.Deleted;
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
