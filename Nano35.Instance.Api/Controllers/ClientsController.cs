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
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllClientsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllClientsRequest>));
            
            return await 
                new ConvertedGetAllClientsOnHttpContext(
                new ValidatedGetAllClientsRequest(
                    new LoggedGetAllClientsRequest(logger, 
                        new GetAllClientsUseCase(bus)))
                ).Ask(query);

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
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetClientByIdRequest>)_services.GetService(typeof(ILogger<LoggedGetClientByIdRequest>));
            var validator = (IValidator<IGetClientByIdRequestContract>) _services.GetService(typeof(IValidator<IGetClientByIdRequestContract>));
            return await 
                new ConvertedGetClientByIdOnHttpContext(
                new ValidatedGetClientByIdRequest(validator,
                    new LoggedGetClientByIdRequest(logger, 
                        new GetClientByIdUseCase(bus)))).Ask(query);
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllClientTypes")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllClientTypesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllClientTypesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllClientTypes()
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllClientTypesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllClientTypesRequest>));
            
            return await new ConvertedGetAllClientTypesOnHttpContext(
                new LoggedGetAllClientTypesRequest(logger,
                    new GetAllClientTypesUseCase(bus))).Ask(new GetAllClientTypesHttpQuery());
        }
    
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllClientStates")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllClientStatesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllClientStatesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllClientStates()
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllClientStates>)_services.GetService(typeof(ILogger<LoggedGetAllClientStates>));
            
            return await new ConvertedGetAllClientStatesOnHttpContext(
                new LoggedGetAllClientStates(logger,
                    new GetAllClientStatesUseCase(bus))).Ask(new GetAllClientStatesHttpQuery());
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
            var bus = (IBus) _services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedCreateClientRequest>) _services.GetService(typeof(ILogger<LoggedCreateClientRequest>));

            return await new ConvertedCreateClientOnHttpContext(
                new LoggedCreateClientRequest(logger,
                    new ValidatedCreateClientRequest(
                        new CreateClientUseCase(bus, auth)))).Ask(body);
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateClientsEmail")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsEmailSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsEmailErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateClientsEmail(
            [FromBody] UpdateClientsEmailHttpBody body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateClientsEmailRequest>) _services.GetService(typeof(ILogger<LoggedUpdateClientsEmailRequest>));

            
            return await new ConvertedUpdateClientsEmailOnHttpContext(
                new LoggedUpdateClientsEmailRequest(logger,  
                    new ValidatedUpdateClientsEmailRequest(
                        new UpdateClientsEmailUseCase(bus, auth)))).Ask(body);

        }

        [Authorize]
        [HttpPut]
        [Route("UpdateClientsName")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsNameErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateClientsName(
            [FromBody] UpdateClientsNameHttpBody body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateClientsNameRequest>) _services.GetService(typeof(ILogger<LoggedUpdateClientsNameRequest>));
            
           return await 
               new ConvertedUpdateClientsNameOnHttpContext( 
               new LoggedUpdateClientsNameRequest(logger,  
                    new ValidatedUpdateClientsNameRequest(
                        new UpdateClientsNameUseCase(bus, auth)))).Ask(body);

        }
        
        [Authorize]
        [HttpPut]
        [Route("UpdateClientsPhone")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsPhoneSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsPhoneErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateClientsPhone(
            [FromBody] UpdateClientsPhoneHttpBody body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateClientsPhoneRequest>) _services.GetService(typeof(ILogger<LoggedUpdateClientsPhoneRequest>));
            
            return await 
                new ConvertedUpdateClientsPhoneOnHttpContext(
                new LoggedUpdateClientsPhoneRequest(logger,  
                    new ValidatedUpdateClientsPhoneRequest(
                        new UpdateClientsPhoneUseCase(bus, auth)))).Ask(body);
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateClientsSelle")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsSelleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsSelleErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]  
        public async Task<IActionResult> UpdateClientsSelle(
            [FromBody] UpdateClientsSelleHttpBody body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateClientsSelleRequest>) _services.GetService(typeof(ILogger<LoggedUpdateClientsSelleRequest>));
            
            
            return await 
                new ConvertedUpdateClientsSelleOnHttpContext(
                new LoggedUpdateClientsSelleRequest(logger,  
                    new ValidatedUpdateClientsSelleRequest(
                        new UpdateClientsSelleUseCase(bus, auth)))).Ask(body);
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateClientsState")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsStateSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsStateErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]  
        public async Task<IActionResult> UpdateClientsState(
            [FromBody] UpdateClientsStateHttpBody body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateClientsStateRequest>) _services.GetService(typeof(ILogger<LoggedUpdateClientsStateRequest>));
            
            
            return await 
                new ConvertedUpdateClientsStateOnHttpContext( 
                new LoggedUpdateClientsStateRequest(logger,  
                    new ValidatedUpdateClientsStateRequest(
                        new UpdateClientsStateUseCase(bus, auth)))).Ask(body);

        }

        [Authorize]
        [HttpPut]
        [Route("UpdateClientsType")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateClientsTypeSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateClientsTypeErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateClientsType(
            [FromBody] UpdateClientsTypeHttpBody body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateClientsTypeRequest>) _services.GetService(typeof(ILogger<LoggedUpdateClientsTypeRequest>));
            
            return await
                new ConvertedUpdateClientsTypeOnHttpContext(
                    new LoggedUpdateClientsTypeRequest(logger, 
                        new ValidatedUpdateClientsTypeRequest(
                            new UpdateClientsTypeUseCase(bus, auth)))).Ask(body);
        }
    }
}