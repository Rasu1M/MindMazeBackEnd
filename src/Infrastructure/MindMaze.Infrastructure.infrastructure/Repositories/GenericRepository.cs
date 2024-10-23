using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Core.Domain.BaseClasses;
using MindMaze.Infrastructure.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Infrastructure.infrastructure.Repositories
{
    public class GenericRepository<Tentity> : IGenericRepository<Tentity> where Tentity : BaseClass
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly DbSet<Tentity> _dbSet;


        public GenericRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Tentity>();
        }

        public IQueryable<Tentity> AsQueryable() => _dbSet.AsQueryable();

        public IQueryable<Tentity> GetAsQueryable() => _dbSet.AsQueryable();

        public IEnumerable<Tentity> GetAll(Func<Tentity, bool> Condition)
        {
            return AsQueryable().Where(Condition);
        }

        public async Task<IEnumerable<Tentity?>> GetAllAsync(Func<Tentity, bool> Condition)
        {
            return GetAll(Condition);
        }

        public Tentity? GetByID(Guid id)
        {
            return _dbSet.FirstOrDefault(x => x.ID == id);
        }

        public async Task<Tentity?> GetByIDAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.ID == id);
        }

        public Tentity? Get(Func<Tentity, bool> Condition)
        {
            return _dbSet.FirstOrDefault(Condition);
        }

        public async Task<Tentity?> Getasync(Expression<Func<Tentity, bool>> Condition)
        {
            return await _dbSet.FirstOrDefaultAsync(Condition);
        }

        public void Add(Tentity entity)
        {
            _dbSet.Add(entity);
        }

        public async Task Addasync(Tentity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void AddRange(IEnumerable<Tentity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<Tentity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Delete(Tentity entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<Tentity> entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public void DeletebyID(Guid id)
        {
            var entity = _dbSet.FirstOrDefault(x => x.ID == id);

            if (entity == null)
                return;

            _dbSet.Remove(entity);

        }

        public async Task DeletebyIDasync(Guid id)
        {
            DeletebyID(id);
        }

        public void UpdateEntity(Tentity entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateEntities(IEnumerable<Tentity> entities)
        {
            _dbSet.UpdateRange(entities);
        }


    }
}
