﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetUnitById
{
    public class GetUnitByIdUseCase :
        EndPointNodeBase<
            IGetUnitByIdRequestContract,
            IGetUnitByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetUnitByIdUseCase(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetUnitByIdSuccessResultContract : 
            IGetUnitByIdSuccessResultContract
        {
            public IUnitViewModel Data { get; set; }
        }

        public override async Task<IGetUnitByIdResultContract> Ask(
            IGetUnitByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Units
                    .FirstOrDefaultAsync(f => f.Id == input.UnitId, cancellationToken: cancellationToken))
                .MapTo<IUnitViewModel>();
            return new GetUnitByIdSuccessResultContract() {Data = result};
        }
    }
}