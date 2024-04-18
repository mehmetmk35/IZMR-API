using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Features.Queries.GetBusiness
{
    public class GetBusinessQueryRequest:IRequest<GetBusinessQueryResponse>
    {
        public string Company { get; set; }
    }
}
