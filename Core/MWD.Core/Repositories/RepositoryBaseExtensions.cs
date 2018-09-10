using MWD.Core.Entities;
using MWD.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWD.Core.Repositories
{
    public static class RepositoryBaseExtensions
    {
        public static List<Email> GetEmailListByEntity (this RepositoryBase<Email> rbRepo, iHasEmail entity)
        {
            return rbRepo.Context.Set<Email>().Where(e => e.ForeignKey == entity.ID).ToList();
        }
        public static Email GetDefaultEmailByEntity(this RepositoryBase<Email> rbRepo, iHasEmail entity)
        {
            var emails = rbRepo.GetEmailListByEntity(entity);
            var email = emails.FirstOrDefault(e => e.IsDefault);
            if (email==null)
            {
                return emails.FirstOrDefault();
            }
            else
            {
                return email;
            }
        }
        public static void SetDefaultEmailByEntity(this RepositoryBase<Email> rbRepo, iHasEmail entity, Email email)
        {
            var context = rbRepo.Context;
            var emails = context.Set<Email>();
            foreach (var oldemail in emails.Where(e => e.ForeignKey == entity.ID))
            {
                if (oldemail.IsDefault)
                {
                    oldemail.IsDefault = false;
                    context.Entry(oldemail).State = EntityState.Modified;
                }
                if (oldemail.Nickname?.ToLower()=="default")
                {
                    oldemail.Nickname = "";
                    context.Entry(oldemail).State = EntityState.Modified;
                }
            }
            context.SaveChanges();
            email.IsDefault = true;
            email.ForeignKey = entity.ID;
            if (string.IsNullOrWhiteSpace(email.Nickname))
            {
                email.Nickname = "Default";
            }
            var existing = emails.FirstOrDefault(e => e.ID == email.ID);
            if (existing == null)
            {
                DbEntityEntry dbEntityEntry = context.Entry(email);
                emails.Add(email);
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbEntityEntry dbEntityEntry = context.Entry(email);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    emails.Attach(email);
                }
                dbEntityEntry.State = EntityState.Modified;
            }
            context.SaveChanges();
        }
        public static void SetEmailListForEntity(this RepositoryBase<Email> rbRepo, iHasEmail entity, ICollection<Email> emailList)
        {
            var context = rbRepo.Context;
            var emails = context.Set<Email>();
            foreach (var email in emailList)
            {
                email.ForeignKey = entity.ID;
                if (email.IsDefault && string.IsNullOrWhiteSpace(email.Nickname))
                {
                    email.Nickname = "Default";
                }
                var existing = emails.FirstOrDefault(e => e.ID == email.ID);
                if (existing == null)
                {
                    DbEntityEntry dbEntityEntry = context.Entry(email);
                    emails.Add(email);
                    dbEntityEntry.State = EntityState.Added;
                }
                else
                {
                    DbEntityEntry dbEntityEntry = context.Entry(email);
                    if (dbEntityEntry.State != EntityState.Detached)
                    {
                        emails.Attach(email);
                    }
                    dbEntityEntry.State = EntityState.Modified;
                }
            }
            var elg = emailList.Select(e => e.ID);
            foreach (var email in emails.Where(e => e.ForeignKey == entity.ID))
            {
                if (!elg.Contains(email.ID))
                {
                    DbEntityEntry dbEntityEntry = context.Entry(email);
                    if (dbEntityEntry.State != EntityState.Deleted)
                    {
                        dbEntityEntry.State = EntityState.Deleted;
                    }
                    else
                    {
                        emails.Attach(email);
                        emails.Remove(email);
                    }
                    dbEntityEntry.State = EntityState.Modified;
                }
            }
            rbRepo.Context.SaveChanges();
        }
    }
}
