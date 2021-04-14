using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;
using Nano35.Instance.Processor.Configurations;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.UseCases;
using Nano35.Instance.Processor.UseCases.CreateCashInput;
using Nano35.Instance.Processor.UseCases.CreateClient;
using Nano35.Instance.Processor.UseCases.CreateInstance;
using Xunit;
using Xunit.Abstractions;

namespace Nano35.Instance.Tests
{
    public class UtilityTests
    {
        [Fact]
        public void PhoneConverterTest()
        {            
            // Arrange
            var phonesInit = new List<string>
            {
                "+79115339541", 
                "89517380259", 
                "89517380259123", 
                "8951738025", 
            };

            var phonesNormal = new List<string>
            {
                "79115339541", 
                "89517380259", 
                null,
                null
            };

            // Act
            var result = phonesInit.Select(PhoneConverter.RuPhoneConverter).ToList();
 
            // Assert
            Assert.Equal(result, phonesNormal);
        }
    }
    
    /// <summary>
    /// https://stackoverflow.com/questions/38890269/how-to-isolate-ef-inmemory-database-per-xunit-test
    /// </summary>
    public class CreateTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private ApplicationContext _context;

        public CreateTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private void Initialize()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer("server=192.168.100.120; Initial Catalog=Nano35.InstanceTest.DB; User id=sa; Password=Cerber666;");
            _context = new ApplicationContext(optionsBuilder.Options);
            
