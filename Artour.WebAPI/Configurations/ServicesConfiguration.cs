using Artour.BLL.Services;
using Artour.BLL.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artour.WebAPI.Configurations
{
    public static class ServicesConfiguration
    {
        public static void ConfigureBLLServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
