﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateInstanceEmail
{
    public class UpdateInstanceEmailRequest :
        IPipelineNode<
            IUpdateInstanceEmailRequestContract, 
            IUpdateInstanceEmailResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateInstanceEmailRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateInstanceEmailSuccessResultContract : 
            IUpdateInstanceEmailSuccessResultContract
        {
            
        }

        public async Task<IUpdateInstanceEmailResultContract> Ask(
            IUpdateInstanceEmailRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await ( _context.Instances
                    .FirstOrDefaultAsync(a => a.Id == input.InstanceId, cancellationToken)
                );

            result.OrgEmail = input.Email;
            return new UpdateInstanceEmailSuccessResultContract();
        }
    }
}