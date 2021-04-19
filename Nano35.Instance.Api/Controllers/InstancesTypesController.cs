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
using Nano35.Instance.Api.Requests.GetAllInstanceTypes;

namespace Nano35.Instance.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class InstancesTypesController : ControllerBase
    {

        private readonly IServiceProvider _services;

        public InstancesTypesController(IServiceProvider services)
        {
            _services = services;
        }

        
        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllInstanceTypesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllInstanceTypesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllInstanceTypes()
        {
            return await new ConvertedGetAllInstanceTypesOnHttpContext( 
                    new LoggedPipeNode<IGetAllInstanceTypesRequestContract, IGetAllInstanceTypesResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllInstanceTypesRequestContract>)) as ILogger<IGetAllInstanceTypesRequestContract>,
                        new ValidatedPipeNode<IGetAllInstanceTypesRequestContract, IGetAllInstanceTypesResultContract>(                      
                            _services.GetService(typeof(IValidator<IGetAllInstanceTypesRequestContract>)) as IValidator<IGetAllInstanceTypesRequestContract>,
                            new GetAllInstanceTypesUseCase(_services.GetService(typeof(IBus)) as IBus))))
                .Ask(new GetAllInstanceTypesHttpQuery());
        }

    }
}
