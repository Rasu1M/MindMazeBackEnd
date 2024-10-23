using MindMaze.Core.Domain.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Interfaces.IGenericRepository
{
    public interface IReadGenericRepository<Tentity> where Tentity : BaseClass
    {

        IQueryable<Tentity> AsQueryable();

        Tentity GetByID(Guid id);
        Task<Tentity> GetByIDAsync(Guid id);

        Tentity Get(Func<Tentity, bool> Condition);

        Task<Tentity> Getasync(Expression<Func<Tentity, bool>> Condition);

        IEnumerable<Tentity> GetAll(Func<Tentity, bool> Condition);

        Task<IEnumerable<Tentity>> GetAllAsync(Func<Tentity, bool> Condition);
    }
}
