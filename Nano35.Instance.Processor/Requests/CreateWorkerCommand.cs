using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Requests.Behaviours;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Requests
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

        private class CreateWorkerSuccessResultContract : 
            ICreateWorkerSuccessResultContract
        {
            
        }

        private class CreateWorkerErrorResultContract : 
            ICreateWorkerErrorResultContract
        {
            public string Message { get; set; }
        }

        public class CreateWorkerHandler : 
            IRequestHandler<CreateWorkerCommand, ICreateWorkerResultContract>
        {
            private readonly ApplicationContext _context;
            private readonly IBus _bus;
            
            public CreateWorkerHandler(
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

                if (response.Is(out Response<IRegisterErrorResultContract> errorResponse))
                {
                    return new CreateWorkerErrorResultContract();
                }
                var worker = new Worker(){
                    Id = message.NewId,
                    InstanceId = message.InstanceId,
                    WorkersRoleId = message.RoleId,
                    Name = message.Name,
                    Comment = message.Comment
                };
                await _context.AddAsync(worker, cancellationToken);
                return new CreateWorkerSuccessResultContract();
            }
        }
    }
    
}