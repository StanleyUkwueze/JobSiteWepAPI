using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteProject.Data
{
    public interface ICRUDRepo
    {
        public T Add<T>(T entity);
        public T Edit<T>(T entity);
        public T Delete<T>(T entity);

    }
}
