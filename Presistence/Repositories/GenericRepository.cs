using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class GenericRepository<TEntity>(AppDbContext _dbcontext) : IGenericRepository<TEntity> where TEntity : class
    {
        public async Task AddAsync(TEntity entity) => await _dbcontext.Set<TEntity>().AddAsync(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbcontext.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(int id) => await _dbcontext.Set<TEntity>().FindAsync(id);

        public void Remove(TEntity entity) => _dbcontext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) => _dbcontext.Set<TEntity>().Update(entity);
    }
}
