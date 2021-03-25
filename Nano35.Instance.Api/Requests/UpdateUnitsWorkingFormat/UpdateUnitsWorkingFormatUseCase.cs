﻿using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateUnitsWorkingFormat
{
    public class UpdateUnitsWorkingFormatUseCase :
        EndPointNodeBase<
            IUpdateUnitsWorkingFormatRequestContract,
            IUpdateUnitsWorkingFormatResultContract>
    {
        private readonly IBus _bus;

        private readonly ICustomAuthStateProvider _auth;

        /// <summary>
        /// The request is accepted by the bus processing the request
        /// </summary>
        public UpdateUnitsWorkingFormatUseCase(
            IBus bus, 
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        
        /// <summary>
        /// Request sends to message bus when processor make magic with input
        /// 1. Generate client from context of request
        /// 2. Sends a request
        /// 3. Check and returns response
        /// 4? Throw exception if overtime
        /// </summary>
        public override async Task<IUpdateUnitsWorkingFormatResultContract> Ask(
            IUpdateUnitsWorkingFormatRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            return (await (new UpdateUnitsWorkingFormatRequest(_bus, input)).GetResponse());

        }
    }
}