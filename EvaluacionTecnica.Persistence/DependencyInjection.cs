using EvaluacionTecnica.Persistence.Context;
using EvaluacionTecnica.Persistence.Interfaces.Repository;
using EvaluacionTecnica.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts


            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                //m => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAuthRepository, AuthRepository>();

            return services;
            #endregion
        }
    }
}
