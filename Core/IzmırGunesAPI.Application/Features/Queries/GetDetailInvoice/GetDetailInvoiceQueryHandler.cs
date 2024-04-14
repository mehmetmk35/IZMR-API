using IzmirGunesAPI.Application.DTOs;
using IzmirGunesAPI.Application.Features.Queries.GetInvoice;
using IzmirGunesAPI.Application.Repositorys.Repositorys.TBLFATUIRS;
using IzmirGunesAPI.Application.Repositorys.Repositorys.TBLSTHAR;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IzmirGunesAPI.Application.Features.Queries.GetDetailInvoice
{
    public class GetDetailInvoiceQueryHandler : IRequestHandler<GetDetailInvoiceQueryRequest, GetDetailInvoiceQueryResponse>
    {
        private readonly IDetailTblSthar _IDetailInvoice;
        private readonly IInvoiceCount _InvoiceCount;
        private readonly ILogger<GetInvoiceQueryHandler> _logger;
        private readonly IGetDetailInvoiceCount _GetDetailInvoiceCount;

        public GetDetailInvoiceQueryHandler(IDetailTblSthar ıDetailInvoice, IInvoiceCount ınvoiceCount, ILogger<GetInvoiceQueryHandler> logger, IGetDetailInvoiceCount getDetailInvoiceCount)
        {
            _IDetailInvoice = ıDetailInvoice;
            _InvoiceCount = ınvoiceCount;
            _logger = logger;
            _GetDetailInvoiceCount = getDetailInvoiceCount;
        }

        public async Task<GetDetailInvoiceQueryResponse> Handle(GetDetailInvoiceQueryRequest request, CancellationToken cancellationToken)
        {

            List <TBLSTHAR> result= await _IDetailInvoice.GetDetailInvoice( request.InvoiceNumber);
            int GetDedailInvıiceCount = await _GetDetailInvoiceCount.Count(request.InvoiceNumber);
            _logger.LogInformation($"{request.InvoiceNumber} Detay Liste Cekildi");
            return new() { DetailInvoice = result,DetailInvoiceCount=GetDedailInvıiceCount };
        



        }
    }
}
