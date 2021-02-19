using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateUnitsType
{
    public class UpdateUnitsTypeRequest :
        IPipelineNode<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateUnitsTypeRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateUnitsTypeSuccessResultContract : 
            IUpdateUnitsTypeSuccessResultContract
        {
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetAllClientStatesErrorResultContract
        {
            public string Message { get; set; }
        }

        public async Task<IUpdateUnitsTypeResultContract> Ask(
            IUpdateUnitsTypeRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await ( _context.Units
                    .FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken)
                );

            result.UnitTypeId = input.Type;
            return new UpdateUnitsTypeSuccessResultContract() ;
        }
    }
}