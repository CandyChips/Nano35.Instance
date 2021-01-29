using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;
using Nano35.Instance.Processor.Services.Requests.Behaviours;

namespace Nano35.Instance.Processor.Services.Requests
{
    public class GetAllClientTypesQuery : 
        IGetAllClientTypesRequestContract,
        IQueryRequest<IGetAllClientTypesResultContract>
    {
        
        public GetAllClientTypesQuery(IGetAllClientTypesRequestContract message)
        {
        }
        
        public class GetAllClientTypesResultContract : 
            IGetAllClientTypesSuccessResultContract
        {
            public IEnumerable<IClientTypeViewModel> Data { get; set; }
        }

        public class GetAllClientTypesHandler 
            : IRequestHandler<GetAllClientTypesQuery, IGetAllClientTypesResultContract>
        {
            private readonly ApplicationContext _context;
            public GetAllClientTypesHandler(
                ApplicationContext context)
            {
                _context = context;
            }
            
            public async Task<IGetAllClientTypesResultContract> Handle(
                GetAllClientTypesQuery message,
                CancellationToken cancellationToken)
            {
                var result = await (_context.ClientTypes
                    .MapAllToAsync<IClientTypeViewModel>());
                return new GetAllClientTypesResultContract() {Data = result};
            }
        }
    }
}