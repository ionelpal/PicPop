using PicPop.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPop.BusinessLogic.Helpers
{
    public class RolesHelper : IRepository<AspNetRole>
    {
        public AspNetRole FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<AspNetRole> List
        {
            get { throw new NotImplementedException(); }
        }

        public void Update(AspNetRole role)
        {
            throw new NotImplementedException();
        }

        public int Add(AspNetRole entity)
        {
            //using (PicPopEntities db = new PicPopEntities())
            //{
            //    db.AspNetRoles.Add(role);
            //    return db.SaveChanges();
            //}
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(AspNetRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
