﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.CreateCashInput
{
    public class LoggedCreateInputCashOperationRequest :
        IPipelineNode<
            ICreateCashInputRequestContract, 
            ICreateCashInputResultContract>
    {
        private readonly ILogger<LoggedCreateInputCashOperationRequest> _logger;
        private readonly IPipelineNode<
            ICreateCashInputRequestContract, 
            ICreateCashInputResultContract> _nextNode;

        public LoggedCreateInputCashOperationRequest(
            ILogger<LoggedCreateInputCashOperationRequest> logger,
            IPipelineNode<
                ICreateCashInputRequestContract,
                ICreateCashInputResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateCashInputResultContract> Ask(
            ICreateCashInputRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateCashInputLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"CreateCashInputLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}