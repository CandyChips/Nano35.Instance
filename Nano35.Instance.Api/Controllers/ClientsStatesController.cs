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
    [ApiController]
    [Route("[controller]")]
    public class ClientsStatesController : ControllerBase
    {
        private readonly IServiceProvider _services;
        public ClientsStatesController(IServiceProvider services) => _services = services;
        
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllClientStatesHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        public IActionResult GetAllClientStates()
        {
            var result =
                new LoggedUseCasePipeNode<IGetAllClientStatesRequestContract, IGetAllClientStatesResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllClientStatesRequestContract>)) as ILogger<IGetAllClientStatesRequestContract>,
                        new GetAllClientStatesUseCase(
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new GetAllClientStatesRequestContract())
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }
}
