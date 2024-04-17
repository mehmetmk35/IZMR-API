using IzmirGunesAPI.Application.Abstractions.Services;
using IzmirGunesAPI.Application.Repositorys.Repositorys.TBLFATUIRS;
using IzmirGunesAPI.Application.Repositorys.Repositorys.TBLSTHAR;
using IzmirGunesAPI.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IRestService, RestService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
           

        }
    }
}
