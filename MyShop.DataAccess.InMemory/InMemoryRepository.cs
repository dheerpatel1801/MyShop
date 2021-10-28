using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        String ClassName;
        public InMemoryRepository()
        {
            ClassName = typeof(T).Name;
            items = cache[ClassName] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }
        public void Commit()
        {
            cache[ClassName] = items;
        }
        public void Insert(T t)
        {
            items.Add(t);
        }
        public void Update(T t)
        {
            T Updatet = items.Find(i => i.Id == t.Id);
            if (Updatet != null)
            {
                Updatet = t;
            }
            else
            {
                throw new Exception(ClassName + "Not Found");
            }
        }
        public T Find(String Id)
        {
            T t = items.Find(i => i.Id == Id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(ClassName + "Not Found");
            }
        }
        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }
        public void Delete(String Id)
        {
            T Deletet = items.Find(i => i.Id == Id);
            if (Deletet != null)
            {
                items.Remove(Deletet);
            }
            else
            {
                throw new Exception(ClassName + "Not Found");
            }
        }

    }
}
