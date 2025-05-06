using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service.Interfaces
{
    public interface IService<T>
    {
        T GetById(int id);
        List<T> GetAll();
        void Delete(int id);
        T AddItem(T item);
        void UpdateItem(int id, T item);

    }
}

