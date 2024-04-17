using IzmirGunesAPI.Application.Abstractions.Services;
using IzmirGunesAPI.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Features.Queries.GetCompany
{
    public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQueryRequest, GetCompanyQueryResponse>
    {
        private IGetCompany _getCompany;

        public GetCompanyQueryHandler(IGetCompany getCompany)
        {
            _getCompany = getCompany;
        }

        async Task<GetCompanyQueryResponse> IRequestHandler<GetCompanyQueryRequest, GetCompanyQueryResponse>.Handle(GetCompanyQueryRequest request, CancellationToken cancellationToken)
        {
           List<Company> result= await _getCompany.GetCompanyList();

            return new() { CompanyName=result };


        }
    }
}
