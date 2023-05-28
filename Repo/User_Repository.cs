using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repo
{
    public class User_Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly User_DBContext context;
        private readonly DbSet<T> entities;

        public User_Repository(User_DBContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public T Get(int id)
        {
            return entities.SingleOrDefault(s => s.ID == id);
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Add(entity);
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Remove(entity);
        }
        public List<T> Include(string entity)
        {
            return entities.Include(entity).ToList();
        }
    }
}
