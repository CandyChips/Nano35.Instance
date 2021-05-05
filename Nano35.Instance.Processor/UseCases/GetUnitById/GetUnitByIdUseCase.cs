using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetUnitById
{
    public class GetUnitByIdUseCase : UseCaseEndPointNodeBase<IGetUnitByIdRequestContract, IGetUnitByIdResultContract>
    {
        private readonly ApplicationContext _context;
        public GetUnitByIdUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetUnitByIdResultContract>> Ask(
            IGetUnitByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Units
                .FirstOrDefaultAsync(f => f.Id == input.UnitId, cancellationToken);
            return result == null ? 
                new UseCaseResponse<IGetUnitByIdResultContract>("Подразделение не найдено.") :
                new UseCaseResponse<IGetUnitByIdResultContract>(
                    new GetUnitByIdResultContract()  
                    {
                        Data =
                            new UnitViewModel()
                                {Id = result.Id,
                                 Address = result.Adress,
                                 Name = result.Name, 
                                 Phone = result.Phone, 
                                 UnitType = result.UnitType.Name,
                                 WorkingFormat = result.WorkingFormat}
                    });
        }
    }
}