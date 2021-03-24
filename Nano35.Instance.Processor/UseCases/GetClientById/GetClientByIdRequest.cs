﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetClientById
{
    public class GetClientByIdRequest :
        EndPointNodeBase<
            IGetClientByIdRequestContract,
            IGetClientByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetClientByIdRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetClientByIdSuccessResultContract : 
            IGetClientByIdSuccessResultContract
        {
            public IClientViewModel Data { get; set; }
        }

        public override async Task<IGetClientByIdResultContract> Ask(
            IGetClientByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Clients
                    .FirstOrDefaultAsync(f => f.Id == input.UnitId, cancellationToken: cancellationToken))
                .MapTo<IClientViewModel>();
            return new GetClientByIdSuccessResultContract() {Data = result};
        }
    }
}