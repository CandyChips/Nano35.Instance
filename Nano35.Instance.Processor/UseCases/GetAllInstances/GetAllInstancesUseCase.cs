using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllInstances
{
    public class GetAllInstancesUseCase : EndPointNodeBase<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>
    {
        private readonly ApplicationContext _context;
        public GetAllInstancesUseCase(ApplicationContext context) => _context = context;
        public override async Task<IGetAllInstancesResultContract> Ask(IGetAllInstancesRequestContract input, CancellationToken cancellationToken) =>
            new GetAllInstancesSuccessResultContract()
            {
                Data = 
                    await _context.Workers
                        .Where(c => c.Id == input.UserId)
                        .Select(e => e.Instance)
                        .Select(a => 
                            new InstanceViewModel()
                                {Id = a.Id,
                                 CompanyInfo = a.CompanyInfo,
                                 OrgEmail = a.OrgEmail,
                                 OrgName = a.OrgName,
                                 OrgRealName = a.OrgRealName,
                                 RegionId = a.RegionId})
                        .ToListAsync(cancellationToken: cancellationToken)
            };
    }
}