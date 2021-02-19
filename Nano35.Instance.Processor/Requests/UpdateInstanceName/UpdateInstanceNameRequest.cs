using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateInstanceName
{
    public class UpdateInstanceNameRequest :
        IPipelineNode<IUpdateInstanceNameRequestContract, IUpdateInstanceNameResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateInstanceNameRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateInstanceNameSuccessResultContract : 
            IUpdateInstanceNameSuccessResultContract
        {
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetAllClientStatesErrorResultContract
        {
            public string Message { get; set; }
        }

        public async Task<IUpdateInstanceNameResultContract> Ask(
            IUpdateInstanceNameRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await ( _context.Instances
                    .FirstOrDefaultAsync(a => a.Id == input.InstanceId, cancellationToken)
                );

            result.OrgName = input.Name;
            return new UpdateInstanceNameSuccessResultContract();
        }
    }
}