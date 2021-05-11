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
using Nano35.Instance.Api.Requests.GetAllClientTypes;

namespace Nano35.Instance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsTypesController : ControllerBase
    {
        private readonly IServiceProvider _services;
        public ClientsTypesController(IServiceProvider services) => _services = services;

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllClientTypesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllClientTypesErrorHttpResponse))]
        public IActionResult GetAllClientTypes()
        {
            var result =
                new LoggedUseCasePipeNode<IGetAllClientTypesRequestContract, IGetAllClientTypesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllClientTypesRequestContract>)) as ILogger<IGetAllClientTypesRequestContract>,
                    new GetAllClientTypesUseCase(
                        _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new GetAllClientTypesRequestContract())
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }
}
