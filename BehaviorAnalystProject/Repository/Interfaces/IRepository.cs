using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(int id);
        List<T> GetAll();
        void Delete(int id);
        T AddItem(T item);
        T UpdateItem(int id,T item);
        //List<Form> GetFormsByIdCategory(int id)
        //{
        //    throw new NotImplementedException("GetFormsByIdCategory  repository לא מומשה");
        //}


    }
}
