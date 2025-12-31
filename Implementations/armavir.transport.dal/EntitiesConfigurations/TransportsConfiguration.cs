using armavir.transport.dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace armavir.transport.dal.EntitiesConfigurations;

public class TransportsConfiguration : IEntityTypeConfiguration<Transports>
{
    public void Configure(EntityTypeBuilder<Transports> builder)
    {
        builder.ToTable("Transports");
            
        builder.HasKey(t => t.Id);
            
        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd();
                
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(200);
                
        builder.Property(t => t.Number)
            .IsRequired()
            .HasMaxLength(20);
                
        builder.Property(t => t.Company)
            .IsRequired()
            .HasMaxLength(100);
                
        builder.Property(t => t.MaxCount)
            .IsRequired();
                
        builder.Property(t => t.ShortRoute)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
                
        builder.Property(t => t.LongRoute)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
            
        builder.HasMany(t => t.TransportStops)
            .WithOne(ts => ts.Transport)
            .HasForeignKey(ts => ts.TransportId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
