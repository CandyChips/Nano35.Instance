﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Requests.Behaviours;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests
{
    public class GetAllClientStatesQuery : 
        IGetAllClientStatesRequestContract,
        IQueryRequest<IGetAllClientStatesResultContract>
    {
        private class GetAllClientStatesResultContract : 
            IGetAllClientStatesSuccessResultContract
        {
            public IEnumerable<IClientStateViewModel> Data { get; set; }
        }

        public class GetAllClientStatesHandler 
            : IRequestHandler<GetAllClientStatesQuery, IGetAllClientStatesResultContract>
        {
            private readonly ApplicationContext _context;
            public GetAllClientStatesHandler(
                ApplicationContext context)
            {
                _context = context;
            }
            
            public async Task<IGetAllClientStatesResultContract> Handle(
                GetAllClientStatesQuery message,
                CancellationToken cancellationToken)
            {
                var result = await (_context.ClientStates
                    .MapAllToAsync<IClientStateViewModel>());
                return new GetAllClientStatesResultContract() {Data = result};
            }
        }
    }
}