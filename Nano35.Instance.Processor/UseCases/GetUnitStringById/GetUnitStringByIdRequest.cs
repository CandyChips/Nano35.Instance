using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetUnitStringById
{
    public class GetUnitStringByIdRequest :
        IPipelineNode<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetUnitStringByIdRequest(ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetUnitStringByIdSuccessResultContract : 
            IGetUnitStringByIdSuccessResultContract
        {
            public string Data { get; set; }
        }

        public async Task<IGetUnitStringByIdResultContract> Ask(
            IGetUnitStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Units.FirstOrDefaultAsync(e => e.Id == input.UnitId, cancellationToken: cancellationToken)).ToString();
            return new GetUnitStringByIdSuccessResultContract() {Data = result};
        }
    }
}