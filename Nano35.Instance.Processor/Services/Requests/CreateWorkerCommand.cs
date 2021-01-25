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

namespace Nano35.Instance.Processor.Services.Requests
{
    public class CreateWorkerCommand :
        ICreateWorkerRequestContract, 
        ICommandRequest<ICreateWorkerResultContract>
    {
        public Guid NewId { get; set; }
        public Guid InstanceId { get; set; }
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        
        public CreateWorkerCommand(ICreateWorkerRequestContract request)
        {
            NewId = request.NewId;
            InstanceId = request.InstanceId;
            RoleId = request.RoleId;
            Name = request.Name;
            Comment = request.Comment;
            Phone = request.Phone;
            Email = request.Email;
            Password = request.Password;
            PasswordConfirm = request.PasswordConfirm;
        }

        public class CreateWorkerCommandValidator : 
            AbstractValidator<CreateWorkerCommand>
        {
            public CreateWorkerCommandValidator()
            {
            }
        }
        
        public class CreateWorkerSuccessResultContract : ICreateWorkerSuccessResultContract
        {
            public Guid Id { get; set; }
        }
        public class CreateWorkerErrorResultContract : ICreateWorkerErrorResultContract
        {
            
        }

        public class CreateInstanceHandler : 
            IRequestHandler<CreateWorkerCommand, ICreateWorkerResultContract>
        {
            private readonly ApplicationContext _context;
            private readonly IBus _bus;
            
            public CreateInstanceHandler(
                ApplicationContext context, 
                IBus bus)
            {
                _context = context;
                _bus = bus;
            }
        
            public async Task<ICreateWorkerResultContract> Handle(
                CreateWorkerCommand message, 
                CancellationToken cancellationToken)
            {
                var instance = this._context.Instances.FirstOrDefault(f => f.Id == message.InstanceId);
                var role = this._context.WorkerRoles.FirstOrDefault(f => f.Id == message.RoleId);
                var client = _bus.CreateRequestClient<IRegisterRequestContract>(TimeSpan.FromSeconds(1000));
                var response = await client
                    .GetResponse<IRegisterSuccessResultContract, IRegisterErrorResultContract>(new
                    {
                        NewUserId = message.NewId,
                        Phone = message.Phone,
                        Email = message.Email,
                        Password = message.Password,
                        PasswordConfirm = message.PasswordConfirm
                    }, cancellationToken);

                if (response.Is(out Response<IRegisterSuccessResultContract> successResponse))
                {
                    var worker = new Worker(){
                        Id = message.NewId,
                        InstanceId = instance.Id,
                        WorkersRole = role,
                        Name = message.Name,
                        Comment = message.Comment
                    };
                    await this._context.AddAsync(worker);
                    _context.SaveChanges();
                    return new CreateWorkerSuccessResultContract() {Id = new Guid()};
                }
                return new CreateWorkerErrorResultContract() {};
            }
        }
    }
    
}