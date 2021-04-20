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
using Nano35.Instance.Api.Helpers;
using Nano35.Instance.Api.Requests;
using Nano35.Instance.Api.Requests.CreateClient;
using Nano35.Instance.Api.Requests.GetAllClients;
using Nano35.Instance.Api.Requests.GetAllClientStates;
using Nano35.Instance.Api.Requests.GetAllClientTypes;
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

        /// ToDo Hey Maslyonok
        /// <summary>
        /// Controller provide IServiceProvider from asp net core DI
        /// for registration services to pipe nodes
        /// </summary>
        /// 
        public ClientsController(IServiceProvider services)
        {
            _services = services;
        }

        /// ToDo Hey Maslyonok
        /// <summary>
        /// Controllers accept a HttpContext type
        /// All controllers actions works by pipelines
        /// Implementation works with 3 steps
        /// 1. Setup DI services from IServiceProvider;
        /// 2. Building pipeline like a onion
        ///     '(PipeNode1(PipeNode2(PipeNode3(...).Ask()).Ask()).Ask()).Ask()';
        /// 3. Response pattern match of pipeline response;
        /// </summary>
        ///
        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllClientsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllClientsErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllClients(
            [FromQuery] GetAllClientsHttpQuery query)
        {
            return await new ValidatedPipeNode<GetAllClientsHttpQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllClientsHttpQuery>)) as IValidator<GetAllClientsHttpQuery>,
                    new ConvertedGetAllClientsOnHttpContext(
                        new LoggedPipeNode<IGetAllClientsRequestContract, IGetAllClientsResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllClientsRequestContract>)) as
                                ILogger<IGetAllClientsRequestContract>,
                            new GetAllClientsUseCase(
                                    _services.GetService(typeof(IBus)) as IBus))))
                .Ask(query);

        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetClientByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetClientByIdErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetClientById(Guid id)
        {
            return await new ValidatedPipeNode<Guid, IActionResult>(
                    _services.GetService(typeof(IValidator<Guid>)) as IValidator<Guid>,
                    new ConvertedGetClientByIdOnHttpContext(
                        new LoggedPipeNode<IGetClientByIdRequestContract, IGetClientByIdResultContract>(
                            _services.GetService(typeof(ILogger<IGetClientByIdRequestContract>)) as ILogger<IGetClientByIdRequestContract>,
                            new GetClientByIdUseCase(
                                    _services.GetService(typeof(IBus)) as IBus))))
                .Ask(id);
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("Client")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateClientSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateClientErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateClient(
            [FromBody] CreateClientHttpBody body)
        {
            return await new ValidatedPipeNode<CreateClientHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<CreateClientHttpBody>)) as IValidator<CreateClientHttpBody>,
                new ConvertedCreateClientOnHttpContext(
                    new LoggedPipeNode<ICreateClientRequestContract, ICreateClientResultContract>(
                        _services.GetService(typeof(ILogger<ICreateClientRequestContract>)) as ILogger<ICreateClientRequestContract>,
                            new CreateClientUseCase(_services.GetService(typeof(IBus)) as IBus, 
                                _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("Email")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsEmailSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsEmailErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateClientsEmail(
            [FromBody] UpdateClientsEmailHttpBody body)
        {
            return await new ValidatedPipeNode<UpdateClientsEmailHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateClientsEmailHttpBody>)) as IValidator<UpdateClientsEmailHttpBody>,
                    new ConvertedUpdateClientsEmailOnHttpContext(
                        new LoggedPipeNode<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateClientsEmailRequestContract>)) as
                                ILogger<IUpdateClientsEmailRequestContract>,
                            new UpdateClientsEmailUseCase(
                                    _services.GetService(typeof(IBus)) as IBus,
                                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);

        }

        [Authorize]
        [HttpPatch]
        [Route("Name")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsNameErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateClientsName(
            [FromBody] UpdateClientsNameHttpBody body)
        {
            return await new ValidatedPipeNode<UpdateClientsNameHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateClientsNameHttpBody>)) as IValidator<UpdateClientsNameHttpBody>,
                    new ConvertedUpdateClientsNameOnHttpContext(
                        new LoggedPipeNode<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateClientsNameRequestContract>)) as
                                ILogger<IUpdateClientsNameRequestContract>,
                            new UpdateClientsNameUseCase(
                                    _services.GetService(typeof(IBus)) as IBus,
                                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);

        }

        [AllowAnonymous]
        [HttpPatch]
        [Route("Phone")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsPhoneSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsPhoneErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateClientsPhone(
            [FromBody] UpdateClientsPhoneHttpBody body)
        {
            return await new ValidatedPipeNode<UpdateClientsPhoneHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateClientsPhoneHttpBody>)) as IValidator<UpdateClientsPhoneHttpBody>,
                    new ConvertedUpdateClientsPhoneOnHttpContext(
                        new LoggedPipeNode<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateClientsPhoneRequestContract>)) as
                                ILogger<IUpdateClientsPhoneRequestContract>,
                            new UpdateClientsPhoneUseCase(
                                    _services.GetService(typeof(IBus)) as IBus,
                                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [AllowAnonymous]
        [HttpPatch]
        [Route("Selle")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsSelleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsSelleErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateClientsSelle(
            [FromBody] UpdateClientsSelleHttpBody body)
        {
            return await new ValidatedPipeNode<UpdateClientsSelleHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateClientsSelleHttpBody>)) as IValidator<UpdateClientsSelleHttpBody>,
                    new ConvertedUpdateClientsSelleOnHttpContext(
                        new LoggedPipeNode<IUpdateClientsSelleRequestContract, IUpdateClientsSelleResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateClientsSelleRequestContract>)) as
                                ILogger<IUpdateClientsSelleRequestContract>,
                            new UpdateClientsSelleUseCase(
                                    _services.GetService(typeof(IBus)) as IBus,
                                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [AllowAnonymous]
        [HttpPatch]
        [Route("State")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsStateSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsStateErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateClientsState(
            [FromBody] UpdateClientsStateHttpBody body)
        {
            return await new ValidatedPipeNode<UpdateClientsStateHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateClientsStateHttpBody>)) as
                        IValidator<UpdateClientsStateHttpBody>,
                    new ConvertedUpdateClientsStateOnHttpContext(
                        new LoggedPipeNode<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateClientsStateRequestContract>)) as
                                ILogger<IUpdateClientsStateRequestContract>,
                            new UpdateClientsStateUseCase(
                                    _services.GetService(typeof(IBus)) as IBus,
                                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);

        }

        [Authorize]
        [HttpPatch]
        [Route("Type")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsTypeSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsTypeErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateClientsType(
            [FromBody] UpdateClientsTypeHttpBody body)
        {
            return await new ValidatedPipeNode<UpdateClientsTypeHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateClientsTypeHttpBody>)) as
                        IValidator<UpdateClientsTypeHttpBody>,
                    new ConvertedUpdateClientsTypeOnHttpContext(
                        new LoggedPipeNode<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateClientsTypeRequestContract>)) as
                                ILogger<IUpdateClientsTypeRequestContract>,
                            new UpdateClientsTypeUseCase(
                                    _services.GetService(typeof(IBus)) as IBus,
                                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            return Ok(id);
        }
    }
}