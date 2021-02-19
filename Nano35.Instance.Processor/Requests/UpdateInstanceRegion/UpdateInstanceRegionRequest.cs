using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateInstanceRegion
{
    public class UpdateInstanceRegionRequest :
        IPipelineNode<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateInstanceRegionRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateInstanceRegionSuccessResultContract : 
            IUpdateInstanceRegionSuccessResultContract
        {
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetAllClientStatesErrorResultContract
        {
            public string Message { get; set; }
        }

        public async Task<IUpdateInstanceRegionResultContract> Ask(
            IUpdateInstanceRegionRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await ( _context.Instances
                    .FirstOrDefaultAsync(a => a.Id == input.InstanceId, cancellationToken)
                );

            result.RegionId = input.Region;
            return new UpdateInstanceRegionSuccessResultContract();
        }
    }
}