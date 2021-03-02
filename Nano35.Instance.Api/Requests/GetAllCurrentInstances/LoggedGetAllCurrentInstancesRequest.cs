﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllCurrentInstances
{
    public class LoggedGetAllCurrentInstancesRequest :
        IPipelineNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>
    {
        private readonly ILogger<LoggedGetAllCurrentInstancesRequest> _logger;
        private readonly IPipelineNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract> _nextNode;

        public LoggedGetAllCurrentInstancesRequest(
            ILogger<LoggedGetAllCurrentInstancesRequest> logger,
            IPipelineNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllInstancesResultContract> Ask(
            IGetAllInstancesRequestContract input)
        {
            _logger.LogInformation($"GetAllInstancesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllInstancesLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllInstancesSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllInstancesErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}