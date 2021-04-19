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
using Nano35.Instance.Api.Requests.GetAllWorkerRoles;

namespace Nano35.Instance.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WorkersRolesController : ControllerBase
    {

        private readonly IServiceProvider _services;

        public WorkersRolesController(IServiceProvider services)
        {
            _services = services;
        }

            
        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllWorkerRolesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllWorkerRolesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllWorkerRoles()
        {
            return await new ConvertedGetAllWorkerRolesOnHttpContext(
                    new LoggedPipeNode<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllWorkerRolesRequestContract>)) as ILogger<IGetAllWorkerRolesRequestContract>,
                        new ValidatedPipeNode<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesResultContract>(
                            _services.GetService(typeof(IValidator<IGetAllWorkerRolesRequestContract>)) as IValidator<IGetAllWorkerRolesRequestContract>,
                            new GetAllWorkerRolesUseCase(
                                _services.GetService(typeof(IBus)) as IBus))))
                .Ask(new GetAllWorkerRolesHttpQuery());
        }

    }
}
