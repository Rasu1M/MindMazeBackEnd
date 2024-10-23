using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MindMaze.Core.Domain.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Infrastructure.infrastructure.Registration
{
    public class CustomSaveChangesInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
           var entities = eventData.Context.ChangeTracker.Entries().Where(entry => entry.State == EntityState.Added).Select(entry => (BaseClass)(entry.Entity)).ToList();

            foreach (var entity in entities)
            {
                entity.ID = Guid.NewGuid();
                entity.Created_Date = DateTime.UtcNow;
            }

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var entities = eventData.Context.ChangeTracker.Entries().Where(entry => entry.State == EntityState.Added).Select(entry => (BaseClass)(entry.Entity)).ToList();

            foreach (var entity in entities)
            {
                entity.ID = Guid.NewGuid();
                entity.Created_Date = DateTime.UtcNow;
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
