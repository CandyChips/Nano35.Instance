﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.DeleteUnit
{
    public class DeleteUnitUseCase :
        EndPointNodeBase<
            IDeleteUnitRequestContract, 
            IDeleteUnitResultContract>
    {
        private readonly ApplicationContext _context;

        public DeleteUnitUseCase(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<IDeleteUnitResultContract> Ask(
            IDeleteUnitRequestContract input,
            CancellationToken cancellationToken)
        {
            var entity = await _context.Units.FirstAsync(e => e.Id == input.UnitId, cancellationToken: cancellationToken);
            entity.Deleted = true;
            return new DeleteUnitSuccessResultContract();
        }
    }
}