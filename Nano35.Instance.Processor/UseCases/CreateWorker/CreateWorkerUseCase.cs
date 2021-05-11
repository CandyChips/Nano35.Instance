﻿using System;
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
    public class CreateWorkerUseCase : UseCaseEndPointNodeBase<ICreateWorkerRequestContract, ICreateWorkerResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;
        public CreateWorkerUseCase(IBus bus, ApplicationContext context) { _bus = bus; _context = context; }
        public override async Task<UseCaseResponse<ICreateWorkerResultContract>> Ask(
            ICreateWorkerRequestContract input,
            CancellationToken cancellationToken)
        {
            if (!_context.Instances.Any(e => e.Id == input.InstanceId))
                return Pass("Повторите попытку позже.");
            if (!_context.WorkerRoles.Any(e => e.Id == input.RoleId))
                return Pass("Неверная роль сотрудника.");
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
                     Comment = input.Comment,
                     Name = input.Name}, 
                    cancellationToken);

            if (!response.Message.IsSuccess()) return new UseCaseResponse<ICreateWorkerResultContract>(response.Message.Error);

            var worker = 
                new Worker()
                    {Id = input.NewId,
                     InstanceId = input.InstanceId,
                     Name = input.Name,
                     Comment = input.Comment};
            
            await _context.AddAsync(worker, cancellationToken);

            var setsRole = new WorkersRole()
            {
                Id = Guid.NewGuid(),
                RoleId = input.RoleId,
                WorkerId = input.NewId
            };
            
            await _context.AddAsync(setsRole, cancellationToken);
            
            return new UseCaseResponse<ICreateWorkerResultContract>(new CreateWorkerResultContract());
        }
    }
}