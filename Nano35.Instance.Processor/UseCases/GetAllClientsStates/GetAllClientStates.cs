﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllClientsStates
{
    public class GetAllClientStates : EndPointNodeBase<IGetAllClientStatesRequestContract, IGetAllClientStatesResultContract>
    {
        private readonly ApplicationContext _context;
        public GetAllClientStates(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetAllClientStatesResultContract>> Ask(
            IGetAllClientStatesRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.ClientStates
                .Select(a =>
                    new ClientStateViewModel()
                        {Id = a.Id,
                         Name = a.Name})
                .ToListAsync(cancellationToken);
            return new UseCaseResponse<IGetAllClientStatesResultContract>(new GetAllClientStatesResultContract() {Data = result});
        }
    }
}