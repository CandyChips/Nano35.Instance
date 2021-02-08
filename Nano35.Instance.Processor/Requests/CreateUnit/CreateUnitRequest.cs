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
    public class CreateUnitRequest :
        IPipelineNode<ICreateUnitRequestContract, ICreateUnitResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateUnitRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateUnitSuccessResultContract : 
            ICreateUnitSuccessResultContract
        {
            
        }
        
        public async Task<ICreateUnitResultContract> Ask(
            ICreateUnitRequestContract input)
        {
            var unit = new Unit(){
                Id = input.Id,
                Name = input.Name,
                WorkingFormat = input.WorkingFormat,
                Adress = input.Adress,
                Phone = input.Phone,
                Date = DateTime.Now,
                Deleted = false,
                CreatorId = input.CreatorId,
                InstanceId = input.InstanceId,
                UnitTypeId = input.UnitTypeId
            };
            await _context.AddAsync(unit);
            return new CreateUnitSuccessResultContract();
        }
    }
}