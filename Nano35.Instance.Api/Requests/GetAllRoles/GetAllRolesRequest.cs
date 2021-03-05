﻿using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllRoles
{
    public class GetAllRolesRequest :
        IPipelineNode<
            IGetAllRolesRequestContract, 
            IGetAllRolesResultContract>
    {
        private readonly IBus _bus;

        public GetAllRolesRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllRolesResultContract> Ask(
            IGetAllRolesRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllRolesRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllRolesSuccessResultContract, IGetAllRolesErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllRolesSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllRolesErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}