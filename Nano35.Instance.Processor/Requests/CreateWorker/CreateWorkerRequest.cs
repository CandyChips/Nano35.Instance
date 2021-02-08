using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Api.Requests.CreateWorker
{
    public class CreateWorkerRequest :
        IPipelineNode<ICreateWorkerRequestContract, ICreateWorkerResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public CreateWorkerRequest(
            IBus bus, 
            ApplicationContext context)
        {
            _bus = bus;
            _context = context;
        }
        
        private class CreateWorkerSuccessResultContract : 
            ICreateWorkerSuccessResultContract
        {
            
        }

        private class CreateWorkerErrorResultContract : 
            ICreateWorkerErrorResultContract
        {
            public string Message { get; set; }
        }
        
        public async Task<ICreateWorkerResultContract> Ask(
            ICreateWorkerRequestContract input)
        {
            var client = _bus.CreateRequestClient<IRegisterRequestContract>(TimeSpan.FromSeconds(1000));
            var response = await client
                .GetResponse<IRegisterSuccessResultContract, IRegisterErrorResultContract>(new
                {
                    NewUserId = input.NewId,
                    Phone = input.Phone,
                    Email = input.Email,
                    Password = input.Password,
                    PasswordConfirm = input.PasswordConfirm
                });

            if (response.Is(out Response<IRegisterErrorResultContract> errorResponse))
            {
                return new CreateWorkerErrorResultContract();
            }
            var worker = new Worker(){
                Id = input.NewId,
                InstanceId = input.InstanceId,
                WorkersRoleId = input.RoleId,
                Name = input.Name,
                Comment = input.Comment
            };
            await _context.AddAsync(worker);
            return new CreateWorkerSuccessResultContract();
        }
    }
}