﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.GetWorkerById
{
    public class GetWorkerByIdRequest :
        IPipelineNode<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetWorkerByIdRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetWorkerByIdSuccessResultContract : 
            IGetWorkerByIdSuccessResultContract
        {
            public IWorkerViewModel Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetAllClientStatesErrorResultContract
        {
            public string Message { get; set; }
        }

        public async Task<IGetWorkerByIdResultContract> Ask(
            IGetWorkerByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Workers
                    .FirstOrDefaultAsync(f => f.Id == input.WorkerId, cancellationToken: cancellationToken))
                .MapTo<IWorkerViewModel>();
            return new GetWorkerByIdSuccessResultContract() {Data = result};
        }
    }
}