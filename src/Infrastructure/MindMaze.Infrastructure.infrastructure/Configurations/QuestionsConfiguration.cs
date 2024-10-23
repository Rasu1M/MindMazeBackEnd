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
    public class QuestionsConfiguration : BaseConfiguration<Questions> 
    {
        public override void Configure(EntityTypeBuilder<Questions> builder)
        {
            base.Configure(builder);

            builder.ToTable("Questions");

            builder.HasMany(x => x.GamesQuestions).WithOne(y => y.Question)
                .HasForeignKey(x => x.Question_ID).OnDelete(DeleteBehavior.Cascade);
        
        }
    }
}
