using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.GetAllUnits
{
    public class GetAllUnitsRequest :
        IPipelineNode<IGetAllUnitsRequestContract, IGetAllUnitsResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllUnitsRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllUnitsSuccessResultContract : 
            IGetAllUnitsSuccessResultContract
        {
            public IEnumerable<IUnitViewModel> Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetAllClientStatesErrorResultContract
        {
            public string Message { get; set; }
        }

        public async Task<IGetAllUnitsResultContract> Ask(IGetAllUnitsRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Units
                .Where(c => c.InstanceId == input.InstanceId)
                .MapAllToAsync<IUnitViewModel>());
            return new GetAllUnitsSuccessResultContract() {Data = result};
        }
    }
}