using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PicPop.BusinessLogic
{
    public static class UsersHelper
    {
        public static AspNetUser Get(string email, bool profile)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                if (profile)
                    return db.AspNetUsers.Include(x => x.Profile).Include(x=>x.AspNetRoles).FirstOrDefault(x => x.Email.Equals(email));

                return db.AspNetUsers.FirstOrDefault(x => x.Email.Equals(email));
            }
        }

        public static AspNetUser Get(string id)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                return db.AspNetUsers.Include(x => x.AspNetRoles).FirstOrDefault(x => x.Id.Equals(id));
            }
        }

        public static AspNetUser GetRoles(string email)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                return db.AspNetUsers.Include(x => x.AspNetRoles).FirstOrDefault(x => x.Email.Equals(email));
            }
        }

        public static List<AspNetUser> GetAll()
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                return db.AspNetUsers.ToList();
            }

        }
    }
}
