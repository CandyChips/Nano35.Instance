using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateUnitsPhone
{
    public class UpdateUnitsPhoneRequest :
        IPipelineNode<
            IUpdateUnitsPhoneRequestContract,
            IUpdateUnitsPhoneResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateUnitsPhoneRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateUnitsPhoneSuccessResultContract : 
            IUpdateUnitsPhoneSuccessResultContract
        {
            
        }

        public async Task<IUpdateUnitsPhoneResultContract> Ask(
            IUpdateUnitsPhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await ( _context.Units
                    .FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken)
                );

            result.Phone = input.Phone;
            return new UpdateUnitsPhoneSuccessResultContract() ;
        }
    }
}