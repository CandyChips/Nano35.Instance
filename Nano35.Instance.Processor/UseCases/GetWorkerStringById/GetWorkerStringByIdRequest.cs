using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerStringById
{
    public class GetWorkerStringByIdRequest :
        EndPointNodeBase<IGetWorkerStringByIdRequestContract, IGetWorkerStringByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetWorkerStringByIdRequest(ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetWorkerStringByIdSuccessResultContract : 
            IGetWorkerStringByIdSuccessResultContract
        {
            public string Data { get; set; }
        }

        public override async Task<IGetWorkerStringByIdResultContract> Ask(
            IGetWorkerStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Workers.FirstOrDefaultAsync(e => e.Id == input.WorkerId, cancellationToken: cancellationToken)).ToString();
            return new GetWorkerStringByIdSuccessResultContract() {Data = result};
        }
    }
}