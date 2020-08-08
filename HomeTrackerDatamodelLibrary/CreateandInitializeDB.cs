using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HomeTrackerDatamodelLibrary
{
    public class CreateAndInitializeDB
    {
        public static void CreateAndInitializeDatabase()
        {
            Database.SetInitializer<HomeTrackerModel1>(new HomeTrackerInitializer());

            // The LINQ below is strictly to cause the database to be created
            //    and initialized, if it does not already exist.  The data
            //    returned is not being used for anything.
            using (HomeTrackerModel1 db = new HomeTrackerModel1())
            {
                //retrieve data:
                Agent agent = (from a in db.Agents
                               select a).FirstOrDefault();

            }
        }
    }    
    
}
