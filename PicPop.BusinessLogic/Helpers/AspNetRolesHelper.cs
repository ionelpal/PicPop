using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.OData.Query.SemanticAst;
using PicPop.BusinessLogic.Models;

namespace PicPop.BusinessLogic.Helpers
{
    public class AspNetRolesHelper
    {
        public static List<UserRolesModel> GetUserRoles(string id)
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                var source = db.AspNetRoles.Select(x => new UserRolesModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.AspNetUsers.Any(y => y.Id.Equals(id))
                }).ToList();
                return source;
            }
        }

        public void EditRoles(string id, List<string> lst )
        {
            using (PicPopEntities db = new PicPopEntities())
            {
                var source = db.AspNetUsers.FirstOrDefault(x => x.Id.Equals(id));
                if (source == null)
                    return;
                source.AspNetRoles.Clear();
                foreach (var item in db.AspNetRoles.Where(x => lst.Contains(x.Id)).ToList())
                {
                    source.AspNetRoles.Add(item);
                }
                db.SaveChanges();
            }
        }
    }
}