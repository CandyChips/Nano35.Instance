﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersName
{
    public class LoggedUpdateWorkersNameRequest :
        PipeNodeBase<
            IUpdateWorkersNameRequestContract,
            IUpdateWorkersNameResultContract>
    {
        private readonly ILogger<LoggedUpdateWorkersNameRequest> _logger;

        public LoggedUpdateWorkersNameRequest(
            ILogger<LoggedUpdateWorkersNameRequest> logger,
            IPipeNode<IUpdateWorkersNameRequestContract,
                IUpdateWorkersNameResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateWorkersNameResultContract> Ask(
            IUpdateWorkersNameRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateWorkersName starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateWorkersName ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateWorkersNameSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateWorkersNameErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}