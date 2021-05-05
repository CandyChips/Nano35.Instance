using System;
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
using Nano35.Instance.Api.Requests.UpdateClientsType;

namespace Nano35.Instance.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IServiceProvider _services;
        public ClientsController(IServiceProvider services) => _services = services;

        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllClientsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllClientsErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetAllClients([FromQuery] GetAllClientsHttpQuery query)
        {
            var result =
                new LoggedUseCasePipeNode<IGetAllClientsRequestContract, IGetAllClientsResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllClientsRequestContract>)) as ILogger<IGetAllClientsRequestContract>,
                    new GetAllClientsUseCase(
                        _services.GetService(typeof(IBus)) as IBus))
                .Ask(new GetAllClientsRequestContract()
                {
                    InstanceId = query.InstanceId,
                    ClientStateId = query.ClientStateId,
                    ClientTypeId = query.ClientTypeId
                })
                .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetClientByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetClientByIdErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetClientById(Guid id)
        {
            var result =
                new LoggedUseCasePipeNode<IGetClientByIdRequestContract, IGetClientByIdResultContract>(
                    _services.GetService(typeof(ILogger<IGetClientByIdRequestContract>)) as
                        ILogger<IGetClientByIdRequestContract>,
                    new GetClientByIdUseCase(_services.GetService(typeof(IBus)) as IBus))
                .Ask(new GetClientByIdRequestContract() {UnitId = id})
                .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateClientSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateClientErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult CreateClient([FromBody] CreateClientHttpBody body)
        {
            var result = 
                new LoggedUseCasePipeNode<ICreateClientRequestContract, ICreateClientResultContract>(
                    _services.GetService(typeof(ILogger<ICreateClientRequestContract>)) as
                        ILogger<ICreateClientRequestContract>,
                    new CreateClientUseCase(_services.GetService(typeof(IBus)) as IBus,
                        _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))
                .Ask(
                    new CreateClientRequestContract()
                    {
                        Name = body.Name,
                        Email = body.Email,
                        Phone = body.Phone,
                        Selle = body.Selle,
                        InstanceId = body.InstanceId,
                        ClientStateId = body.ClientStateId,
                        ClientTypeId = body.ClientTypeId,
                        NewId = body.NewId
                    })
                .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [Authorize]
        [HttpPatch("{id}/Email")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsEmailSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsEmailErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult UpdateClientsEmail([FromBody] UpdateClientsEmailHttpBody body, Guid id)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateClientsEmailRequestContract>)) as
                            ILogger<IUpdateClientsEmailRequestContract>,
                        new UpdateClientsEmailUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))
                    .Ask(new UpdateClientsEmailRequestContract() {ClientId = id, Email = body.Email})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [Authorize]
        [HttpPatch("{id}/Name")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsNameErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult UpdateClientsName([FromBody] UpdateClientsNameHttpBody body, Guid id)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateClientsNameRequestContract>)) as
                            ILogger<IUpdateClientsNameRequestContract>,
                        new UpdateClientsNameUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))
                    .Ask(new UpdateClientsNameRequestContract() {ClientId = id, Name = body.Name})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [AllowAnonymous]
        [HttpPatch("{id}/Phone")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsPhoneSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsPhoneErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult UpdateClientsPhone([FromBody] UpdateClientsPhoneHttpBody body, Guid id)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateClientsPhoneRequestContract>)) as
                            ILogger<IUpdateClientsPhoneRequestContract>,
                        new UpdateClientsPhoneUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))
                    .Ask(new UpdateClientsPhoneRequestContract() {ClientId = id, Phone = body.Phone})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [AllowAnonymous]
        [HttpPatch("{id}/Selle")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsSelleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsSelleErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult UpdateClientsSelle([FromBody] UpdateClientsSelleHttpBody body, Guid id)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateClientsSelleRequestContract, IUpdateClientsSelleResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateClientsSelleRequestContract>)) as
                            ILogger<IUpdateClientsSelleRequestContract>,
                        new UpdateClientsSelleUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))
                    .Ask(new UpdateClientsSelleRequestContract() {ClientId = id, Selle = body.Selle})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [AllowAnonymous]
        [HttpPatch("{id}/State")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsStateSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsStateErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult UpdateClientsState([FromBody] UpdateClientsStateHttpBody body, Guid id)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateClientsStateRequestContract>)) as
                            ILogger<IUpdateClientsStateRequestContract>,
                        new UpdateClientsStateUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))
                    .Ask(new UpdateClientsStateRequestContract() {ClientId = id, StateId = body.StateId})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [Authorize]
        [HttpPatch("{id}/Type")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsTypeSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsTypeErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult UpdateClientsType([FromBody] UpdateClientsTypeHttpBody body, Guid id)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateClientsTypeRequestContract>)) as
                            ILogger<IUpdateClientsTypeRequestContract>,
                        new UpdateClientsTypeUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))
                    .Ask(new UpdateClientsTypeRequestContract() {ClientId = id, TypeId = body.TypeId})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteClient(Guid id) => Ok(id);
    }
}