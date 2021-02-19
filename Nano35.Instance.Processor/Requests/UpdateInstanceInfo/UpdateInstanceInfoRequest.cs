using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateInstanceInfo
{
    public class UpdateInstanceInfoRequest :
        IPipelineNode<
            IUpdateInstanceInfoRequestContract, 
            IUpdateInstanceInfoResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateInstanceInfoRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateInstanceInfoSuccessResultContract : 
            IUpdateInstanceInfoSuccessResultContract
        {
            
        }

        public async Task<IUpdateInstanceInfoResultContract> Ask(
            IUpdateInstanceInfoRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await ( _context.Instances
                    .FirstOrDefaultAsync(a => a.Id == input.InstanceId, cancellationToken)
                );

            result.CompanyInfo = input.Info;
            return new UpdateInstanceInfoSuccessResultContract();
        }
    }
}