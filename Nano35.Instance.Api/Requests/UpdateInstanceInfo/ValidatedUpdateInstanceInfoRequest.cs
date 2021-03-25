﻿using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceInfo
{
    public class UpdateInstanceInfoValidatorErrorResult : IUpdateInstanceInfoErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateInstanceInfoRequest:
        PipeNodeBase
        <IUpdateInstanceInfoRequestContract,
            IUpdateInstanceInfoResultContract>
    {

        public ValidatedUpdateInstanceInfoRequest(
            IPipeNode<IUpdateInstanceInfoRequestContract,
                IUpdateInstanceInfoResultContract> next) : base(next)
        {}

        public override async Task<IUpdateInstanceInfoResultContract> Ask(
            IUpdateInstanceInfoRequestContract input)
        {
            if (false)
            {
                return new UpdateInstanceInfoValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}