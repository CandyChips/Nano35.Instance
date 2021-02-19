using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.GetClientById
{
    public class GetClientByIdRequest :
        IPipelineNode<IGetClientByIdRequestContract, IGetClientByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetClientByIdRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetClientByIdSuccessResultContract : 
            IGetClientByIdSuccessResultContract
        {
            public IClientViewModel Data { get; set; }
        }

        private class GetClientByIdErrorResultContract : 
            IGetClientByIdErrorResultContract
        {
            public string Message { get; set; }
        }

        public async Task<IGetClientByIdResultContract> Ask(IGetClientByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Clients
                    .FirstOrDefaultAsync(f => f.Id == input.UnitId, cancellationToken: cancellationToken))
                .MapTo<IClientViewModel>();
            return new GetClientByIdSuccessResultContract() {Data = result};
        }
    }
}