using Microsoft.Extensions.DependencyInjection;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Registration
{
    public static class ApplicationRegistration
    {

        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.AddMediatR(con =>
            {
                con.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            
           return services;
        }
    }
}
