using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetAllUnitTypes
{
    public class GetAllUnitTypesRequest :
        IPipelineNode<IGetAllUnitTypesRequestContract, IGetAllUnitTypesResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllUnitTypesRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllUnitTypesSuccessResultContract : 
            IGetAllUnitTypesSuccessResultContract
        {
            public IEnumerable<IUnitTypeViewModel> Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetAllClientStatesErrorResultContract
        {
            public string Message { get; set; }
        }

        public async Task<IGetAllUnitTypesResultContract> Ask(IGetAllUnitTypesRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.UnitTypes
                .MapAllToAsync<IUnitTypeViewModel>());
            return new GetAllUnitTypesSuccessResultContract() {Data = result};
        }
    }
}