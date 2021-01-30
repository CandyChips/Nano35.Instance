﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Requests.Behaviours;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Requests
{
    public class CreateClientCommand :
        ICreateClientRequestContract, 
        ICommandRequest<ICreateClientResultContract>
    {
        public Guid NewId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid SalleId { get; set; }
        public float Salle { get; set; }
        public Guid ClientTypeId { get; set; }
        public Guid ClientStateId { get; set; }
        public Guid UserId { get; set; }
        public Guid InstanceId { get; set; }

        private class CreateClientSuccessResultContract : ICreateClientSuccessResultContract
        {
            
        }

        private class CreateClientErrorResultContract : ICreateClientErrorResultContract
        {
            public string Message { get; set; }
        }

        public class CreateClientHandler : 
            IRequestHandler<CreateClientCommand, ICreateClientResultContract>
        {
            private readonly ApplicationContext _context;
            private readonly IBus _bus;
            
            public CreateClientHandler(
                ApplicationContext context, 
                IBus bus)
            {
                _context = context;
                _bus = bus;
            }
        
            public async Task<ICreateClientResultContract> Handle(
                CreateClientCommand message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    var client = new Client(){
                        Id = message.NewId,
                        InstanceId = message.InstanceId,
                        Name = message.Name,
                        Email = message.Email,
                        Phone = message.Phone,
                        Deleted = false,
                        WorkerId = message.UserId,
                        ClientStateId = message.ClientStateId,
                        ClientTypeId =  message.ClientTypeId
                    };
                    await this._context.AddAsync(client);
                    _context.SaveChanges();
                    return new CreateClientSuccessResultContract();
                }
                catch (Exception e)
                {
                    return new CreateClientErrorResultContract();
                }
            }
        }
    }
}