using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllUnitsByType
{
    public class GetAllUnitsByTypeUseCase :
        EndPointNodeBase<
            IGetAllUnitsByTypeRequestContract, 
            IGetAllUnitsByTypeResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllUnitsByTypeUseCase(ApplicationContext context) { _context = context; }

        public override async Task<IGetAllUnitsByTypeResultContract> Ask(
            IGetAllUnitsByTypeRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Units)
                .Where(c => c.UnitTypeId == input.UnitTypeId)
                .Select(a => 
                    new UnitViewModel()
                    {
                        Id = a.Id, 
                        Address = a.Adress,
                        Name = a.Name,
                        Phone = a.Phone,
                        UnitType = a.UnitType.Name,
                        WorkingFormat = a.WorkingFormat
                    })
                .ToListAsync(cancellationToken: cancellationToken);
            return new GetAllUnitsByTypeSuccessResultContract() {Data = result};
        }
    }
}