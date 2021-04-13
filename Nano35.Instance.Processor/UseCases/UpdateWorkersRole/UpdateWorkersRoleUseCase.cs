﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersRole
{
    public class UpdateWorkersRoleUseCase :
        EndPointNodeBase<
            IUpdateWorkersRoleRequestContract,
            IUpdateWorkersRoleResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateWorkersRoleUseCase(ApplicationContext context)
        {
            _context = context;
        }
        

        public override async Task<IUpdateWorkersRoleResultContract> Ask(
            IUpdateWorkersRoleRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfWorker = await _context.Workers.FirstAsync(f => f.Id == input.WorkersId, cancellationToken: cancellationToken);
            entityOfWorker.WorkersRoleId = input.RoleId;
            return new UpdateWorkersRoleSuccessResultContract();
        }
    }
}