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
    public class MyFriendsConfiguration :BaseConfiguration<MyFriends>
    {
        public override void Configure(EntityTypeBuilder<MyFriends> builder)
        {
            base.Configure(builder);

            builder.ToTable(nameof(MyFriends));
        }
    }
}
