using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Requests.Behaviours;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Requests
{
    public class CreateInstanceCommand :
        ICreateInstanceRequestContract, 
        ICommandRequest<ICreateInstanceResultContract>
    {
        public Guid NewId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public string Info { get; set; }
        public string Phone { get; set; }
        public Guid TypeId { get; set; }
        public Guid RegionId { get; set; }

        private class CreateInstanceSuccessResultContract : 
            ICreateInstanceSuccessResultContract
        {
            
        }

        private class CreateInstanceErrorResultContract : 
            ICreateInstanceErrorResultContract
        {
            public string Message { get; set; }
        }

        public class CreateInstanceHandler : 
            IRequestHandler<CreateInstanceCommand, ICreateInstanceResultContract>
        {
            private readonly ApplicationContext _context;
            
            public CreateInstanceHandler(
                ApplicationContext context)
            {
                _context = context;
            }
        
            public async Task<ICreateInstanceResultContract> Handle(
                CreateInstanceCommand message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    var instanceType = this._context.InstanceTypes.Find(message.TypeId);
                    var region = this._context.Regions.Find(message.RegionId);
                    var role = this._context.WorkerRoles.FirstOrDefault();
                    var instance = new Models.Instance(){
                        Id = message.NewId,
                        OrgEmail = message.Email,
                        OrgName = message.Name,
                        OrgRealName = message.RealName,
                        CompanyInfo = message.Info,
                        InstanceType = instanceType,
                        InstanceTypeId = instanceType.Id,
                        Region = region,
                        RegionId = region.Id
                    };
                    await this._context.AddAsync(instance);
                    var defaultUser = new Worker(){
                        Id = message.UserId,
                        Instance = instance,
                        WorkersRole = role,
                        Name = "Администратор системы",
                        Comment = ""
                    };
                    await this._context.AddAsync(defaultUser);
                    return new CreateInstanceSuccessResultContract();
                }
                catch (Exception e)
                {
                    return new CreateInstanceErrorResultContract();
                }
            }
        }
    }
    
}