using System;
using System.Text.Json.Serialization;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.HttpContext
{
    public class CreateClientHttpContext : 
        ICreateClientRequestContract
    {
        public Guid NewId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double Selle { get; set; }
        public Guid ClientTypeId { get; set; }
        public Guid ClientStateId { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
        public Guid InstanceId { get; set; }
    }
}