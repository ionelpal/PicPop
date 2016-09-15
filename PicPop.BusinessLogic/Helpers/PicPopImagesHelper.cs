using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using PicPop.BusinessLogic.Interfaces;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;


namespace PicPop.BusinessLogic
{
    public class PicPopImagesHelper : IRepository<PicPopImage>
    {
        #region GET

        public List<PicPopImage> List
        {
            get
            {
                using (PicPopEntities db = new PicPopEntities())
                {
                    return db.PicPopImages.ToList();
                }
            }
        }

        public List<PicPopImage> GetMyImagesList(string userId)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                var source =
                    db.PicPopImages.Where(
                        x => (x.UserId.Equals(userId) || x.TransactionItems.Any(y=>y.Transaction.UserId.Equals(userId))))
                        .Include(x => x.BlobFiles)
                        .ToList();

                return source;
            }
        }

        public List<PicPopImage> GetShop(string userId, int? categoryId = null)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                return
                    db.PicPopImages.Include(x => x.BlobFiles)
                        .Where(x => !x.UserId.Equals(userId) && (!categoryId.HasValue || x.CategoryId == categoryId) && !x.TransactionItems.Any(y=>y.Transaction.UserId.Equals(userId)))
                        .ToList();
            }
        }

        public PicPopImage FindById(int id)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                return db.PicPopImages.Find(id);
            }
        }

        public PicPopImage Details(int id)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                return
                    db.PicPopImages.Include(x => x.BlobFiles)
                        .Include(x => x.Category)
                        .Include(x => x.TransactionItems)
                        .Include("TransactionItems.Transaction")
                        .FirstOrDefault(x => x.Id.Equals(id));
            }
        }

        public List<BlobFile> GetUrlImages(int? categoryId = null, int numImages = 10)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                var source = db.PicPopImages.Where(x => (!categoryId.HasValue || x.CategoryId == categoryId.Value) && x.Active);
                var numObject = source.Count();

                if (numObject == 0)
                    return new List<BlobFile>();

                if (numObject > numImages)
                {
                    var randomSkip = new Random().Next(0, numObject - numImages);

                    return
                        source.OrderBy(x => x.Id).Skip(randomSkip)
                            .Take(numImages)
                            .Select(x => x.BlobFiles.FirstOrDefault(y => y.Container.Equals((int)BlobFileType.ImagesWaterMark)))
                            .ToList();
                }
                return source.Select(x => x.BlobFiles.FirstOrDefault(y=>y.Container.Equals((int)BlobFileType.ImagesWaterMark))).ToList();
            }
        }

        public List<BlobFile> LastImages(int numImages)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                var source = db.PicPopImages.OrderByDescending(x=>x.Id).Take(10).Select(x => x.BlobFiles.FirstOrDefault(y => y.Container.Equals((int)BlobFileType.ImagesWaterMark)))
                            .ToList();

                return source;
            }

        }

        #endregion

        #region UPDATE

        public void Update(PicPopImage entity)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        #endregion

        #region DELETE

        public bool Delete(int id)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                var image = db.PicPopImages.Find(id);
                if (image == null)
                    return false;
                db.PicPopImages.Remove(image);
                db.SaveChanges();

                return true;
            }
        }

        public bool Delete(PicPopImage entity)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                db.PicPopImages.Remove(entity);
                db.SaveChanges();
                return true;
            }
        }

        #endregion

        #region INSERT

        public int Add(PicPopImage image)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                image.DtAdded = DateTime.Now;
                image.Active = true;
                db.PicPopImages.Add(image);
                return db.SaveChanges();


                


            }
        }

        #endregion
    }
}
