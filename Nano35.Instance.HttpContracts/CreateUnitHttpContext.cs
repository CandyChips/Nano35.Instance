using System;
using System.Text.Json.Serialization;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.HttpContracts
{
    public class CreateUnitHttpContext : 
        ICreateUnitRequestContract
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string WorkingFormat { get; set; }
        public string Phone { get; set; }
        public Guid UnitTypeId { get; set; }
        [JsonIgnore]
        public Guid CreatorId { get; set; }
        public Guid InstanceId { get; set; }
    }
}