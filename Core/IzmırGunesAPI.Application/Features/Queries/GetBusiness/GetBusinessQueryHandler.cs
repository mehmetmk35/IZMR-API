using IzmirGunesAPI.Application.Abstractions.Services;
using IzmirGunesAPI.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Features.Queries.GetBusiness
{
    public class GetBusinessQueryHandler : IRequestHandler<GetBusinessQueryRequest, GetBusinessQueryResponse>
    {
        private readonly IGetBusinessService _service;

        public GetBusinessQueryHandler(IGetBusinessService service)
        {
            _service = service;
        }

        public async Task<GetBusinessQueryResponse> Handle(GetBusinessQueryRequest request, CancellationToken cancellationToken)
        {
            List<Business> result = await _service.GetBusinessList(request.Company);
            return new() { Business = result };





        }
    }
}
