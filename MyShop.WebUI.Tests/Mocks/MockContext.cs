using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.WebUI.Tests.Mocks
{
    public class MockContext<T> : IRepository<T> where T : BaseEntity
    {
        List<T> items;
        String ClassName;
        public MockContext()
        {
            items = new List<T>();
        }
        public void Commit()
        {
            return;
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
