using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicPop.BusinessLogic.Interfaces;
using PicPop.BusinessLogic.Models;

namespace PicPop.BusinessLogic
{
    public class CategoriesHelper: IRepository<Category>
    {
        public List<Category> List
        {
            get
            {
                using (PicPopEntities db = new PicPopEntities())
                {
                    return db.Categories.ToList();
                }
            }
        }

        public int Add(Category entity)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                db.Categories.Add(entity);
                return db.SaveChanges();
            }
        }

        public bool Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Category FindById(int id)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                return db.Categories.Find(id);
            }
        }

        public List<Category> GetData()
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                return db.Categories.ToList();
            }
        }

        public List<SelectItemModel> GetListItem(bool withSelected = true)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                var source = db.Categories.ToList();
                if (!source.Any())
                    return null;


                var lst = source.Select(x => new SelectItemModel()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();


                if (withSelected)
                    lst.Insert(0, new SelectItemModel()
                    {
                        Value = "",
                        Text = "Select One"
                    });

                return lst;
            }
        }

        public Boolean Exits(string name)
        {
            using (PicPopEntities db= new PicPopEntities())
            {
                return db.Categories.Any(x => x.Name.ToLower().Equals(name.ToLower().Trim()));
            }
        }

        public void Update(Category entity)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
