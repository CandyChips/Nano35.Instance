using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetAllInstanceTypes
{
    public class GetAllInstanceTypesUseCase :
        EndPointNodeBase<
            IGetAllInstanceTypesRequestContract,
            IGetAllInstanceTypesResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllInstanceTypesUseCase(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<IGetAllInstanceTypesResultContract> Ask(
            IGetAllInstanceTypesRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.InstanceTypes
                .Select(a =>
                    new InstanceTypeViewModel()
                    {
                        Id = a.Id,
                        Name = a.Name
                    })
                .ToListAsync(cancellationToken: cancellationToken);
            return new GetAllInstanceTypesSuccessResultContract() {Data = result};
        }
    }
}