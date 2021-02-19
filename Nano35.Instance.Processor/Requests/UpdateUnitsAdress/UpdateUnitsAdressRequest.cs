using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateUnitsAdress
{
    public class UpdateUnitsAdressRequest :
        IPipelineNode<
            IUpdateUnitsAdressRequestContract,
            IUpdateUnitsAdressResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateUnitsAdressRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateUnitsAdressSuccessResultContract : 
            IUpdateUnitsAdressSuccessResultContract
        {
            
        }

        public async Task<IUpdateUnitsAdressResultContract> Ask(
            IUpdateUnitsAdressRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await ( _context.Units
                    .FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken)
                );

            result.Adress = input.Adress;
            return new UpdateUnitsAdressSuccessResultContract() ;
        }
    }
}