using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Features.Queries.GetInvoice
{
    public class GetInvoiceQueryResponse
    {
        public int InvoiceCount { get; set; }
        public object Invoices { get; set; }
    }
}


