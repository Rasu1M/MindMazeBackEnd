using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindMaze.Core.Application.Interfaces;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Infrastructure.infrastructure.Data;
using MindMaze.Infrastructure.infrastructure.Repositories;
using MindMaze.Infrastructure.infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Infrastructure.infrastructure.Registration
{
    public static class InfrastructureRegistration
    {

        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddDbContext<ApplicationDBContext>(sp =>
            {

                var connstr = configuration["ConnectionStrings:CloudSqlServer"];

                sp.UseSqlServer(connstr, op =>
                {
                    op.EnableRetryOnFailure();
                    op.CommandTimeout(100);
                });

                sp.AddInterceptors(new CustomSaveChangesInterceptor());

                sp.EnableDetailedErrors(true);
                //TODO change this after
                sp.EnableSensitiveDataLogging(true);
            });

            services.AddScoped(typeof(IReadGenericRepository<>), typeof(ReadRepository<>));

            services.AddScoped(typeof(IWriteGenericRepository<>), typeof(WriteRepository<>));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();






            return services;
        }
    }
}
