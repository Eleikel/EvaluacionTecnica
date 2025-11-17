using EvaluacionTecnica.Business.Interfaces.Service;
using EvaluacionTecnica.Business.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IRoleService, RoleServices>();
            services.AddTransient<IUserService, UserServices>();
            services.AddTransient<IAuthService, AuthServices>();

            return services;
        }

        public static IServiceCollection AddDtoMaping(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
