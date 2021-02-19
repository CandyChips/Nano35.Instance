using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateClientsPhone
{
    public class UpdateClientsPhoneRequest :
        IPipelineNode<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateClientsPhoneRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateClientsPhoneSuccessResultContract : 
            IUpdateClientsPhoneSuccessResultContract
        {
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetAllClientStatesErrorResultContract
        {
            public string Message { get; set; }
        }

        public async Task<IUpdateClientsPhoneResultContract> Ask(
            IUpdateClientsPhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await ( _context.Clients
                    .FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken)
                );

            result.Phone = input.Phone;
            return new UpdateClientsPhoneSuccessResultContract();
        }
    }
}