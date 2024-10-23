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
    public class UsersConfiguration : BaseConfiguration<Users>
    {
        public override void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users");

            builder.HasMany(x => x.Games).WithOne(x => x.Opponent)
                .HasForeignKey(x => x.Opponent_ID).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Notifications).WithOne(x => x.Sender)
                .HasForeignKey(x => x.Sender_ID).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.MyFirends).WithOne(x => x.Friend)
                .HasForeignKey(x => x.Friend_ID).OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
