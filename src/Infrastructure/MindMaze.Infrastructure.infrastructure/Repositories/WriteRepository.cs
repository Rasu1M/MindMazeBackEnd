using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindMaze.Core.Application.Interfaces;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Core.Domain.BaseClasses;
using MindMaze.Infrastructure.infrastructure.Data;

namespace MindMaze.Infrastructure.infrastructure.Repositories
{
    public class WriteRepository<Tentity> : IWriteGenericRepository<Tentity> where Tentity : BaseClass
    {

        private readonly ApplicationDBContext _dbContext;
        private readonly DbSet<Tentity> _dbSet;


        public WriteRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Tentity>();
        }

        public IQueryable<Tentity> GetAsQueryable() => _dbSet.AsQueryable();

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

        public  void DeleteRange(IEnumerable<Tentity> entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public void DeletebyID(Guid id)
        {
           var entity = _dbSet.FirstOrDefault(x => x.ID == id);

            if (entity == null)
                return ;

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
