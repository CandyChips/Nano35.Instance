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
using Nano35.Instance.Api.Requests.GetAllClientTypes;

namespace Nano35.Instance.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ClientsTypesController : ControllerBase
    {

        private readonly IServiceProvider _services;

        public ClientsTypesController(IServiceProvider services)
        {
            _services = services;
        }

        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllClientTypesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllClientTypesErrorHttpResponse))]
        public async Task<IActionResult> GetAllClientTypes()
        {
            return await new ConvertedGetAllClientTypesOnHttpContext(
                    new LoggedPipeNode<IGetAllClientTypesRequestContract, IGetAllClientTypesResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllClientTypesRequestContract>)) as
                            ILogger<IGetAllClientTypesRequestContract>,
                        new ValidatedPipeNode<IGetAllClientTypesRequestContract, IGetAllClientTypesResultContract>(
                            _services.GetService(typeof(IValidator<IGetAllClientTypesRequestContract>)) as
                                IValidator<IGetAllClientTypesRequestContract>,
                            new GetAllClientTypesUseCase(
                                _services.GetService(typeof(IBus)) as IBus))))
                .Ask(new GetAllClientTypesHttpQuery());
        }
    }
}
