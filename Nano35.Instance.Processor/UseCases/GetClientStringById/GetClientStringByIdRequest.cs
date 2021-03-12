using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetClientStringById
{
    public class GetClientStringByIdRequest :
        IPipelineNode<IGetClientStringByIdRequestContract, IGetClientStringByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetClientStringByIdRequest(ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetClientStringByIdSuccessResultContract : 
            IGetClientStringByIdSuccessResultContract
        {
            public string Data { get; set; }
        }

        public async Task<IGetClientStringByIdResultContract> Ask(
            IGetClientStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Clients.FirstOrDefaultAsync(e => e.Id == input.ClientId, cancellationToken: cancellationToken)).ToString();
            return new GetClientStringByIdSuccessResultContract() {Data = result};
        }
    }
}