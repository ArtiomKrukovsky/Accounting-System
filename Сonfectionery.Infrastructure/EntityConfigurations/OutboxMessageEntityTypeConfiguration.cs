using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Сonfectionery.Infrastructure.Processing.Outbox;

namespace Сonfectionery.Infrastructure.EntityConfigurations
{
    public class OutboxMessageEntityTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("OutboxMessage");

            builder.HasKey(b => b.Id).HasName("PK_OutboxMessage");
            builder.Property(b => b.Id).ValueGeneratedNever();

            builder
                .Property(x => x.OccurredOn)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(x => x.ProcessedAt)
                .HasColumnType("datetime");

            builder
                .Property(x => x.Type)
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(x => x.Payload)
                .HasMaxLength(int.MaxValue)
                .IsRequired();
        }
    }
}