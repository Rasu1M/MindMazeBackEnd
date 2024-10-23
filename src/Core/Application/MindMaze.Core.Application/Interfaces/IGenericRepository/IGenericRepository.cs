using MindMaze.Core.Domain.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Interfaces.IGenericRepository
{
    public interface IGenericRepository<TEntity> : IReadGenericRepository<TEntity>, IWriteGenericRepository<TEntity> where TEntity : BaseClass
    {
    }
}
