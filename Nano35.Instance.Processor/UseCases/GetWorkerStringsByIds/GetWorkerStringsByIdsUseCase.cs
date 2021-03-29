using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerStringsByIds
{
    public class GetWorkerStringsByIdsUseCase :
        EndPointNodeBase
        <IGetWorkerStringsByIdsRequestContract,
            IGetWorkerStringsByIdsResultContract>
    {
        private readonly ApplicationContext _context;

        public GetWorkerStringsByIdsUseCase(ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetWorkerStringsByIdsSuccessResultContract : 
            IGetWorkerStringsByIdsSuccessResultContract
        {
            public List<string> Data { get; set; }
        }

        public override async Task<IGetWorkerStringsByIdsResultContract> Ask(
            IGetWorkerStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Workers.Where(c => input.WorkerIds.Contains(c.Id))
                .Select(e => $"{e.Name}")
                .ToListAsync(cancellationToken));            
            return new GetWorkerStringsByIdsSuccessResultContract() {Data = result};
        }
    }
}