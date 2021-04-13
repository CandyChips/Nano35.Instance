﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsWorkingFormat
{
    public class UpdateUnitsWorkingFormatUseCase :
        EndPointNodeBase<
            IUpdateUnitsWorkingFormatRequestContract,
            IUpdateUnitsWorkingFormatResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateUnitsWorkingFormatUseCase(
            ApplicationContext context)
        {
            _context = context;
        }

        public override async Task<IUpdateUnitsWorkingFormatResultContract> Ask(
            IUpdateUnitsWorkingFormatRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.Units.FirstOrDefaultAsync(a => a.Id == input.UnitId, cancellationToken);
            result.WorkingFormat = input.WorkingFormat;
            result.CreatorId = input.UpdaterId;
            return new UpdateUnitsWorkingFormatSuccessResultContract();
        }
    }
}