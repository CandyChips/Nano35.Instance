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
    public class GetAllUnitsQuery : 
        IGetAllUnitsRequestContract,
        IQueryRequest<IGetAllUnitsResultContract>
    {
        public Guid InstanceId { get; set; }
        public Guid UnitTypeId { get; set; }

        public GetAllUnitsQuery(IGetAllUnitsRequestContract message)
        {
            InstanceId = message.InstanceId;
            UnitTypeId = message.UnitTypeId;
        }

        private class GetAllUnitsResultContract : 
            IGetAllUnitsSuccessResultContract
        {
            public IEnumerable<IUnitViewModel> Data { get; set; }
        }

        public class GetAllUnitsHandler 
            : IRequestHandler<GetAllUnitsQuery, IGetAllUnitsResultContract>
        {
            private readonly ApplicationContext _context;
            public GetAllUnitsHandler(
                ApplicationContext context)
            {
                _context = context;
            }
            
            public async Task<IGetAllUnitsResultContract> Handle(
                GetAllUnitsQuery message,
                CancellationToken cancellationToken)
            {
                var result = await (_context.Units
                    .Where(c => c.InstanceId == message.InstanceId)
                    .MapAllToAsync<IUnitViewModel>());
                return new GetAllUnitsResultContract() {Data = result};
            }
        }
    }
}