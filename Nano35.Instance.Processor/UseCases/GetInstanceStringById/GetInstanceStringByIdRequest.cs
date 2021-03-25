﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetInstanceStringById
{
    public class GetInstanceStringByIdRequest :
        EndPointNodeBase<IGetInstanceStringByIdRequestContract, IGetInstanceStringByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetInstanceStringByIdRequest(ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetInstanceStringByIdSuccessResultContract : 
            IGetInstanceStringByIdSuccessResultContract
        {
            public string Data { get; set; }
        }

        public override async Task<IGetInstanceStringByIdResultContract> Ask(
            IGetInstanceStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Instances.FirstOrDefaultAsync(e => e.Id == input.InstanceId, cancellationToken: cancellationToken)).ToString();
            return new GetInstanceStringByIdSuccessResultContract() {Data = result};
        }
    }
}