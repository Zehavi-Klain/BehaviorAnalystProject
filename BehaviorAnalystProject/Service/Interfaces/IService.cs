using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service.Interfaces
{
    public interface IService<T,TKey>
    {
        T GetById(TKey id);
        List<T> GetAll();
        void Delete(TKey id);
        T AddItem(T item);
        void UpdateItem(TKey id, T item);

    }
}

