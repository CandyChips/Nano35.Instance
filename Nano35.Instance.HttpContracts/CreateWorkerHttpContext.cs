using System;
using System.Text.Json.Serialization;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.HttpContracts
{
    public class CreateWorkerHttpContext : 
        ICreateWorkerRequestContract
    {
        public Guid NewId { get; set; }
        public Guid InstanceId { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}