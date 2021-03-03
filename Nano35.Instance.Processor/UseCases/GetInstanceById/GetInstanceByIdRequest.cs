using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetInstanceById
{
    public class GetInstanceByIdRequest :
        IPipelineNode<
            IGetInstanceByIdRequestContract,
            IGetInstanceByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetInstanceByIdRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetInstanceByIdSuccessResultContract : 
            IGetInstanceByIdSuccessResultContract
        {
            public IInstanceViewModel Data { get; set; }
        }

        public async Task<IGetInstanceByIdResultContract> Ask(
            IGetInstanceByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Instances
                    .FirstOrDefaultAsync(f => f.Id == input.InstanceId, cancellationToken: cancellationToken))
                .MapTo<IInstanceViewModel>();
            return new GetInstanceByIdSuccessResultContract() {Data = result};
        }
    }
}