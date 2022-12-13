using EntityRestaurant = Restaurant.Core.Entities.Restaurant;

namespace Restaurant.Infrastructure.Persistence.Mappings
{
    public class RestaurantMapping : IEntityTypeConfiguration<EntityRestaurant>
    {
        public void Configure(EntityTypeBuilder<EntityRestaurant> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(p => p.Id).IsRequired();
            
            builder.Property(p => p.CreatedAt).IsRequired();

            builder.Property(p => p.UpdatedAt).IsRequired();

            builder.Property(p => p.Name).IsRequired();

            builder.HasMany(p => p.DaysOfWork)
                   .WithOne(d => d.Restaurant);

            builder.HasMany(p => p.Contacts)
                   .WithOne(d => d.Restaurant);

            builder.OwnsOne(p => p.Address)
                .Property(a => a.FullAddress)
                .HasColumnName("AddressFullAddress");

            builder.OwnsOne(p => p.Address)
                .Property(a => a.PostalCode)
                .HasColumnName("AddressPostalCode");

            builder.OwnsOne(p => p.Address)
               .Property(a => a.Number)
               .HasColumnName("AddressNumber");

            builder.OwnsOne(p => p.Address)
               .Property(a => a.State)
               .HasColumnName("AddressState");

            builder.OwnsOne(p => p.Address)
               .Property(a => a.Street)
               .HasColumnName("AddressStreet");

            builder.OwnsOne(p => p.Address)
              .Property(a => a.Neighborhood)
              .HasColumnName("AddressNeighborhood");

            builder.OwnsOne(p => p.Address)
               .Property(a => a.Country)
               .HasColumnName("AddressCountry");

            builder.OwnsOne(p => p.Address)
               .Property(a => a.Zone)
               .HasColumnName("AddressZone");

            builder.OwnsOne(p => p.Address)
              .Property(a => a.City)
              .HasColumnName("AddressCity");

            builder.ToTable("Restaurants");
        }
    }
}
