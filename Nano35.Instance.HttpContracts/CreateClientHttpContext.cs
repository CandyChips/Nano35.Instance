using System;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.HttpContracts
{
    public class CreateClientHttpContext : 
        ICreateClientRequestContract
    {
        public Guid NewId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double Salle { get; set; }
        public Guid ClientTypeId { get; set; }
        public Guid ClientStateId { get; set; }
        public Guid UserId { get; set; }
        public Guid InstanceId { get; set; }
    }
}