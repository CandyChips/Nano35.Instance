﻿using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstancePhone
{
    public class UpdateInstancePhoneValidatorErrorResult : IUpdateInstancePhoneErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateInstancePhoneRequest:
        PipeNodeBase
        <IUpdateInstancePhoneRequestContract,
            IUpdateInstancePhoneResultContract>
    {

        public ValidatedUpdateInstancePhoneRequest(
            IPipeNode<IUpdateInstancePhoneRequestContract,
                IUpdateInstancePhoneResultContract> next) : base(next)
        {}

        public override async Task<IUpdateInstancePhoneResultContract> Ask(
            IUpdateInstancePhoneRequestContract input)
        {
            if (false)
            {
                return new UpdateInstancePhoneValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}