            //add incert support tables
        }
        
        [Fact]
        public async void CreateCashOperationTest1()
        {            
            // Arrange
            Initialize();
            
            var types = new List<CashOperation>()
            {
                new CashOperation()
                {
                    Id = new Guid(), 
                    UnitId = new Guid(), 
                    InstanceId = new Guid(), 
                    WorkerId = new Guid(),
                    Description = "123", 
                    Cash = 100.25
                },
            };
            
            var message = new CreateCashInputRequestContract()
            {
                NewId = Guid.Parse("bb5f6b47-86dc-4038-08b1-08d8d1d70d46"),
                UnitId = Guid.Parse("69329e05-c72a-e3c6-4c8a-232348b480fc"),
                Description = "123",
                InstanceId = Guid.Parse("86f7e707-3e34-09b7-a27d-d2016a8b8436"),
                WorkerId = Guid.Parse("3fa85f64-eeee-4562-b3fc-2c963f66afa6"),
                Cash = 100.25
            };

            var source = new CancellationTokenSource();
            var cancellationToken = source.Token;
            var context = Mock.Of<ConsumeContext<ICreateCashInputRequestContract>>(_ =>
                _.CancellationToken == cancellationToken);
            var logger = Mock.Of<ILogger<ICreateCashInputRequestContract>>();
            
            // Act
            var result =
                await new LoggedPipeNode<ICreateCashInputRequestContract, ICreateCashInputResultContract>(logger,
                        new TransactedPipeNode<ICreateCashInputRequestContract, ICreateCashInputResultContract>(_context,
                            new CreateCashInputUseCase(_context)))
                    .Ask(message, context.CancellationToken);
 
            // Assert
            Assert.Equal(types, _context.CashOperations.ToList());
        }
        [Fact]
        public async void CreateClientTest1()
        {            
            // Arrange
            Initialize();
            
            var types = new List<Client>()
            {   //expected values
                new Client()
                {
                    Id = Guid.Parse("bb5f6b47-86dc-4038-08b1-08d8d1d70d46"),
                    Name = "Alex",
                    Email = "Alex@g.c",
                    Phone = "88005553535",
                    Salle = 0,
                    Deleted = false,
                    ClientTypeId = Guid.Parse("5e9ffc4f-4a52-4c49-0cf2-08d8d1d70d30"),
                    ClientStateId = Guid.Parse("4bb66deb-8869-4c04-5045-08d8d1d70d42"),
                    InstanceId = Guid.Parse("86f7e707-3e34-09b7-a27d-d2016a8b8436"),
                    WorkerId = Guid.Parse("3fa85f64-eeee-4562-b3fc-2c963f66afa6")
                },
            };
            
            var message = new CreateClientRequestContract()
            {   //input values
                NewId = Guid.Parse("bb5f6b47-86dc-4038-08b1-08d8d1d70d46"),
                Name = "Alex",
                Email = "Alex@g.c",
                Phone = "88005553535",
                Selle = 0,
                ClientTypeId = Guid.Parse("5e9ffc4f-4a52-4c49-0cf2-08d8d1d70d30"),
                ClientStateId = Guid.Parse("4bb66deb-8869-4c04-5045-08d8d1d70d42"),
                InstanceId = Guid.Parse("86f7e707-3e34-09b7-a27d-d2016a8b8436"),
                UserId = Guid.Parse("3fa85f64-eeee-4562-b3fc-2c963f66afa6")
            };

            var source = new CancellationTokenSource();
            var cancellationToken = source.Token;
            var context = Mock.Of<ConsumeContext<ICreateClientRequestContract>>(_ =>
                _.CancellationToken == cancellationToken);
            var logger = Mock.Of<ILogger<ICreateClientRequestContract>>();
            
            // Act
            var result = 
                await new LoggedPipeNode<ICreateClientRequestContract, ICreateClientResultContract>(logger,
                    new TransactedPipeNode<ICreateClientRequestContract, ICreateClientResultContract>(_context,
                        new CreateClientUseCase(_context))).Ask(message, context.CancellationToken);
 
            // Assert
            Assert.Equal(types, _context.Clients.ToList());
        }
        [Fact]
        public async void CreateInstanceTest1()
        {            
            // Arrange
            Initialize();

            var Id = Guid.NewGuid();
            
            var types = new List<Processor.Models.Instance>()
            {
                new Processor.Models.Instance()
                {
                    Id = Id,
                    CompanyInfo = "123123",
                    Deleted = false,
                    InstanceTypeId = Guid.Parse("7a0ebdcd-b945-4d40-2f05-08d8d1d70d44"),
                    InstanceType = new InstanceType(),
                    OrgEmail = "crazy@killer-b.ru",
                    OrgName = "Нано сервис",
                    OrgRealName = "ООО Нано",
                    RegionId = Guid.Parse("76135956-9436-46fc-e6eb-08d8d1d70d47"),
                    Region = new Region(),
                },
            };
            
            var message = new CreateInstanceRequestContract()
            {
                NewId = Id,
                Name = "Нано сервис",
                RealName = "ООО Нано",
                Email = "crazy@killer-b.ru",
                Info = "123123",
                Phone = "88005553535",
                TypeId = Guid.Parse("7a0ebdcd-b945-4d40-2f05-08d8d1d70d44"),
                RegionId = Guid.Parse("76135956-9436-46fc-e6eb-08d8d1d70d47")
            };

            var source = new CancellationTokenSource();
            var cancellationToken = source.Token;
            var context = Mock.Of<ConsumeContext<ICreateInstanceRequestContract>>(_ =>
                _.CancellationToken == cancellationToken);
            var logger = Mock.Of<ILogger<ICreateInstanceRequestContract>>();
            
            // Act
            var result = 
                await new LoggedPipeNode<ICreateInstanceRequestContract, ICreateInstanceResultContract>(logger,
                    new TransactedPipeNode<ICreateInstanceRequestContract, ICreateInstanceResultContract>(_context,
                        new CreateInstanceUseCase(_context))).Ask(message, context.CancellationToken);

            // Assert
            Assert.Equal(types, _context.Instances.ToList());

            _context.Database.EnsureDeleted();
        }
    }

    public class GetTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private ApplicationContext _context;

        public GetTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private void Initialize()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer("server=192.168.100.120; Initial Catalog=Nano35.InstanceTest.DB; User id=sa; Password=Cerber666;");
            _context = new ApplicationContext(optionsBuilder.Options);
        }

        [Fact]
        public async void GetClients()
        {
            
        }
    }
}