using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateUnitsWorkingFormat
{
    public class UpdateUnitsWorkingFormatRequest :
        IPipelineNode<
            IUpdateUnitsWorkingFormatRequestContract,
            IUpdateUnitsWorkingFormatResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateUnitsWorkingFormatRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateUnitsWorkingFormatSuccessResultContract : 
            IUpdateUnitsWorkingFormatSuccessResultContract
        {
            
        }

        public async Task<IUpdateUnitsWorkingFormatResultContract> Ask(
            IUpdateUnitsWorkingFormatRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await ( _context.Units
                    .FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken)
                );

            result.WorkingFormat = input.WorkingFormat;
            return new UpdateUnitsWorkingFormatSuccessResultContract() ;
        }
    }
}