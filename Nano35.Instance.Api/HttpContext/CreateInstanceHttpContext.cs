using System;
using System.Text.Json.Serialization;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.HttpContext
{
    public class CreateInstanceHttpContext : 
        ICreateInstanceRequestContract
    {
        public Guid NewId { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public string Info { get; set; }
        public string Phone { get; set; }
        public Guid TypeId { get; set; }
        public Guid RegionId { get; set; }
    }
    
    public class CreateCashOutputHttpContext :
        ICreateCashOutputRequestContract
    {
        public Guid NewId { get; set; }
        public Guid UnitId { get; set; }
        public string Description { get; set; }
        public Guid InstanceId { get; set; }
        [JsonIgnore]
        public Guid UpdaterId { get; set; }
        public double Cash { get; set; }
    }
    
    public class CreateCashInputHttpContext :
        ICreateCashInputRequestContract
    {
        public Guid NewId { get; set; }
        public Guid UnitId { get; set; }
        public string Description { get; set; }
        public Guid InstanceId { get; set; }
        [JsonIgnore]
        public Guid UpdaterId { get; set; }
        public double Cash { get; set; }
    }
}