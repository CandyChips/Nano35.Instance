using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;
using Nano35.Instance.Processor.Services.Requests.Behaviours;

namespace Nano35.Instance.Processor.Services.Requests
{
    public class GetAllInstanceTypesQuery : 
        IGetAllInstanceTypesRequestContract, 
        IQueryRequest<IGetAllInstanceTypesResultContract>
    {
        private class GetAllInstanceTypesSuccessResultContract : 
            IGetAllInstanceTypesSuccessResultContract
        {
            public IEnumerable<IInstanceTypeViewModel> Data { get; set; }
        }

        public class GetAllInstanceTypesHandler 
            : IRequestHandler<GetAllInstanceTypesQuery, IGetAllInstanceTypesResultContract>
        {
            private readonly ApplicationContext _context;
            public GetAllInstanceTypesHandler(
                ApplicationContext context)
            {
                _context = context;
            }

            public async Task<IGetAllInstanceTypesResultContract> Handle(
                GetAllInstanceTypesQuery message,
                CancellationToken cancellationToken)
            {
                var result = await this._context.InstanceTypes
                    .MapAllToAsync<IInstanceTypeViewModel>();
                return new GetAllInstanceTypesSuccessResultContract() {Data = result};
            }
        }
    }
}