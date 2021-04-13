using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetInstanceById
{
    public class GetInstanceByIdUseCase :
        EndPointNodeBase<
            IGetInstanceByIdRequestContract,
            IGetInstanceByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetInstanceByIdUseCase(ApplicationContext context)
        {
            _context = context;
        }

        public override async Task<IGetInstanceByIdResultContract> Ask(
            IGetInstanceByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.Instances
                .FirstOrDefaultAsync(f => f.Id == input.InstanceId, cancellationToken: cancellationToken);
            return new GetInstanceByIdSuccessResultContract()
            {
                Data = new InstanceViewModel()
                {
                    Id = result.Id,
                    CompanyInfo = result.CompanyInfo,
                    OrgEmail = result.OrgEmail,
                    OrgName = result.OrgName,
                    RegionId = result.RegionId,
                    OrgRealName = result.OrgRealName
                }
            };
        }
    }
}