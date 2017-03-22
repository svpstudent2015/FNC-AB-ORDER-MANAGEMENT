using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoryFolder
{
    public class Repository<T> where T : class 
    {
        public void Add(T item)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {
                    db.Set<T>().Add(item);
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
