using armavir.transport.dal.Entities;
using dal.abstractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace armavir.transport.dal.EntitiesConfigurations;

public class TransportStopsConfiguration : IEntityTypeConfiguration<TransportStops>
{
    public void Configure(EntityTypeBuilder<TransportStops> builder)
    {
        builder.ToTable("TransportStops");
            
        builder.HasKey(ts => new { ts.TransportId, ts.StopId, ts.Direction });
        
        builder.Property(ts => ts.Direction)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);
                
        builder.Property(ts => ts.StopOrder)
            .IsRequired();
                
        builder.Property(ts => ts.TransportId)
            .IsRequired();
                
        builder.Property(ts => ts.StopId)
            .IsRequired();
            
        builder.HasOne(ts => ts.Transport)
            .WithMany(t => t.Stop)
            .HasForeignKey(ts => ts.TransportId)
            .OnDelete(DeleteBehavior.Cascade);
                
        builder.HasOne(ts => ts.Stop)
            .WithMany(s => s.TransportStops)
            .HasForeignKey(ts => ts.StopId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasIndex(ts => ts.TransportId);
        builder.HasIndex(ts => ts.StopId);
        builder.HasIndex(ts => new { ts.TransportId, ts.Direction });
        builder.HasIndex(ts => new { ts.StopId, ts.Direction });
    }
}
