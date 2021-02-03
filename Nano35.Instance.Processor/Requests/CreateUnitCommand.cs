using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests.Behaviours;
using Nano35.Instance.Processor.Services.Contexts;
using Unit = Nano35.Instance.Processor.Models.Unit;

namespace Nano35.Instance.Processor.Requests
{
    public class CreateUnitCommand :
        ICreateUnitRequestContract, 
        ICommandRequest<ICreateUnitResultContract>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string WorkingFormat { get; set; }
        public string Phone { get; set; }
        public Guid UnitTypeId { get; set; }
        public Guid CreatorId { get; set; }
        public Guid InstanceId { get; set; }

        private class CreateUnitSuccessResultContract :
            ICreateUnitSuccessResultContract
        {
            
        }

        private class CreateUnitErrorResultContract :
            ICreateUnitErrorResultContract
        {
            public string Message { get; set; }
        }

        public class CreateUnitHandler : 
            IRequestHandler<CreateUnitCommand, ICreateUnitResultContract>
        {
            private readonly ApplicationContext _context;
            private readonly IBus _bus;
            
            public CreateUnitHandler(
                ApplicationContext context, 
                IBus bus)
            {
                _context = context;
                _bus = bus;
            }
        
            public async Task<ICreateUnitResultContract> Handle(
                CreateUnitCommand message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    var unit = new Unit(){
                        Id = message.Id,
                        Name = message.Name,
                        WorkingFormat = message.WorkingFormat,
                        Adress = message.Adress,
                        Phone = message.Phone,
                        Date = DateTime.Now,
                        Deleted = false,
                        CreatorId = message.CreatorId,
                        InstanceId = message.InstanceId,
                        UnitTypeId = message.UnitTypeId
                    };
                    await _context.AddAsync(unit, cancellationToken);
                    return new CreateUnitSuccessResultContract();
                }
                catch (Exception e)
                {
                    return new CreateUnitErrorResultContract();
                }
            }
        }
    }
    
}