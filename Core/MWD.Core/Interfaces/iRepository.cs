using MWD.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MWD.Core.Interfaces
{
    public interface iRepository<T> where T : MWDEntity
    {
        T GetById(Guid ID);
        ICollection<T> List();
        ICollection<T> List(Expression<Func<T, bool>> ListFilter);

        void Add(T entity);
        void Edit(T entity);
        void Delete(T entity);
        void Delete(Guid ID);
    }
}
