using PicPop.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPop.BusinessLogic
{
    public class BlobFilesHelper : IRepository<BlobFile>
    {
        public BlobFile FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<BlobFile> List
        {
            get { throw new NotImplementedException(); }
        }

        public BlobFile GetByKey(string key)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                return db.BlobFiles.FirstOrDefault(x => x.BlobKey.Equals(key));
            }
        }

        public bool Exist(string key)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                return db.BlobFiles.Any(x => x.BlobKey.Equals(key));
            }
        }

        public void Update(BlobFile entity)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public int Add(BlobFile entity)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                entity.dtCreated = DateTime.Now;
                db.BlobFiles.Add(entity);
                return db.SaveChanges();
            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(BlobFile entity)
        {
            throw new NotImplementedException();
        }
    }
}
