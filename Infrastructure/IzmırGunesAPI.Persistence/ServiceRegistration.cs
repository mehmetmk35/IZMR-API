using IzmirGunesAPI.Application.Abstractions.Services;
using IzmirGunesAPI.Application.Repositorys.Repositorys.TBLFATUIRS;
using IzmirGunesAPI.Application.Repositorys.Repositorys.TBLSTHAR;
using IzmirGunesAPI.Persistence.Repositorys;
using IzmirGunesAPI.Persistence.Services;
using IzmirGunesAPI.Persistence.Services.Company;
using IzmirGunesAPI.Persistence.Services.ExistsTable;
using IzmirGunesAPI.Persistence.Services.InvoiceCount;
using IzmirGunesAPI.Persistence.Services.UserService;
using Microsoft.Extensions.DependencyInjection;

namespace IzmirGunesAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddScoped<ITblFatuirsRepository, TblFatuirsRepository>();
            services.AddScoped<IDetailTblSthar, DetailTblSthar>();
            services.AddScoped<IInvoiceCount, InvoiceCount>();
            services.AddScoped<IGetDetailInvoiceCount, GetDetailInvoiceCount>();
            services.AddScoped<IExistsTable, ExistsTable>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGetBusinessService, GetBusinessService>();
            services.AddScoped<IGetCompany, GetCompany>();
            services.AddScoped<IGetBranchService, GetBranchService>();

        }
    }
}
