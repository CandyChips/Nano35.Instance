﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetAllInstancesQuery : 
        IGetAllInstancesRequestContract,
        IQueryRequest<IGetAllInstancesResultContract>
    {
        public Guid UserId { get; set; }
        public Guid InstanceTypeId { get; set; }
        public Guid RegionId { get; set; }

        public GetAllInstancesQuery(IGetAllInstancesRequestContract request)
        {
            UserId = request.UserId;
            InstanceTypeId = request.InstanceTypeId;
            RegionId = request.RegionId;
        }

        private class GetAllInstancesResultContract : 
            IGetAllInstancesSuccessResultContract
        {
            public IEnumerable<IInstanceViewModel> Data { get; set; }
        }

        public class GetAllInstancesHandler 
            : IRequestHandler<GetAllInstancesQuery, IGetAllInstancesResultContract>
        {
            private readonly ApplicationContext _context;
            public GetAllInstancesHandler(
                ApplicationContext context)
            {
                _context = context;
            }

            public async Task<IGetAllInstancesResultContract> Handle(
                GetAllInstancesQuery message,
                CancellationToken cancellationToken)
            {
                var result = await (_context.Workers
                    .Where(c => c.Id == message.UserId)
                    .Select(a => a.Instance)
                    .MapAllToAsync<IInstanceViewModel>());
                return new GetAllInstancesResultContract() {Data = result};
            }
        }
    }
}