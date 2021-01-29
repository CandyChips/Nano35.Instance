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
    public class GetWorkerByIdQuery : 
        IGetWorkerByIdRequestContract,
        IQueryRequest<IGetWorkerByIdResultContract>
    {
        public Guid WorkerId { get; set; }

        private class GetWorkerByIdResultContract :
            IGetWorkerByIdSuccessResultContract
        {
            public IWorkerViewModel Data { get; set; }
        }

        public class GetWorkerByIdHandler 
            : IRequestHandler<GetWorkerByIdQuery, IGetWorkerByIdResultContract>
        {
            private readonly ApplicationContext _context;
            public GetWorkerByIdHandler(
                ApplicationContext context)
            {
                _context = context;
            }

            public async Task<IGetWorkerByIdResultContract> Handle(
                GetWorkerByIdQuery message,
                CancellationToken cancellationToken)
            {
                var result = this._context.Workers
                    .MapTo<IWorkerViewModel>();
                return new GetWorkerByIdResultContract() {Data = result};
            }
        }

    }
}