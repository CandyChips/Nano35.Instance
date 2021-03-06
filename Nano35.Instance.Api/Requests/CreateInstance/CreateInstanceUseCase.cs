﻿using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreateInstance
{
    public class CreateInstanceUseCase : UseCaseEndPointNodeBase<ICreateInstanceRequestContract, ICreateInstanceResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public CreateInstanceUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<UseCaseResponse<ICreateInstanceResultContract>> Ask(ICreateInstanceRequestContract input)
        {
            input.UserId = _auth.CurrentUserId;
            input.Phone = PhoneConverter.RuPhoneConverter(input.Phone);
            return await new MasstransitUseCaseRequest<ICreateInstanceRequestContract, ICreateInstanceResultContract>(_bus, input)
                .GetResponse();
        }
    }
}