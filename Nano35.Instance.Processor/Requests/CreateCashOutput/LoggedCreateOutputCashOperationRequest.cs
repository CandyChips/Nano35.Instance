﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.CreateCashOutput
{
    public class LoggedCreateOutputCashOperationRequest :
        IPipelineNode<
            ICreateCashOutputRequestContract,
            ICreateCashOutputResultContract>
    {
        private readonly ILogger<LoggedCreateOutputCashOperationRequest> _logger;
        private readonly IPipelineNode<
            ICreateCashOutputRequestContract,
            ICreateCashOutputResultContract> _nextNode;

        public LoggedCreateOutputCashOperationRequest(
            ILogger<LoggedCreateOutputCashOperationRequest> logger,
            IPipelineNode<
                ICreateCashOutputRequestContract,
                ICreateCashOutputResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateCashOutputResultContract> Ask(
            ICreateCashOutputRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateCashOutputLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"CreateCashOutputLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}