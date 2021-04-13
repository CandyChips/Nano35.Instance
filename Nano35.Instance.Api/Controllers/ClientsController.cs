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
        
        private readonly IServiceProvider  _services;

        /// ToDo Hey Maslyonok
        /// <summary>
        /// Controller provide IServiceProvider from asp net core DI
        /// for registration services to pipe nodes
        /// </summary>
        /// 
        public ClientsController(IServiceProvider  services)
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
        [Authorize]
        [HttpGet]
        [Route("GetAllClients")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllClientsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllClientsErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> GetAllClients(
            [FromQuery] GetAllClientsHttpQuery query)
        {
            return await new ConvertedGetAllClientsOnHttpContext(
                new LoggedPipeNode<IGetAllClientsRequestContract, IGetAllClientsResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllClientsRequestContract>)) as ILogger<IGetAllClientsRequestContract>, 
                    new ValidatedPipeNode<IGetAllClientsRequestContract, IGetAllClientsResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllClientsRequestContract>)) as IValidator<IGetAllClientsRequestContract>,
                        new GetAllClientsUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(query);

        }

        [Authorize]
        [HttpGet]
        [Route("GetClientById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetClientByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetClientByIdErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> GetClientById(
            [FromQuery] GetClientByIdHttpQuery query)
        {
            return await new ConvertedGetClientByIdOnHttpContext(
                new LoggedPipeNode<IGetClientByIdRequestContract, IGetClientByIdResultContract>(
                    _services.GetService(typeof(ILogger<IGetClientByIdRequestContract>)) as ILogger<IGetClientByIdRequestContract>, 
                    new ValidatedPipeNode<IGetClientByIdRequestContract, IGetClientByIdResultContract>(
                        _services.GetService(typeof(IValidator<IGetClientByIdRequestContract>)) as IValidator<IGetClientByIdRequestContract>,
                        new GetClientByIdUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(query);
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllClientTypes")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllClientTypesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllClientTypesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllClientTypes()
        {
            return await new ConvertedGetAllClientTypesOnHttpContext(
                new LoggedPipeNode<IGetAllClientTypesRequestContract, IGetAllClientTypesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllClientTypesRequestContract>)) as ILogger<IGetAllClientTypesRequestContract>,
                    new ValidatedPipeNode<IGetAllClientTypesRequestContract, IGetAllClientTypesResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllClientTypesRequestContract>)) as IValidator<IGetAllClientTypesRequestContract>,
                        new GetAllClientTypesUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(new GetAllClientTypesHttpQuery());
        }
    
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllClientStates")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllClientStatesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllClientStatesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllClientStates()
        {
            return await new ConvertedGetAllClientStatesOnHttpContext(
                new LoggedPipeNode<IGetAllClientStatesRequestContract, IGetAllClientStatesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllClientStatesRequestContract>)) as ILogger<IGetAllClientStatesRequestContract>,
                    new ValidatedPipeNode<IGetAllClientStatesRequestContract, IGetAllClientStatesResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllClientStatesRequestContract>)) as IValidator<IGetAllClientStatesRequestContract>,
                        new GetAllClientStatesUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(new GetAllClientStatesHttpQuery());
        }

        [Authorize]
        [HttpPost]
        [Route("CreateClient")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateClientSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateClientErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> CreateClient(
            [FromBody]CreateClientHttpBody body)
        {
            return await new ConvertedCreateClientOnHttpContext(
                new LoggedPipeNode<ICreateClientRequestContract, ICreateClientResultContract>(
                    _services.GetService(typeof(ILogger<ICreateClientRequestContract>)) as ILogger<ICreateClientRequestContract>,
                    new ValidatedPipeNode<ICreateClientRequestContract, ICreateClientResultContract>(
                        _services.GetService(typeof(IValidator<ICreateClientRequestContract>)) as IValidator<ICreateClientRequestContract>,
                        new CreateClientUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("UpdateClientsEmail")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsEmailSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsEmailErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateClientsEmail(
            [FromBody] UpdateClientsEmailHttpBody body)
        {
            return await new ConvertedUpdateClientsEmailOnHttpContext(
                new LoggedPipeNode<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateClientsEmailRequestContract>)) as ILogger<IUpdateClientsEmailRequestContract>,
                    new ValidatedPipeNode<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract>(
                        _services.GetService(typeof(IValidator<IUpdateClientsEmailRequestContract>)) as IValidator<IUpdateClientsEmailRequestContract>,
                        new UpdateClientsEmailUseCase(
                            _services.GetService(typeof(IBus)) as IBus, 
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);

        }

        [Authorize]
        [HttpPatch]
        [Route("UpdateClientsName")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsNameErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateClientsName(
            [FromBody] UpdateClientsNameHttpBody body)
        {
            return await new ConvertedUpdateClientsNameOnHttpContext( 
                new LoggedPipeNode<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract>(
                   _services.GetService(typeof(ILogger<IUpdateClientsNameRequestContract>)) as ILogger<IUpdateClientsNameRequestContract>,
                    new ValidatedPipeNode<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract>(
                        _services.GetService(typeof(IValidator<IUpdateClientsNameRequestContract>)) as IValidator<IUpdateClientsNameRequestContract>,
                        new UpdateClientsNameUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);

        }
        
        [Authorize]
        [HttpPatch]
        [Route("UpdateClientsPhone")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsPhoneSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsPhoneErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateClientsPhone(
            [FromBody] UpdateClientsPhoneHttpBody body)
        {
            return await new ConvertedUpdateClientsPhoneOnHttpContext(
                new LoggedPipeNode<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateClientsPhoneRequestContract>)) as ILogger<IUpdateClientsPhoneRequestContract>,
                    new ValidatedPipeNode<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract>(
                        _services.GetService(typeof(IValidator<IUpdateClientsPhoneRequestContract>)) as IValidator<IUpdateClientsPhoneRequestContract>,
                        new UpdateClientsPhoneUseCase(
                            _services.GetService(typeof(IBus)) as IBus, 
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("UpdateClientsSelle")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsSelleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsSelleErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]  
        public async Task<IActionResult> UpdateClientsSelle(
            [FromBody] UpdateClientsSelleHttpBody body)
        {
            return await new ConvertedUpdateClientsSelleOnHttpContext(
                new LoggedPipeNode<IUpdateClientsSelleRequestContract, IUpdateClientsSelleResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateClientsSelleRequestContract>)) as ILogger<IUpdateClientsSelleRequestContract>,
                    new ValidatedPipeNode<IUpdateClientsSelleRequestContract, IUpdateClientsSelleResultContract>(
                        _services.GetService(typeof(IValidator<IUpdateClientsSelleRequestContract>)) as IValidator<IUpdateClientsSelleRequestContract>,
                        new UpdateClientsSelleUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("UpdateClientsState")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsStateSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsStateErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]  
        public async Task<IActionResult> UpdateClientsState(
            [FromBody] UpdateClientsStateHttpBody body)
        {
            return await new ConvertedUpdateClientsStateOnHttpContext( 
                new LoggedPipeNode<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateClientsStateRequestContract>)) as ILogger<IUpdateClientsStateRequestContract>,  
                    new ValidatedPipeNode<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract>(
                        _services.GetService(typeof(IValidator<IUpdateClientsStateRequestContract>)) as IValidator<IUpdateClientsStateRequestContract>,
                        new UpdateClientsStateUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);

        }

        [Authorize]
        [HttpPatch]
        [Route("UpdateClientsType")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsTypeSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsTypeErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateClientsType(
            [FromBody] UpdateClientsTypeHttpBody body)
        {
            return await new ConvertedUpdateClientsTypeOnHttpContext(
                new LoggedPipeNode<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateClientsTypeRequestContract>)) as ILogger<IUpdateClientsTypeRequestContract>,
                    new ValidatedPipeNode<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract>(
                        _services.GetService(typeof(IValidator<IUpdateClientsTypeRequestContract>)) as IValidator<IUpdateClientsTypeRequestContract>,
                        new UpdateClientsTypeUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }
    }
}