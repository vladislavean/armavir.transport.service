using armavir.transport.dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace armavir.transport.dal.EntitiesConfigurations;

public class StopsConfiguration : IEntityTypeConfiguration<Stops>
{
    public void Configure(EntityTypeBuilder<Stops> builder)
    {
        builder.ToTable("Stops");
        
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd();
                
        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.HasMany(s => s.TransportStops)
            .WithOne(ts => ts.Stop)
            .HasForeignKey(ts => ts.StopId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
