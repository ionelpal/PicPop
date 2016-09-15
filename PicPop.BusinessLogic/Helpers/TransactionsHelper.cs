using PicPop.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace PicPop.BusinessLogic
{
    public class TransactionsHelper : IRepository<Transaction>
    {
        #region STATIC
        public static bool HasTransaction(string userId)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                return db.Transactions.Any(x => x.UserId.Equals(userId));
            }
        }
        #endregion
        

        #region GET
        public Transaction FindById(int id)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                return db.Transactions.Find(id);
            }
        }

        public List<Transaction> List
        {
            get
            {
                using (PicPopEntities db = new PicPopEntities())
                {
                    return db.Transactions.ToList();
                }
            }
        }

        public Transaction GetTransaction(int id)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                return db.Transactions.Include(x => x.TransactionItems.Select(y => y.PicPopImage)).FirstOrDefault(x=>x.Id.Equals(id));
            }
        }

        public List<Transaction> GetMyTransaction(string userId)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                return db.Transactions.Include(x => x.TransactionItems).Where(x => x.UserId.Equals(userId)).ToList();
            }
        }
        #endregion

        #region UPDATE
        public void Update(Transaction entity)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        #endregion

        #region INSERT
        public int Add(Transaction entity)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                db.Transactions.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Add(List<Transaction> lstEntity)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                db.Transactions.AddRange(lstEntity);
                return db.SaveChanges();
            }
        }
        #endregion

        #region DELETE
        public bool Delete(int id)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                var transaction = db.Transactions.Find(id);
                if (transaction == null)
                    return false;
                db.Transactions.Remove(transaction);
                db.SaveChanges();

                return true;
            }
        }

        public bool Delete(Transaction entity)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                db.Transactions.Remove(entity);
                db.SaveChanges();
                return true;
            }
        }
        #endregion
    }
}
