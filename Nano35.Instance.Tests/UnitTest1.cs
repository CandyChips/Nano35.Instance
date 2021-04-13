using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nano35.Instance.Api.Helpers;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Xunit;

namespace Nano35.Instance.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
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
    public class UnitTest2
    {
        private ApplicationContext _context;
        private void Initialize()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseInMemoryDatabase("test");
            _context = new ApplicationContext(optionsBuilder.Options);
        }
        
        [Fact]
        public void Test2()
        {            
            // Arrange
            Initialize();
            var types = new List<ClientType>()
            {
                new ClientType() {Id = new Guid(), Name = "Type1"},
                new ClientType() {Id = new Guid(), Name = "Type2"}
            };

            // Act
            _context.ClientTypes.AddRange(types);
            _context.SaveChanges();
 
            // Assert
            Assert.Equal(_context.ClientTypes.ToList(), types);
        }
    }
}