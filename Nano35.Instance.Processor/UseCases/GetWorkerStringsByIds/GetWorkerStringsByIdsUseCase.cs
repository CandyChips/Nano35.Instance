﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerStringsByIds
{
    public class GetWorkerStringsByIdsUseCase : UseCaseEndPointNodeBase<IGetWorkerStringsByIdsRequestContract, IGetWorkerStringsByIdsSuccessResultContract>
    {
        private readonly ApplicationContext _context;
        public GetWorkerStringsByIdsUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetWorkerStringsByIdsSuccessResultContract>> Ask(
            IGetWorkerStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Workers
                .Where(c => input.WorkerIds.Contains(c.Id))
                .Select(e => $"{e.Name}")
                .ToListAsync(cancellationToken);            
            
            return new UseCaseResponse<IGetWorkerStringsByIdsSuccessResultContract>(new GetWorkerStringsByIdsSuccessResultContract() {Data = result});
        }
    }
}