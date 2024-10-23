using Microsoft.EntityFrameworkCore;
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
    public class ReadRepository<Tentity> : IReadGenericRepository<Tentity> where Tentity : BaseClass
    {

        private readonly ApplicationDBContext _dbContext;
        private readonly DbSet<Tentity> _dbSet;


        public ReadRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Tentity>();
        }

        public IQueryable<Tentity> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

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

        
    }
}
