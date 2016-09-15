using PicPop.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPop.BusinessLogic
{
    public class ProfilesHelper : IRepository<Profile>
    {
        public Profile FindById(int id)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                return db.Profiles.Find(id);
            }
        }

        public List<Profile> List
        {
            get
            {
                using (PicPopEntities db = new PicPopEntities())
                {
                    return db.Profiles.ToList();
                }
            }
        }

        public void Update(Profile profile)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                db.Entry(profile).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public int Add(Profile profile)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                db.Profiles.Add(profile);
                return db.SaveChanges();
            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Profile entity)
        {
            throw new NotImplementedException();
        }
    }
}
