using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllInstances
{
    public class GetAllInstances : EndPointNodeBase<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>
    {
        private readonly ApplicationContext _context;
        public GetAllInstances(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetAllInstancesResultContract>> Ask(
            IGetAllInstancesRequestContract input, 
            CancellationToken cancellationToken) =>
            new(
                new GetAllInstancesResultContract()
                {Data = await _context
                    .Workers
                    .Where(c => c.Id == input.UserId) 
                    .Select(a => 
                        new InstanceViewModel()
                            {Id = a.Instance.Id,
                             CompanyInfo = a.Instance.CompanyInfo,
                             OrgEmail = a.Instance.OrgEmail,
                             OrgName = a.Instance.OrgName,
                             OrgRealName = a.Instance.OrgRealName,
                             RegionId = a.Instance.RegionId})
                    .ToListAsync(cancellationToken)});
    }
}