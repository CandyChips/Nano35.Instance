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
    public class GetAllWorkerRolesQuery : 
        IGetAllWorkerRolesRequestContract, 
        IQueryRequest<IGetAllWorkerRolesResultContract>
    {
        private class GetAllWorkerRolesSuccessResultContract : 
            IGetAllWorkerRolesSuccessResultContract
        {
            public IEnumerable<IWorkersRoleViewModel> Data { get; set; }
        }

        public class GetAllWorkerRolesHandler 
            : IRequestHandler<GetAllWorkerRolesQuery, IGetAllWorkerRolesResultContract>
        {
            private readonly ApplicationContext _context;
            public GetAllWorkerRolesHandler(
                ApplicationContext context)
            {
                _context = context;
            }

            public async Task<IGetAllWorkerRolesResultContract> Handle(
                GetAllWorkerRolesQuery message,
                CancellationToken cancellationToken)
            {
                var result = await this._context.WorkerRoles
                    .MapAllToAsync<IWorkersRoleViewModel>();
                return new GetAllWorkerRolesSuccessResultContract() {Data = result};
            }
        }
    }
}