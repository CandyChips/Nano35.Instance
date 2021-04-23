using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public enum MessengerTypes
    {
        Whatsapp = 0,
        Telegram = 1
    }
    public class Messenger
    {
        [Key]
        public string Id { get; init; }
        [Required]
        public string UserId { get; init; }
        [Required]
        public MessengerTypes Type { get; init; }

        public class Configuration : IEntityTypeConfiguration<Messenger>
        {
            public void Configure(EntityTypeBuilder<Messenger> builder)
            {
                builder.ToTable("Messengers");
                builder.HasKey(e => new { e.Id, e.UserId });
                builder.Property(b => b.Type).IsRequired();
            }
        }
    }
}