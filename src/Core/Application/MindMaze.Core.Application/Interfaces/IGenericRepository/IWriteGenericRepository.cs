using MindMaze.Core.Domain.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Interfaces.IGenericRepository
{
    public interface IWriteGenericRepository<Tentity> where Tentity : BaseClass
    {

        IQueryable<Tentity> GetAsQueryable();

        void Add(Tentity entity);

        Task Addasync(Tentity entity);

        void AddRange(IEnumerable<Tentity> entities);

        Task AddRangeAsync(IEnumerable<Tentity> entities);

        void Delete(Tentity entity);

        void DeleteRange(IEnumerable<Tentity> entity);

        void DeletebyID(Guid id);

        Task DeletebyIDasync(Guid id);

        void UpdateEntity(Tentity entity);

        void UpdateEntities(IEnumerable<Tentity> entities);
    }
}
