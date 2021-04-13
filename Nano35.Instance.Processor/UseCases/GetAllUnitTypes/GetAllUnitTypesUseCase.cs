using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetAllUnitTypes
{
    public class GetAllUnitTypesUseCase :
        EndPointNodeBase<
            IGetAllUnitTypesRequestContract,
            IGetAllUnitTypesResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllUnitTypesUseCase(ApplicationContext context) { _context = context; }

        public override async Task<IGetAllUnitTypesResultContract> Ask(
            IGetAllUnitTypesRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.UnitTypes
                .MapAllToAsync<IUnitTypeViewModel>());
            return new GetAllUnitTypesSuccessResultContract() {Data = result};
        }
    }
}