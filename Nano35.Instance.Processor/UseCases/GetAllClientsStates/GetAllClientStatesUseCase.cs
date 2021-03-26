using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetAllClientsStates
{
    public class GetAllClientStatesUseCase :
        EndPointNodeBase<
            IGetAllClientStatesRequestContract,
            IGetAllClientStatesResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllClientStatesUseCase(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllClientStatesSuccessResultContract : 
            IGetAllClientStatesSuccessResultContract
        {
            public IEnumerable<IClientStateViewModel> Data { get; set; }
        }
        
        public override async Task<IGetAllClientStatesResultContract> Ask(
            IGetAllClientStatesRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.ClientStates
                .MapAllToAsync<IClientStateViewModel>());
            return new GetAllClientStatesSuccessResultContract() {Data = result};
        }
    }
}