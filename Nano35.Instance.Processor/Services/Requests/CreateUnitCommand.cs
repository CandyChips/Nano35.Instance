using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.Requests.Behaviours;
using Unit = Nano35.Instance.Processor.Models.Unit;

namespace Nano35.Instance.Processor.Services.Requests
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

        public CreateUnitCommand(ICreateUnitRequestContract request)
        {
            Id = request.Id;
            Name = request.Name;
            Adress = request.Adress;
            WorkingFormat = request.WorkingFormat;
            Phone = request.Phone;
            UnitTypeId = request.UnitTypeId;
            CreatorId = request.CreatorId;
            InstanceId = request.InstanceId;
        }
        
        public class CreateUnitSuccessResultContract : ICreateUnitSuccessResultContract
        {
            
        }
        
        public class CreateUnitErrorResultContract : ICreateUnitErrorResultContract
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
                    //var creator = this._context.Workers.FirstOrDefault(f => f.Id == message.CreatorId && f.InstanceId == message.InstanceId);
                    //var instance = this._context.Instances.FirstOrDefault(f => f.Id == message.InstanceId);
                    //var unitType = this._context.UnitTypes.FirstOrDefault(f => f.Id == message.UnitTypeId);
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
                    await this._context.AddAsync(unit);
                    _context.SaveChanges();
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