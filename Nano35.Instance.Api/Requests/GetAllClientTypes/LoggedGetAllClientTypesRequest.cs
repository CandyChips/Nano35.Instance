﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllClientTypes
{
    public class LoggedGetAllClientTypesRequest :
        IPipelineNode<IGetAllClientTypesRequestContract, IGetAllClientTypesResultContract>
    {
        private readonly ILogger<LoggedGetAllClientTypesRequest> _logger;
        private readonly IPipelineNode<IGetAllClientTypesRequestContract, IGetAllClientTypesResultContract> _nextNode;

        public LoggedGetAllClientTypesRequest(
            ILogger<LoggedGetAllClientTypesRequest> logger,
            IPipelineNode<IGetAllClientTypesRequestContract, IGetAllClientTypesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllClientTypesResultContract> Ask(
            IGetAllClientTypesRequestContract input)
        {
            _logger.LogInformation($"GetAllClientTypesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllClientTypesLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}