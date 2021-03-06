﻿using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;
using Nano35.Instance.Api.Helpers;
using Nano35.Instance.Api.Requests;
using Nano35.Instance.Api.Requests.CreateClient;
using Nano35.Instance.Api.Requests.GetAllClients;
using Nano35.Instance.Api.Requests.GetClientById;
using Nano35.Instance.Api.Requests.UpdateClientsEmail;
using Nano35.Instance.Api.Requests.UpdateClientsName;
using Nano35.Instance.Api.Requests.UpdateClientsPhone;
using Nano35.Instance.Api.Requests.UpdateClientsSelle;
using Nano35.Instance.Api.Requests.UpdateClientsState;

namespace Nano35.Instance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IServiceProvider _services;
        public ClientsController(IServiceProvider services) => _services = services;

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllClientsHttpResponse))]
        public IActionResult GetAllClients([FromQuery] GetAllClientsHttpQuery query)
        {
            var result =
                new LoggedUseCasePipeNode<IGetAllClientsRequestContract, IGetAllClientsResultContract>(_services.GetService(typeof(ILogger<IGetAllClientsRequestContract>)) as ILogger<IGetAllClientsRequestContract>,
                    new GetAllClientsUseCase(_services.GetService(typeof(IBus)) as IBus))
                .Ask(new GetAllClientsRequestContract()
                {
                    InstanceId = query.InstanceId,
                    ClientStateId = query.ClientStateId
                })
                .Result;
            return result.IsSuccess() ? (IActionResult) 
                Ok(result.Success) : 
                BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetClientByIdHttpResponse))]
        public IActionResult GetClientById(Guid id)
        {
            var result =
                new LoggedUseCasePipeNode<IGetClientByIdRequestContract, IGetClientByIdResultContract>(_services.GetService(typeof(ILogger<IGetClientByIdRequestContract>)) as ILogger<IGetClientByIdRequestContract>,
                    new GetClientByIdUseCase(_services.GetService(typeof(IBus)) as IBus))
                    .Ask(new GetClientByIdRequestContract() {UnitId = id})
                .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateClientHttpResponse))]
        public IActionResult CreateClient([FromBody] CreateClientHttpBody body)
        {
            var result = 
                new LoggedUseCasePipeNode<ICreateClientRequestContract, ICreateClientResultContract>(_services.GetService(typeof(ILogger<ICreateClientRequestContract>)) as ILogger<ICreateClientRequestContract>,
                    new CreateClientUseCase(_services.GetService(typeof(IBus)) as IBus, _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))
                .Ask(
                    new CreateClientRequestContract()
                        {Name = body.Name,
                         Email = body.Email ?? "",
                         Phone = body.Phone,
                         Selle = body.Selle ?? 0.0,
                         InstanceId = body.InstanceId,
                         ClientStateId = body.ClientStateId,
                         NewId = body.NewId})
                .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPatch("{id}/Email")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsEmailHttpResponse))]
        public IActionResult UpdateClientsEmail([FromBody] UpdateClientsEmailHttpBody body, Guid id)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract>(_services.GetService(typeof(ILogger<IUpdateClientsEmailRequestContract>)) as ILogger<IUpdateClientsEmailRequestContract>,
                        new UpdateClientsEmailUseCase(_services.GetService(typeof(IBus)) as IBus))
                    .Ask(new UpdateClientsEmailRequestContract() {ClientId = id, Email = body.Email})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPatch("{id}/Name")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsNameHttpResponse))]
        public IActionResult UpdateClientsName([FromBody] UpdateClientsNameHttpBody body, Guid id)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract>(_services.GetService(typeof(ILogger<IUpdateClientsNameRequestContract>)) as ILogger<IUpdateClientsNameRequestContract>,
                        new UpdateClientsNameUseCase(_services.GetService(typeof(IBus)) as IBus))
                    .Ask(new UpdateClientsNameRequestContract() {ClientId = id, Name = body.Name})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPatch("{id}/Phone")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsPhoneHttpResponse))]
        public IActionResult UpdateClientsPhone([FromBody] UpdateClientsPhoneHttpBody body, Guid id)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract>(_services.GetService(typeof(ILogger<IUpdateClientsPhoneRequestContract>)) as ILogger<IUpdateClientsPhoneRequestContract>,
                        new UpdateClientsPhoneUseCase(_services.GetService(typeof(IBus)) as IBus))
                    .Ask(new UpdateClientsPhoneRequestContract() {ClientId = id, Phone = body.Phone})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPatch("{id}/Selle")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsSelleHttpResponse))]
        public IActionResult UpdateClientsSelle([FromBody] UpdateClientsSelleHttpBody body, Guid id)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateClientsSelleRequestContract, IUpdateClientsSelleResultContract>(_services.GetService(typeof(ILogger<IUpdateClientsSelleRequestContract>)) as ILogger<IUpdateClientsSelleRequestContract>,
                        new UpdateClientsSelleUseCase(_services.GetService(typeof(IBus)) as IBus))
                    .Ask(new UpdateClientsSelleRequestContract() {ClientId = id, Selle = body.Selle})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPatch("{id}/State")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsStateHttpResponse))]
        public IActionResult UpdateClientsState([FromBody] UpdateClientsStateHttpBody body, Guid id)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract>(_services.GetService(typeof(ILogger<IUpdateClientsStateRequestContract>)) as ILogger<IUpdateClientsStateRequestContract>,
                        new UpdateClientsStateUseCase(_services.GetService(typeof(IBus)) as IBus))
                    .Ask(new UpdateClientsStateRequestContract() {ClientId = id, StateId = body.StateId})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpDelete]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteClient(Guid id) => Ok(id);
    }
}