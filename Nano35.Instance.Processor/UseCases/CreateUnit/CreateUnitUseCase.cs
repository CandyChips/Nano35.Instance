using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Cashbox.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateUnit
{
    public class CreateUnitUseCase : UseCaseEndPointNodeBase<ICreateUnitRequestContract, ICreateUnitResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;
        public CreateUnitUseCase(ApplicationContext context, IBus bus) { _context = context; _bus = bus; }
        public override async Task<UseCaseResponse<ICreateUnitResultContract>> Ask(
            ICreateUnitRequestContract input,
            CancellationToken cancellationToken)
        {
            var unit = 
                new Unit()
                    {Id = input.Id,
                     Name = input.Name,
                     WorkingFormat = input.WorkingFormat,
                     Adress = input.Address,
                     Phone = input.Phone,
                     Date = DateTime.Now,
                     Deleted = false,
                     CreatorId = input.CreatorId,
                     InstanceId = input.InstanceId,
                     UnitTypeId = input.UnitTypeId};

            await _context.AddAsync(unit, cancellationToken);
            
            return new UseCaseResponse<ICreateUnitResultContract>(new CreateUnitResultContract());
        }
    }
}