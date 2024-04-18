using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Features.Queries.GetDetailInvoice
{
    public class GetDetailInvoiceQueryRequest:IRequest<GetDetailInvoiceQueryResponse>
    {
        public string InvoiceNumber { get; set; }
        public string company { get; set; }

    }
}
