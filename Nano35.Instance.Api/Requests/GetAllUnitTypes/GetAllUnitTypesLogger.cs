﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllUnitTypes
{
    public class GetAllUnitTypesLogger :
        IPipelineNode<IGetAllUnitTypesRequestContract, IGetAllUnitTypesResultContract>
    {
        private readonly ILogger<GetAllUnitTypesLogger> _logger;
        private readonly IPipelineNode<IGetAllUnitTypesRequestContract, IGetAllUnitTypesResultContract> _nextNode;

        public GetAllUnitTypesLogger(
            ILogger<GetAllUnitTypesLogger> logger,
            IPipelineNode<IGetAllUnitTypesRequestContract, IGetAllUnitTypesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllUnitTypesResultContract> Ask(
            IGetAllUnitTypesRequestContract input)
        {
            _logger.LogInformation($"GetAllUnitTypesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllUnitTypesLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}