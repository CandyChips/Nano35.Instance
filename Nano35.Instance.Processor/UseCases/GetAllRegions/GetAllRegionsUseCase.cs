using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllRegions
{
    public class GetAllRegionsUseCase : UseCaseEndPointNodeBase<IGetAllRegionsRequestContract, IGetAllRegionsResultContract>
    {
        private readonly ApplicationContext _context;
        public GetAllRegionsUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetAllRegionsResultContract>> Ask(
            IGetAllRegionsRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.Regions
                .Select(a => new RegionViewModel() {Id = a.Id, Name = a.Name})
                .ToListAsync(cancellationToken);
            return new UseCaseResponse<IGetAllRegionsResultContract>(new GetAllRegionsResultContract() {Regions = result});
        }
    }
}