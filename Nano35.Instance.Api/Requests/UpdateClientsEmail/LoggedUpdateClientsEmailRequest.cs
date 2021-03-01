﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsEmail
{
    public class LoggedUpdateClientsEmailRequest :
        IPipelineNode<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract>
    {
        private readonly ILogger<LoggedUpdateClientsEmailRequest> _logger;
        private readonly IPipelineNode<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract> _nextNode;

        public LoggedUpdateClientsEmailRequest(
            ILogger<LoggedUpdateClientsEmailRequest> logger,
            IPipelineNode<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateClientsEmailResultContract> Ask(
            IUpdateClientsEmailRequestContract input)
        {
            _logger.LogInformation($"UpdateClientsEmailLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateClientsEmailLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateClientsEmailSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateClientsEmailErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}