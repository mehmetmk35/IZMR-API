using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Features.Queries.GetCompany
{
    public class GetCompanyQueryRequest:IRequest<GetCompanyQueryResponse>   
    {
    }
}
