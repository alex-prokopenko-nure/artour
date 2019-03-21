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
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IToursService, ToursService>();
            services.AddScoped<ISightsService, SightsService>();
            services.AddScoped<IVisitsService, VisitsService>();
            services.AddScoped<ISightImagesService, SightImagesService>();
            services.AddScoped<ISightSeensService, SightSeensService>();
        }
    }
}
