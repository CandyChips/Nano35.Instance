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
using Nano35.Instance.Api.Requests.GetAllRegions;

namespace Nano35.Instance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstancesRegionsController : ControllerBase
    {
        private readonly IServiceProvider _services;
        public InstancesRegionsController(IServiceProvider services) => _services = services;

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllRegionsHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        public IActionResult GetAllRegions()
        {
            var result =
                new LoggedUseCasePipeNode<IGetAllRegionsRequestContract, IGetAllRegionsResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllRegionsRequestContract>)) as ILogger<IGetAllRegionsRequestContract>,
                    new GetAllRegionsUseCase(
                        _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new GetAllRegionsRequestContract())
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }
}
