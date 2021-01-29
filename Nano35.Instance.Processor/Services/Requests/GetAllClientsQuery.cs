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
    public class GetAllClientsQuery : 
        IGetAllClientsRequestContract,
        IQueryRequest<IGetAllClientsResultContract>
    {
        public Guid ClientTypeId { get; set; }
        public Guid ClientStateId { get; set; }
        public Guid InstanceId { get; set; }

        private class GetAllClientsResultContract : 
            IGetAllClientsSuccessResultContract
        {
            public IEnumerable<IClientViewModel> Data { get; set; }
        }

        public class GetAllClientsHandler 
            : IRequestHandler<GetAllClientsQuery, IGetAllClientsResultContract>
        {
            private readonly ApplicationContext _context;
            public GetAllClientsHandler(
                ApplicationContext context)
            {
                _context = context;
            }
            
            public async Task<IGetAllClientsResultContract> Handle(
                GetAllClientsQuery message,
                CancellationToken cancellationToken)
            {
                var result = await (_context.Clients
                    .Where(c => c.InstanceId == message.InstanceId)
                    .MapAllToAsync<IClientViewModel>());
                return new GetAllClientsResultContract() {Data = result};
            }
        }
    }
}