using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPop.BusinessLogic.Interfaces
{
    internal interface IRepository<T> where T : class
    {
        T FindById(int id);
        List<T> List { get; }
        void Update(T entity); 
        int Add(T entity);
        bool Delete(int id);
        bool Delete(T entity);
    }
}
