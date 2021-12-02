using BazaPodataka;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PristupBaziPodataka
{
    public class Repository<TEntity> where TEntity : class
    {
        public TEntity Get(params object[] keyValues)
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<TEntity>().Find(keyValues);
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<TEntity>().ToList();
            }
        }

        public void Add(TEntity entity)
        {
            using (var context = new DatabaseContext())
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
        }

        public void AddOrUpdate(TEntity entity)
        {
            using (var context = new DatabaseContext())
            {
                context.Set<TEntity>().AddOrUpdate(entity);
                context.SaveChanges();
            }
        }


        public void Remove(params object[] keyValues)
        {
            using (var context = new DatabaseContext())
            {
                var ret = context.Set<TEntity>().Find(keyValues);
                context.Set<TEntity>().Remove(ret);
                context.SaveChanges();
            }
        }

        // Ideja je da se prvo u servisu preuzme stari entitet i da se on tu i modifikuje 
        // i kao takav da se prosledi u ovu metodu
        public void Update(TEntity modifiedOldEntity, params object[] keyValues)
        {
            using (var context = new DatabaseContext())
            {
                context.Entry(modifiedOldEntity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
