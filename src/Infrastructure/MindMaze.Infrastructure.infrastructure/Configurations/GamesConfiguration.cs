using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindMaze.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Infrastructure.infrastructure.Configurations
{
    public class GamesConfiguration : BaseConfiguration<Games>
    {
        public override void Configure(EntityTypeBuilder<Games> builder)
        {

            base.Configure(builder);

            builder.ToTable("Games");

            builder.HasMany(x => x.gamesQuestions).WithOne(y => y.Game)
                .HasForeignKey(x => x.Game_ID).OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
