using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
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
            // ToDo Hey Maslyonok
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllClientsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllClientsRequest>));

            var request = new GetAllClientsRequestContract()
            {
                InstanceId = query.InstanceId,
                ClientStateId = query.ClientStateId,
                ClientTypeId = query.ClientTypeId
            };
            
            // ToDo Hey Maslyonok
            // Send request to pipeline
            var result = 
                await new ValidatedGetAllClientsRequest(
                    new LoggedGetAllClientsRequest(logger, 
                        new GetAllClientsRequest(bus))
                ).Ask(request);

            // ToDo Hey Maslyonok
            // Check response of get all clients request
            return result switch
            {
                IGetAllClientsSuccessResultContract success => Ok(success),
                IGetAllClientsErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new GetClientByIdRequestContract()
            {
                UnitId = query.UnitId
            };
            
            var result = 
                await new ValidatedGetClientByIdRequest(
                    new LoggedGetClientByIdRequest(logger, 
                        new GetClientByIdRequest(bus))
                ).Ask(request);

            return result switch
            {
                IGetClientByIdSuccessResultContract success => Ok(success),
                IGetClientByIdErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new GetAllClientTypesRequestContract();
            
            var result = 
                await new LoggedGetAllClientTypesRequest(logger, 
                    new GetAllClientTypesRequest(bus)
                    ).Ask(request);

            return result switch
            {
                IGetAllClientTypesSuccessResultContract success => Ok(success),
                IGetAllClientTypesErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new GetAllClientStatesRequestContract();
            
            var result = 
                await new LoggedGetAllClientStates(logger, 
                    new GetAllClientStatesRequest(bus)
                    ).Ask(request);
            
            return result switch
            {
                IGetAllClientStatesSuccessResultContract success => Ok(success),
                IGetAllClientStatesErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new CreateClientRequestContract()
            {
                Name = body.Name,
                Email = body.Email,
                Phone = body.Phone,
                Selle = body.Selle,
                InstanceId = body.InstanceId,
                ClientStateId = body.ClientStateId,
                ClientTypeId = body.ClientTypeId,
                NewId = body.NewId
            };
            
            var result = 
                await new LoggedCreateClientRequest(logger,  
                    new ValidatedCreateClientRequest(
                        new CreateClientRequest(bus, auth)
                        )
                    ).Ask(request);

            return result switch
            {
                ICreateClientSuccessResultContract success => Ok(success),
                ICreateClientErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new UpdateClientsEmailRequestContract()
            {
                ClientId = body.ClientId,
                Email = body.Email
            };
            
            var result = 
                await new LoggedUpdateClientsEmailRequest(logger,  
                    new ValidatedUpdateClientsEmailRequest(
                        new UpdateClientsEmailRequest(bus, auth)
                    )
                ).Ask(request);

            return result switch
            {
                IUpdateClientsEmailSuccessResultContract success => Ok(success),
                IUpdateClientsEmailErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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
            
            var request = new UpdateClientsNameRequestContract()
            {
                ClientId = body.ClientId,
                Name = body.Name
            };
            
            var result = 
                await new LoggedUpdateClientsNameRequest(logger,  
                    new ValidatedUpdateClientsNameRequest(
                        new UpdateClientsNameRequest(bus, auth)
                    )
                ).Ask(request);

            return result switch
            {
                IUpdateClientsNameSuccessResultContract success => Ok(success),
                IUpdateClientsNameErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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
            
            var request = new UpdateClientsPhoneRequestContract()
            {
                ClientId = body.ClientId,
                Phone = body.Phone
            };
            
            var result = 
                await new LoggedUpdateClientsPhoneRequest(logger,  
                    new ValidatedUpdateClientsPhoneRequest(
                        new UpdateClientsPhoneRequest(bus, auth)
                    )
                ).Ask(request);

            return result switch
            {
                IUpdateClientsPhoneSuccessResultContract success => Ok(success),
                IUpdateClientsPhoneErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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
            
            var request = new UpdateClientsSelleRequestContract()
            {
                ClientId = body.ClientId,
                Selle = body.Selle
            };
            
            var result = 
                await new LoggedUpdateClientsSelleRequest(logger,  
                    new ValidatedUpdateClientsSelleRequest(
                        new UpdateClientsSelleRequest(bus, auth)
                    )
                ).Ask(request);

            return result switch
            {
                IUpdateClientsSelleSuccessResultContract success => Ok(success),
                IUpdateClientsSelleErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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
            
            var request = new UpdateClientsStateRequestContract()
            {
                ClientId = body.ClientId,
                StateId = body.StateId
            };
            
            var result = 
                await new LoggedUpdateClientsStateRequest(logger,  
                    new ValidatedUpdateClientsStateRequest(
                        new UpdateClientsStateRequest(bus, auth)
                    )
                ).Ask(request);

            return result switch
            {
                IUpdateClientsStateSuccessResultContract success => Ok(success),
                IUpdateClientsStateErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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
            
            var request = new UpdateClientsTypeRequestContract()
            {
                ClientId = body.ClientId,
                TypeId = body.TypeId
            };

            var result = 
                await new LoggedUpdateClientsTypeRequest(logger,  
                    new ValidatedUpdateClientsTypeRequest(
                        new UpdateClientsTypeRequest(bus, auth)
                    )
                ).Ask(request);

            return result switch
            {
                IUpdateClientsTypeSuccessResultContract success => Ok(success),
                IUpdateClientsTypeErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
    }
}