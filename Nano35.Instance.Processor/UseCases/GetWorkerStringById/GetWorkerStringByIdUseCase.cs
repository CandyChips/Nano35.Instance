﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerStringById
{
    public class GetWorkerStringByIdUseCase :
        EndPointNodeBase<
            IGetWorkerStringByIdRequestContract,
            IGetWorkerStringByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetWorkerStringByIdUseCase(ApplicationContext context)
        {
            _context = context;
        }

        public override async Task<IGetWorkerStringByIdResultContract> Ask(
            IGetWorkerStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Workers
                .FirstOrDefaultAsync(e => e.Id == input.WorkerId, cancellationToken: cancellationToken))
                .ToString();
            return new GetWorkerStringByIdSuccessResultContract() {Data = result};
        }
    }
}