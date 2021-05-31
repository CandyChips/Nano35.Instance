using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Cashbox.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateUnit
{
    public class CreateUnit : EndPointNodeBase<ICreateUnitRequestContract, ICreateUnitResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;
        public CreateUnit(ApplicationContext context, IBus bus) { _context = context; _bus = bus; }
        public override async Task<UseCaseResponse<ICreateUnitResultContract>> Ask(
            ICreateUnitRequestContract input,
            CancellationToken cancellationToken)
        {
            if (!_context.Instances.Any(e => e.Id == input.InstanceId))
                return Pass("Повторите попытку позже.");
            if (!_context.UnitTypes.Any(e => e.Id == input.UnitTypeId))
                return Pass("Неверный тип подразделения.");
            if (_context.Units.Any(e => e.Id == input.Id))
                return Pass("Повторите попытку позже.");
            var unit = 
                new Unit()
                    {Id = input.Id,
                     Name = input.Name,
                     WorkingFormat = input.WorkingFormat,
                     Adress = input.Address,
                     Phone = input.Phone,
                     Date = DateTime.Now,
                     Deleted = false,
                     InstanceId = input.InstanceId,
                     UnitTypeId = input.UnitTypeId};

            await _context.AddAsync(unit, cancellationToken);
            
            return Pass(new CreateUnitResultContract());
        }
    }
}