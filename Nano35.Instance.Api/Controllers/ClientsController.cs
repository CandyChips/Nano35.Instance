using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;
using Nano35.Instance.Api.Requests.CreateClient;
using Nano35.Instance.Api.Requests.GetAllClients;
using Nano35.Instance.Api.Requests.GetAllClientStates;
using Nano35.Instance.Api.Requests.GetAllClientTypes;
using Nano35.Instance.HttpContracts;

namespace Nano35.Instance.Api.Controllers
{
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
        [HttpGet]
        [Route("GetAllClients")]
        public async Task<IActionResult> GetAllClients(
            [FromQuery] GetAllClientHttpContext query)
        {
            // ToDo Hey Maslyonok
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllClientsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllClientsRequest>));
            
            // ToDo Hey Maslyonok
            // Send request to pipeline
            var result = 
                await new ValidatedGetAllClientsRequest(
                    new LoggedGetAllClientsRequest(logger, 
                        new GetAllClientsRequest(bus))
                    ).Ask(query);

            // ToDo Hey Maslyonok
            // Check response of get all clients request
            return result switch
            {
                IGetAllClientsSuccessResultContract success => Ok(success.Data),
                IGetAllClientsErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    
        [HttpGet]
        [Route("GetAllClientTypes")]
        public async Task<IActionResult> GetAllClientTypes()
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllClientTypesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllClientTypesRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedGetAllClientTypesRequest(logger, 
                    new GetAllClientTypesRequest(bus)
                    ).Ask(new GetAllClientTypesHttpContext());

            // Check response of get all client types request
            return result switch
            {
                IGetAllClientTypesSuccessResultContract success => Ok(success.Data),
                IGetAllClientTypesErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    
        [HttpGet]
        [Route("GetAllClientStates")]
        public async Task<IActionResult> GetAllClientStates()
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllClientStates>)_services.GetService(typeof(ILogger<LoggedGetAllClientStates>));
            
            // Send request to pipeline
            var result = 
                await new LoggedGetAllClientStates(logger, 
                    new GetAllClientStatesRequest(bus)
                    ).Ask(new GetAllClientStatesHttpContext());
            
            // Check response of get all client states request
            return result switch
            {
                IGetAllClientStatesSuccessResultContract success => Ok(success.Data),
                IGetAllClientStatesErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPost]
        [Route("CreateClient")]
        public async Task<IActionResult> CreateClient(
            [FromBody]CreateClientHttpContext body)
        {
            // Setup configuration of pipeline
            var bus = (IBus) _services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedCreateClientRequest>) _services.GetService(typeof(ILogger<LoggedCreateClientRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedCreateClientRequest(logger,  
                    new ValidatedCreateClientRequest(
                        new CreateClientRequest(bus, auth)
                        )
                    ).Ask(body);

            // Check response of create client request
            return result switch
            {
                ICreateClientSuccessResultContract => Ok(),
                ICreateClientErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}