using EntityRestaurant = Restaurant.Core.Entities.Restaurant;

namespace Restaurant.Infrastructure.Persistence.Mappings
{
    public class RestaurantMapping : IEntityTypeConfiguration<EntityRestaurant>
    {
        public void Configure(EntityTypeBuilder<EntityRestaurant> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id).IsRequired();
            
            builder.Property(r => r.CreatedAt).IsRequired();

            builder.Property(r => r.UpdatedAt).IsRequired();

            builder.Property(r => r.Name).IsRequired();

            builder.HasMany(r => r.DaysOfWork)
                   .WithOne(d => d.Restaurant)
                   .HasForeignKey(d => d.RestaurantId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Contacts)
                   .WithOne(c => c.Restaurant)
                   .HasForeignKey(c => c.RestaurantId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(r => r.Address)
                .Property(a => a.FullAddress)
                .HasColumnName("AddressFullAddress");

            builder.OwnsOne(r => r.Address)
                .Property(a => a.PostalCode)
                .HasColumnName("AddressPostalCode");

            builder.OwnsOne(r => r.Address)
               .Property(a => a.Number)
               .HasColumnName("AddressNumber");

            builder.OwnsOne(r => r.Address)
               .Property(a => a.State)
               .HasColumnName("AddressState");

            builder.OwnsOne(r => r.Address)
               .Property(a => a.Street)
               .HasColumnName("AddressStreet");

            builder.OwnsOne(r => r.Address)
              .Property(a => a.Neighborhood)
              .HasColumnName("AddressNeighborhood");

            builder.OwnsOne(r => r.Address)
               .Property(a => a.Country)
               .HasColumnName("AddressCountry");

            builder.OwnsOne(r => r.Address)
               .Property(a => a.Zone)
               .HasColumnName("AddressZone");

            builder.OwnsOne(r => r.Address)
              .Property(a => a.City)
              .HasColumnName("AddressCity");

            builder.ToTable("Restaurants");
        }
    }
}
