﻿using System;
using System.Threading.Tasks;
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
    [ApiController]
    [Route("[controller]")]
    public class WorkersRolesController : ControllerBase
    {
        private readonly IServiceProvider _services;
        public WorkersRolesController(IServiceProvider services) => _services = services;

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllWorkerRolesHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        public IActionResult GetAllWorkerRoles()
        {
            var result =
                new LoggedUseCasePipeNode<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllWorkerRolesRequestContract>)) as ILogger<IGetAllWorkerRolesRequestContract>,
                        new GetAllWorkerRolesUseCase(
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new GetAllWorkerRolesRequestContract())
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }
}
