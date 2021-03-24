using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetAllClientsTypes
{
    public class GetAllClientTypesRequest :
        EndPointNodeBase<
            IGetAllClientTypesRequestContract,
            IGetAllClientTypesResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllClientTypesRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllClientTypesSuccessResultContract : 
            IGetAllClientTypesSuccessResultContract
        {
            public IEnumerable<IClientTypeViewModel> Data { get; set; }
        }
        
        public override async Task<IGetAllClientTypesResultContract> Ask(
            IGetAllClientTypesRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.ClientTypes
                .MapAllToAsync<IClientTypeViewModel>());
            return new GetAllClientTypesSuccessResultContract() {Data = result};
        }
    }
}