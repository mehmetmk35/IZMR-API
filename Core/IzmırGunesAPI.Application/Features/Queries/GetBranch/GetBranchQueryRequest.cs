using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Features.Queries.GetBranch
{
    public class GetBranchQueryRequest:IRequest<GetBranchQueryResponse>
    {
        public string Company { get; set; }
    }
}
