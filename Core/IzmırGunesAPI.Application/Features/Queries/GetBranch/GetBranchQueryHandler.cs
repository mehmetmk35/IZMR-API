using IzmirGunesAPI.Application.Abstractions.Services;
using IzmirGunesAPI.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Features.Queries.GetBranch
{
    public class GetBranchQueryHandler : IRequestHandler<GetBranchQueryRequest, GetBranchQueryResponse>
    {
        private readonly IGetBranchService _getBranchService;

        public GetBranchQueryHandler(IGetBranchService getBranchService)
        {
            _getBranchService = getBranchService;
        }

        public async Task<GetBranchQueryResponse> Handle(GetBranchQueryRequest request, CancellationToken cancellationToken)
        {
            List<Branch> result = await _getBranchService.GetBranch(request.Company,request.BusinessCode);

            return new() { Branch = result };
        }
    }
}
