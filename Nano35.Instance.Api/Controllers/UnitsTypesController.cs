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
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UnitsTypesController : ControllerBase
    {
        private readonly IServiceProvider _services;
        public UnitsTypesController(IServiceProvider services) => _services = services;

        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllUnitTypesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllUnitTypesErrorHttpResponse))] 
        public Task<IActionResult> GetAllUnitTypes() =>
            new ConvertedGetAllUnitTypesOnHttpContext(
                new LoggedPipeNode<IGetAllUnitTypesRequestContract, IGetAllUnitTypesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllUnitTypesRequestContract>)) as ILogger<IGetAllUnitTypesRequestContract>,
                    new GetAllUnitTypesUseCase(
                        _services.GetService(typeof(IBus)) as IBus)))
                .Ask(new GetAllUnitTypesHttpQuery());
    }
}
