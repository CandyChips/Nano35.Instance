using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Cashbox.Artifacts;
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
        private readonly IBus _bus;

        public GetAllUnitsUseCase(ApplicationContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        public override async Task<IGetAllUnitsResultContract> Ask(
            IGetAllUnitsRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.Units
                .Where(c => c.InstanceId == input.InstanceId && c.Deleted == false)
                .Select(a =>
                    new UnitViewModel()
                        {Id = a.Id,
                         Address = a.Adress,
                         Name = a.Name,
                         Phone = a.Phone,
                         UnitType = a.UnitType.Name,
                         WorkingFormat = a.WorkingFormat})
                .ToListAsync(cancellationToken);
            result.ForEach(async e =>
            {
                var response = await new GetCashboxByUnitId(_bus, new GetCashboxByUnitIdRequestContract() {UnitId = e.Id}).GetResponse();
                if (response is IGetCashboxByUnitIdSuccessResultContract s)
                {
                    e.Cashbox = s.Cash;
                }
                else
                {
                    throw new Exception();
                }
            });
            
            return new GetAllUnitsSuccessResultContract() {Data = result};
        }
    }
    
    public class GetCashboxByUnitId : 
        MasstransitRequest
        <IGetCashboxByUnitIdRequestContract, 
            IGetCashboxByUnitIdResultContract,
            IGetCashboxByUnitIdSuccessResultContract, 
            IGetCashboxByUnitIdErrorResultContract>
    {
        public GetCashboxByUnitId(IBus bus, IGetCashboxByUnitIdRequestContract request) : base(bus, request) {}
    }
}