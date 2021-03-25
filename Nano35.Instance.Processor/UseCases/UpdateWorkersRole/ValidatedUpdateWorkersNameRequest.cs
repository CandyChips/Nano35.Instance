﻿using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersRole
{
    public class UpdateWorkersRoleValidatorErrorResult : 
        IUpdateWorkersRoleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateWorkersRoleRequest:
        PipeNodeBase<
            IUpdateWorkersRoleRequestContract,
            IUpdateWorkersRoleResultContract>
    {
        public ValidatedUpdateWorkersRoleRequest(
            IPipeNode<IUpdateWorkersRoleRequestContract,
             IUpdateWorkersRoleResultContract> next) : base(next)
        {}

        public override async Task<IUpdateWorkersRoleResultContract> Ask(
            IUpdateWorkersRoleRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateWorkersRoleValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}