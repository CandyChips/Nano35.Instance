using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateClientsEmail
{
    public class UpdateClientsEmailRequest :
        IPipelineNode<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateClientsEmailRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateClientsEmailSuccessResultContract : 
            IUpdateClientsEmailSuccessResultContract
        {
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetAllClientStatesErrorResultContract
        {
            public string Message { get; set; }
        }

        public async Task<IUpdateClientsEmailResultContract> Ask(
            IUpdateClientsEmailRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await ( _context.Clients
                .FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken)
                );

            result.Email = input.Email;

            return new UpdateClientsEmailSuccessResultContract();
        }
    }
}