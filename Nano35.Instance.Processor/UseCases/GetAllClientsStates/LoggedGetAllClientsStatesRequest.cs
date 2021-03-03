﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllClientsStates
{
    public class LoggedGetAllClientStatesRequest :
        IPipelineNode<
            IGetAllClientStatesRequestContract, 
            IGetAllClientStatesResultContract>
    {
        private readonly ILogger<LoggedGetAllClientStatesRequest> _logger;
        private readonly IPipelineNode<
            IGetAllClientStatesRequestContract, 
            IGetAllClientStatesResultContract> _nextNode;

        public LoggedGetAllClientStatesRequest(
            ILogger<LoggedGetAllClientStatesRequest> logger,
            IPipelineNode<
                IGetAllClientStatesRequestContract, 
                IGetAllClientStatesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllClientStatesResultContract> Ask(
            IGetAllClientStatesRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllClientStatesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllClientStatesLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllClientStatesSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllClientStatesErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}