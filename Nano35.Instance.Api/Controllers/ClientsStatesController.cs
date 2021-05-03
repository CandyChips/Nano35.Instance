using System;
using System.Threading.Tasks;
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
        public ClientsStatesController(IServiceProvider services) => _services = services;
        
        [AllowAnonymous]
        [HttpGet("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllClientStatesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllClientStatesErrorHttpResponse))] 
        public IActionResult GetAllClientStates()
        {
            var result =
                new LoggedUseCasePipeNode<IGetAllClientStatesRequestContract, IGetAllClientStatesSuccessResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllClientStatesRequestContract>)) as ILogger<IGetAllClientStatesRequestContract>,
                        new GetAllClientStatesUseCase(
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new GetAllClientStatesRequestContract())
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }
}
