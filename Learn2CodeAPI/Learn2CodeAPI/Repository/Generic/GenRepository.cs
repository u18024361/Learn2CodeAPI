using Learn2CodeAPI.Data;
using Learn2CodeAPI.IRepository.Generic;
using Learn2CodeAPI.Models.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Repository.Generic
{
    public  class GenRepository<TEntity> : IGenRepository<TEntity>
        where TEntity : BaseEntity
       
    {
        private readonly AppDbContext db;
        public GenRepository(AppDbContext _db)
        {
            db =  _db;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            //set identifies table and then adds to that table
            db.Set<TEntity>().Add(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await db.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            db.Set<TEntity>().Remove(entity);
            await db.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Get(int id)
        {
            return await db.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await db.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return entity;
        }
    }
}
