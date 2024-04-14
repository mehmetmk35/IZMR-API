using IzmirGunesAPI.Application.Repositorys.Repositorys.TBLFATUIRS;
using IzmirGunesAPI.Application.Repositorys.Repositorys.TBLSTHAR;
using IzmirGunesAPI.Persistence.Repositorys;
using IzmirGunesAPI.Persistence.Services.InvoiceCount;
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

        }
    }
}
