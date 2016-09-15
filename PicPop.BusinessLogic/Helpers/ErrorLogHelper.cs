using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPop.BusinessLogic.Helpers
{
    public class ErrorLogHelper
    {
        public static int Add(ErrorsLog error)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                db.ErrorsLogs.Add(error);
                return db.SaveChanges();
            }

        }
    }
}
