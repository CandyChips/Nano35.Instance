using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateWorker
{
    public class CreateWorker : EndPointNodeBase<ICreateWorkerRequestContract, ICreateWorkerResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;
        public CreateWorker(IBus bus, ApplicationContext context) { _bus = bus; _context = context; }
        public override async Task<UseCaseResponse<ICreateWorkerResultContract>> Ask(
            ICreateWorkerRequestContract input,
            CancellationToken cancellationToken)
        {
            if (!_context.Instances.Any(e => e.Id == input.InstanceId))
                return Pass("Повторите попытку позже.");
            if (_context.ClientProfiles.Any(e => e.Id == input.NewId))
                return Pass("Повторите попытку позже.");
            
            var client = _bus.CreateRequestClient<ICreateUserRequestContract>();
            
            var response = await client.GetResponse<UseCaseResponse<ICreateUserResultContract>>(
                new CreateUserRequestContract()
                    {NewId = input.NewId,
                     Phone = input.Phone,
                     Email = input.Email,
                     Password = input.Password,
                     InstanceId = input.InstanceId,
                     Comment = input.Comment ?? "",
                     Name = input.Name}, 
                    cancellationToken);

            if (!response.Message.IsSuccess()) return Pass(response.Message.Error);

            var worker = 
                new Worker()
                    {Id = input.NewId,
                     InstanceId = input.InstanceId,
                     Name = input.Name,
                     Comment = input.Comment,
                     Deleted = false};
            
            await _context.AddAsync(worker, cancellationToken);

            foreach (var item in input.Roles)
            {
                await _context.AddAsync(
                    new WorkersRole()
                        {Id = Guid.NewGuid(),
                         RoleId = item,
                         WorkerId = input.NewId}, 
                    cancellationToken);
            }
            
            
            return Pass(new CreateWorkerResultContract());
        }
    }
}