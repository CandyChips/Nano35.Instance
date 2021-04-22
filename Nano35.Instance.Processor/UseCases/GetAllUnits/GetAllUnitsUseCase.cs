using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllUnits
{
    public class GetAllUnitsUseCase :
        EndPointNodeBase<
            IGetAllUnitsRequestContract, 
            IGetAllUnitsResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllUnitsUseCase(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<IGetAllUnitsResultContract> Ask(
            IGetAllUnitsRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.Units
                .Where(c => c.InstanceId == input.InstanceId && c.Deleted == false)
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
            return new GetAllUnitsSuccessResultContract() {Data = result};
        }
    }
}