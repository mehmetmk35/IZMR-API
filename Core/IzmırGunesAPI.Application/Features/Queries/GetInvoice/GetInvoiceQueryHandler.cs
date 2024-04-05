using IzmirGunesAPI.Application.DTOs.TBLFATUIRS;
using IzmirGunesAPI.Application.Repositorys.Repositorys.TBLFATUIRS;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IzmirGunesAPI.Application.Features.Queries.GetInvoice
{
    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQueryRequest, GetInvoiceQueryResponse>
    {
        private readonly ITblFatuirsRepository _TBLFATUIRS;
        private readonly IInvoiceCount _InvoiceCount;
        private readonly ILogger<GetInvoiceQueryHandler> _logger;

        public GetInvoiceQueryHandler(ITblFatuirsRepository tBLFATUIRS, ILogger<GetInvoiceQueryHandler> logger, IInvoiceCount ınvoiceCount)
        {
            _TBLFATUIRS = tBLFATUIRS;
            _logger = logger;
            _InvoiceCount = ınvoiceCount;
        }

        public async Task<GetInvoiceQueryResponse> Handle(GetInvoiceQueryRequest request, CancellationToken cancellationToken)
        {
           List<TBLFATUIRS> result = await  _TBLFATUIRS.GetFatuirs(request.Page,request.Size);
            int invoiceCount = await _InvoiceCount.Count();
            _logger.LogInformation("TBLFATUIRS LISTELEME İŞLEMİ TAMAMLANDI");
            
            
            return new() {InvoiceCount = invoiceCount,Invoices = result };
        }
    }
}
