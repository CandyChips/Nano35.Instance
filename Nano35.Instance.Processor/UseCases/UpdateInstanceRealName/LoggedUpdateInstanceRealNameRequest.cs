using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceRealName
{
    public class LoggedUpdateInstanceRealNameRequest :
        PipeNodeBase<
            IUpdateInstanceRealNameRequestContract,
            IUpdateInstanceRealNameResultContract>
    {
        private readonly ILogger<LoggedUpdateInstanceRealNameRequest> _logger;

        public LoggedUpdateInstanceRealNameRequest(
            ILogger<LoggedUpdateInstanceRealNameRequest> logger,
            IPipeNode<IUpdateInstanceRealNameRequestContract,
                IUpdateInstanceRealNameResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateInstanceRealNameResultContract> Ask
        (IUpdateInstanceRealNameRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateInstanceRealName starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateInstanceRealName ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateInstanceRealNameSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateInstanceRealNameErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}