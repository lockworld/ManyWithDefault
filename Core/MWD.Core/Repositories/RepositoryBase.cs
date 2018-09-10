using MWD.Core.Entities;
using MWD.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MWD.Core.Repositories
{
    public class RepositoryBase<T> : iRepository<T>, IDisposable where T : MWDEntity
    {
        public RepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        protected DbContext _context { get; set; }
        protected DbSet<T> _dbSet { get; set; }
        public DbContext Context
        {
            get
            {
                return _context;
            }
        }

        public T GetById(Guid EntityID)
        {
            return _dbSet
                .Find(EntityID);
        }

        public ICollection<T> List()
        {
            return _dbSet
                .AsEnumerable<T>().ToList();
        }

        public ICollection<T> List(Expression<Func<T, bool>> listFilter)
        {
            return _dbSet
                .Where(listFilter)
                .AsEnumerable<T>().ToList();
        }

        public DataSet ToDataSet()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);

            foreach (var item in _dbSet.GetType().GetProperties())
            {
                dt.Columns.Add(item.Name, item.PropertyType);
            }
            foreach (var item in _dbSet)
            {
                DataRow row = dt.NewRow();
                foreach (var rowItem in _dbSet.GetType().GetProperties())
                {
                    row[rowItem.Name] = rowItem.GetValue(item, null);
                }
                dt.Rows.Add(row);
            }
            return ds;
        }


        public void Add(T entity)
        {
            entity.ID = Guid.NewGuid();
            _dbSet.Add(entity);
            DbEntityEntry dbEntityEntry = _context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                dbEntityEntry.State = EntityState.Modified;
            }
        }

        public void Edit(T entity)
        {
            if (GetById(entity.ID) == null)
            {
                Add(entity);
            }
            else
            {
                DbEntityEntry dbEntityEntry = _context.Entry(entity);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                dbEntityEntry.State = EntityState.Modified;
            }
        }

        public void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = _context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public void Delete(Guid EntityID)
        {
            var entity = GetById(EntityID);
            if (entity == null) return; //Not found;
            Delete(entity);
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        //public ICollection<T2> RelatedData<T2>(T entity)
        //    where T2 : MWDEntity, iHasDefault
        //{
        //    var _t2 = new RepositoryBase<T2>(_context);
        //    return _t2.List(x => x.ForeignKey == entity.ID);
        //}

        public RepositoryBase<T2> RelatedRepo<T2>(T entity)
            where T2 : MWDEntity, iHasParent
        {
            return new RepositoryBase<T2>(_context);
        }

        //public List<T2> RelatedList<T2>(T entity)
        //    where T2: MWDEntity, iHasParent
        //{
        //    return _context.Set<T2>()
        //        .Where(e=>e.ForeignKey==entity.ID)
        //        .AsEnumerable<T2>().ToList();
        //}

        //public T2 RelatedPrincipal<T2>(T entity)
        //    where T2 : MWDEntity, iHasParent, iHasDefault
        //{
        //    var ret = _context.Set<T2>()
        //        .Where(e => e.ForeignKey == entity.ID)
        //        .AsEnumerable<T2>();
        //    if (ret.Count()==0)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        var retfirst = ret.Where(e => e.IsDefault).FirstOrDefault();
        //        if (retfirst != null)
        //        {
        //            return retfirst;
        //        }
        //        else
        //        {
        //            return ret.FirstOrDefault();
        //        }
        //    }
            
        //}



        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        #endregion

    }

}
