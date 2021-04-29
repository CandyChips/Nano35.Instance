﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceRealName
{
    public class UpdateInstanceRealNameUseCase :
        EndPointNodeBase<
            IUpdateInstanceRealNameRequestContract,
            IUpdateInstanceRealNameResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateInstanceRealNameUseCase(ApplicationContext context) => _context = context;
        public override async Task<IUpdateInstanceRealNameResultContract> Ask(
            IUpdateInstanceRealNameRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.Instances.FirstOrDefaultAsync(a => a.Id == input.InstanceId, cancellationToken);
            result.OrgRealName = input.RealName;
            return new UpdateInstanceRealNameSuccessResultContract();
        }
    }
}