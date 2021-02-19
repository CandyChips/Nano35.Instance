using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateInstancePhone
{
    public class UpdateInstancePhoneRequest :
        IPipelineNode<
            IUpdateInstancePhoneRequestContract, 
            IUpdateInstancePhoneResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateInstancePhoneRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateInstancePhoneSuccessResultContract : 
            IUpdateInstancePhoneSuccessResultContract
        {
            
        }

        public async Task<IUpdateInstancePhoneResultContract> Ask(
            IUpdateInstancePhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await ( _context.Instances
                    .FirstOrDefaultAsync(a => a.Id == input.InstanceId, cancellationToken)
                );

            //result. = input.Phone;
            return new UpdateInstancePhoneSuccessResultContract() ;
        }
    }
}