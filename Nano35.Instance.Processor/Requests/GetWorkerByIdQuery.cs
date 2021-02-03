using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Requests.Behaviours;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests
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
                var result = (await _context.Workers
                    .FirstOrDefaultAsync(f => f.Id == message.WorkerId, cancellationToken: cancellationToken))
                    .MapTo<IWorkerViewModel>();
                return new GetWorkerByIdResultContract() {Data = result};
            }
        }

    }
}