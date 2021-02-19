using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Requests.CreateUnit
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
            ICreateUnitRequestContract input,
            CancellationToken cancellationToken)
        {
            var unit = new Unit(){
                Id = input.Id,
                Name = input.Name,
                WorkingFormat = input.WorkingFormat,
                Adress = input.Address,
                Phone = input.Phone,
                Date = DateTime.Now,
                Deleted = false,
                CreatorId = input.CreatorId,
                InstanceId = input.InstanceId,
                UnitTypeId = input.UnitTypeId
            };
            await _context.AddAsync(unit, cancellationToken);
            return new CreateUnitSuccessResultContract();
        }
    }
}