﻿using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetInstanceById
{
    public class GetInstanceByIdUseCase : UseCaseEndPointNodeBase<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract>
    {
        private readonly IBus _bus;
        public GetInstanceByIdUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetInstanceByIdResultContract>> Ask(IGetInstanceByIdRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract>(_bus, input)
                .GetResponse();
    }
}