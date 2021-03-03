using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetAllUnitsByType
{
    public class GetAllUnitsByTypeRequest :
        IPipelineNode<
            IGetAllUnitsByTypeRequestContract, 
            IGetAllUnitsByTypeResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllUnitsByTypeRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllUnitsByTypeSuccessResultContract : 
            IGetAllUnitsByTypeSuccessResultContract
        {
            public IEnumerable<IUnitViewModel> Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetAllClientStatesErrorResultContract
        {
            public string Message { get; set; }
        }

        public async Task<IGetAllUnitsByTypeResultContract> Ask(
            IGetAllUnitsByTypeRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Units)
                .Where(c => c.UnitTypeId == input.UnitTypeId)
                .MapAllToAsync<IUnitViewModel>();
            return new GetAllUnitsByTypeSuccessResultContract() {Data = result};
        }
    }
}