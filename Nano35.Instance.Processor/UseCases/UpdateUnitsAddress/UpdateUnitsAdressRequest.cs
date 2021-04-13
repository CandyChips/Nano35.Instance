﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsAddress
{
    public class UpdateUnitsAddressUseCase :
        EndPointNodeBase<
            IUpdateUnitsAddressRequestContract,
            IUpdateUnitsAddressResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateUnitsAddressUseCase(ApplicationContext context)
        {
            _context = context;
        }

        public override async Task<IUpdateUnitsAddressResultContract> Ask(
            IUpdateUnitsAddressRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Units.FirstOrDefaultAsync(a => a.Id == input.UnitId, cancellationToken));
            result.Adress = input.Address;
            result.CreatorId = input.UpdaterId;
            return new UpdateUnitsAddressSuccessResultContract();
        }
    }
}