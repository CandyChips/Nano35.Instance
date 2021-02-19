﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetUnitById
{
    public class LoggedGetUnitByIdRequest :
        IPipelineNode<IGetUnitByIdRequestContract, IGetUnitByIdResultContract>
    {
        private readonly ILogger<LoggedGetUnitByIdRequest> _logger;
        private readonly IPipelineNode<IGetUnitByIdRequestContract, IGetUnitByIdResultContract> _nextNode;

        public LoggedGetUnitByIdRequest(
            ILogger<LoggedGetUnitByIdRequest> logger,
            IPipelineNode<IGetUnitByIdRequestContract, IGetUnitByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetUnitByIdResultContract> Ask(
            IGetUnitByIdRequestContract input)
        {
            _logger.LogInformation($"GetUnitByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetUnitByIdLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetUnitByIdSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetUnitByIdErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}