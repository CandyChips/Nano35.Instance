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
using Nano35.Instance.Api.Requests.GetAllRegions;

namespace Nano35.Instance.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class InstancesRegionsController : ControllerBase
    {

        private readonly IServiceProvider _services;

        public InstancesRegionsController(IServiceProvider services)
        {
            _services = services;
        }

        
        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllRegionsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllRegionsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllRegions()
        {
            return await 
                new ValidatedPipeNode<GetAllRegionsHttpQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllRegionsHttpQuery>)) as IValidator<GetAllRegionsHttpQuery>,
                    new ConvertedGetAllRegionsOnHttpContext(
                        new LoggedPipeNode<IGetAllRegionsRequestContract, IGetAllRegionsResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllRegionsRequestContract>)) as ILogger<IGetAllRegionsRequestContract>,
                            new GetAllRegionsUseCase(_services.GetService(typeof(IBus)) as IBus))))
                .Ask(new GetAllRegionsHttpQuery());
        }

    }
}
