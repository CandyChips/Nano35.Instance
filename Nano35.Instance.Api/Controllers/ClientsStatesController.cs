using System;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;
using Nano35.Instance.Api.Requests;
using Nano35.Instance.Api.Requests.GetAllClientStates;

namespace Nano35.Instance.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ClientsStatesController : ControllerBase
    {

        private readonly IServiceProvider _services;

        public ClientsStatesController(IServiceProvider services)
        {
            _services = services;
        }

        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllClientStatesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllClientStatesErrorHttpResponse))]
        public async Task<IActionResult> GetAllClientStates()
        {
            return await new ConvertedGetAllClientStatesOnHttpContext(
                    new LoggedPipeNode<IGetAllClientStatesRequestContract, IGetAllClientStatesResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllClientStatesRequestContract>)) as
                            ILogger<IGetAllClientStatesRequestContract>,
                        new ValidatedPipeNode<IGetAllClientStatesRequestContract, IGetAllClientStatesResultContract>(
                            _services.GetService(typeof(IValidator<IGetAllClientStatesRequestContract>)) as
                                IValidator<IGetAllClientStatesRequestContract>,
                            new GetAllClientStatesUseCase(
                                _services.GetService(typeof(IBus)) as IBus))))
                .Ask(new GetAllClientStatesHttpQuery());
        }

    }
}
