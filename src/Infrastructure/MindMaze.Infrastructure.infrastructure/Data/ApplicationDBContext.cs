using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindMaze.Core.Domain;
using Bogus;
using MindMaze.Core.Application.Extensions.PassWordGenerator;
using System.Security.Cryptography;
using MindMaze.Infrastructure.infrastructure.Configurations;

namespace MindMaze.Infrastructure.infrastructure.Data
{
    public class ApplicationDBContext : DbContext
    {
       

        public ApplicationDBContext()
        {

        }

        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Users> users { get; set; }

        public DbSet<Games> Games { get; set; }

        public DbSet<Questions> Questions { get; set; }

        public DbSet<Notifications> Notifications { get; set; }

        public DbSet<MyFriends> MyFriends { get; set; }

        public DbSet<GamesQuestions> GamesQuestions { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                var connecstr = "Data Source=34.159.231.199;Initial Catalog=MindMazeDB;User id=sqlserver;Password=MINDMAZE;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

                optionsBuilder.UseSqlServer(connecstr, op =>
                {
                    op.EnableRetryOnFailure();
                    op.CommandTimeout(100);
                });
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}