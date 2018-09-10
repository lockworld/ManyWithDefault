using MWD.Core.Entities;
using MWD.Core.Interfaces;
using MWD.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWD.ArvixeSQL.Repositories
{
    public class UnitOfWork :  IDisposable
    {
        private ArvixeDB _context { get; set; }
        public UnitOfWork()
        {
            CreateDbContext();
        }
        public UnitOfWork(ArvixeDB dbContext)
        {
            _context = dbContext;
        }

        #region Repositories
        //private iRepository<Person> _people;
        private RepositoryBase<Person> _people;
        private RepositoryBase<Email> _email;
        #endregion

        public RepositoryBase<Person> People
        {
            get
            {
                if (_people == null)
                {
                    _people = new RepositoryBase<Person>(_context);
                }
                return _people;
            }
        }

        public RepositoryBase<Email> Email
        {
            get
            {
                if (_email == null)
                {
                    _email = new RepositoryBase<Email>(_context);
                }
                return _email;
            }
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



               

        public void Save<T>(T entity) where T : MWDEntity
        {
            if (entity is Person)
            {
                People.Edit(entity as Person);
            }
            if (entity is Email)
            {
                Email.Edit(entity as Email);
            }
            Save();
        }

        protected void CreateDbContext()
        {
            _context = new ArvixeDB();

            // Do NOT enable proxied entities, else serialization fails.
            //if false it will not get the associated certification and skills when we
            //get the applicants
            _context.Configuration.ProxyCreationEnabled = false;

            // Load navigation properties explicitly (avoid serialization trouble)
            _context.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
            _context.Configuration.ValidateOnSaveEnabled = false;

            //DbContext.Configuration.AutoDetectChangesEnabled = false;
            // We won't use this performance tweak because we don't need
            // the extra performance and, when autodetect is false,
            // we'd have to be careful. We're not being that careful.
        }


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
