using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindMaze.Core.Domain.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Infrastructure.infrastructure.Configurations
{
    public class BaseConfiguration<Tentity> : IEntityTypeConfiguration<Tentity> where Tentity : BaseClass
    {
        public virtual void Configure(EntityTypeBuilder<Tentity> builder)
        {
            builder.HasKey(x => x.ID);
        }
    }
}
