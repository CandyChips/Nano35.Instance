﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetInstanceStringsByIds
{
    public class GetInstanceStringsByIds : EndPointNodeBase<IGetInstanceStringsByIdsRequestContract, IGetInstanceStringsByIdsResultContract>
    {
        private readonly ApplicationContext _context;
        public GetInstanceStringsByIds(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetInstanceStringsByIdsResultContract>> Ask(
            IGetInstanceStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context
                .Instances
                .Where(c => input.InstanceIds.Contains(c.Id))
                .Select(e => $"{e.OrgRealName}")
                .ToListAsync(cancellationToken));
            
            return new UseCaseResponse<IGetInstanceStringsByIdsResultContract>(new GetInstanceStringsByIdsResultContract() {Data = result});
        }
    }
}