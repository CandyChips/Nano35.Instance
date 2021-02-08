using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Requests;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Requests.CreateClient
{
    public class CreateInstanceRequest :
        IPipelineNode<ICreateInstanceRequestContract, ICreateInstanceResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateInstanceRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateInstanceSuccessResultContract : 
            ICreateInstanceSuccessResultContract
        {
            
        }
        
        public async Task<ICreateInstanceResultContract> Ask(
            ICreateInstanceRequestContract input)
        {
            var instanceType = this._context.InstanceTypes.Find(input.TypeId);
            var region = this._context.Regions.Find(input.RegionId);
            var role = this._context.WorkerRoles.FirstOrDefault();
            var instance = new Models.Instance(){
                Id = input.NewId,
                OrgEmail = input.Email,
                OrgName = input.Name,
                OrgRealName = input.RealName,
                CompanyInfo = input.Info,
                InstanceType = instanceType,
                InstanceTypeId = instanceType.Id,
                Region = region,
                RegionId = region.Id
            };
            await _context.AddAsync(instance);
            var defaultUser = new Worker(){
                Id = input.UserId,
                Instance = instance,
                WorkersRole = role,
                Name = "Администратор системы",
                Comment = ""
            };
            await _context.AddAsync(defaultUser);
            return new CreateInstanceSuccessResultContract();
        }
    }
}