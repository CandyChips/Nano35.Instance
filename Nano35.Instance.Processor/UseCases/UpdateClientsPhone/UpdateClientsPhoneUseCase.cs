﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsPhone
{
    public class UpdateClientsPhoneUseCase :
        EndPointNodeBase<
            IUpdateClientsPhoneRequestContract, 
            IUpdateClientsPhoneResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateClientsPhoneUseCase(ApplicationContext context)
        {
            _context = context;
        }

        public override async Task<IUpdateClientsPhoneResultContract> Ask(
            IUpdateClientsPhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Clients.FirstOrDefaultAsync(a => a.Id == input.ClientId, cancellationToken));
            result.WorkerId = input.UpdaterId;
            result.Phone = input.Phone;
            return new UpdateClientsPhoneSuccessResultContract();
        }
    }
}