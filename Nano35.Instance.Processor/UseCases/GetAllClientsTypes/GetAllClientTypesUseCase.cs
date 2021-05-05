using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllClientsTypes
{
    public class GetAllClientTypesUseCase : UseCaseEndPointNodeBase<IGetAllClientTypesRequestContract, IGetAllClientTypesResultContract>
    {
        private readonly ApplicationContext _context;
        public GetAllClientTypesUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetAllClientTypesResultContract>> Ask(
            IGetAllClientTypesRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context
                .ClientTypes
                .Select(a => 
                    new ClientTypeViewModel()
                        {Id = a.Id,
                         Name = a.Name})
                .ToListAsync(cancellationToken);
            return new UseCaseResponse<IGetAllClientTypesResultContract>(new GetAllClientTypesResultContract() {Data = result});
        }
    }
}