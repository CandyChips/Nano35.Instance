using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateInstanceRealName
{
    public class UpdateInstanceRealNameRequest :
        IPipelineNode<IUpdateInstanceRealNameRequestContract, IUpdateInstanceRealNameResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateInstanceRealNameRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateInstanceRealNameSuccessResultContract : 
            IUpdateInstanceRealNameSuccessResultContract
        {
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetAllClientStatesErrorResultContract
        {
            public string Message { get; set; }
        }

        public async Task<IUpdateInstanceRealNameResultContract> Ask(
            IUpdateInstanceRealNameRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await ( _context.Instances
                    .FirstOrDefaultAsync(a => a.Id == input.InstanceId, cancellationToken)
                );

            result.OrgRealName = input.RealName;
            return new UpdateInstanceRealNameSuccessResultContract();
        }
    }
}