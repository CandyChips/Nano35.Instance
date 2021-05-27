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
using Nano35.Instance.Api.Requests.GetAllUnitTypes;

namespace Nano35.Instance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UnitsTypesController : ControllerBase
    {
        private readonly IServiceProvider _services;
        public UnitsTypesController(IServiceProvider services) => _services = services;

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllUnitTypesHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        public IActionResult GetAllUnitTypes()
        {
            var result =
                new LoggedUseCasePipeNode<IGetAllUnitTypesRequestContract, IGetAllUnitTypesResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllUnitTypesRequestContract>)) as ILogger<IGetAllUnitTypesRequestContract>,
                        new GetAllUnitTypesUseCase(
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new GetAllUnitTypesRequestContract())
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }
}
