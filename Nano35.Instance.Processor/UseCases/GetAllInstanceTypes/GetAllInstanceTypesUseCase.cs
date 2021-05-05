using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllInstanceTypes
{
    public class GetAllInstanceTypesUseCase : UseCaseEndPointNodeBase<IGetAllInstanceTypesRequestContract, IGetAllInstanceTypesResultContract>
    {
        private readonly ApplicationContext _context;
        public GetAllInstanceTypesUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetAllInstanceTypesResultContract>> Ask(
            IGetAllInstanceTypesRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context
                .InstanceTypes
                .Select(a =>
                    new InstanceTypeViewModel()
                        {Id = a.Id,
                         Name = a.Name})
                .ToListAsync(cancellationToken);
            return new UseCaseResponse<IGetAllInstanceTypesResultContract>(new GetAllInstanceTypesResultContract() {Data = result});
        }
    }
}