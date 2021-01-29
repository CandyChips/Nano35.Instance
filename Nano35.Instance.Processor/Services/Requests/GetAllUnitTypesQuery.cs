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
    public class GetAllUnitTypesQuery : 
        IGetAllUnitTypesRequestContract,
        IQueryRequest<IGetAllUnitTypesResultContract>
    {
        public class GetAllUnitTypesResultContract : 
            IGetAllUnitTypesSuccessResultContract
        {
            public IEnumerable<IUnitTypeViewModel> Data { get; set; }
        }

        public class GetAllUnitTypesHandler 
            : IRequestHandler<GetAllUnitTypesQuery, IGetAllUnitTypesResultContract>
        {
            private readonly ApplicationContext _context;
            public GetAllUnitTypesHandler(
                ApplicationContext context)
            {
                _context = context;
            }
            
            public async Task<IGetAllUnitTypesResultContract> Handle(
                GetAllUnitTypesQuery message,
                CancellationToken cancellationToken)
            {
                var result = await (_context.UnitTypes
                    .MapAllToAsync<IUnitTypeViewModel>());
                return new GetAllUnitTypesResultContract() {Data = result};
            }
        }
    }
}