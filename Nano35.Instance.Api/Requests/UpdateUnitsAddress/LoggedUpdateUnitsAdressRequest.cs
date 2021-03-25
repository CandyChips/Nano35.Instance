﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsAddress
{
    public class LoggedUpdateUnitsAddressRequest :
        PipeNodeBase
        <IUpdateUnitsAddressRequestContract,
            IUpdateUnitsAddressResultContract>
    {
        private readonly ILogger<LoggedUpdateUnitsAddressRequest> _logger;

        public LoggedUpdateUnitsAddressRequest(
            ILogger<LoggedUpdateUnitsAddressRequest> logger,
            IPipeNode<IUpdateUnitsAddressRequestContract,
                IUpdateUnitsAddressResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateUnitsAddressResultContract> Ask(
            IUpdateUnitsAddressRequestContract input)
        {
            _logger.LogInformation($"UpdateUnitsAddressLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"UpdateUnitsAddressLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateUnitsAddressSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateUnitsAddressErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}