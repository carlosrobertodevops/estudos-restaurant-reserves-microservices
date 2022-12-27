namespace Restaurant.Infrastructure.Persistence.Mappings
{
    public sealed class ContactMapping : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).IsRequired();

            builder.Property(c => c.CreatedAt).IsRequired();

            builder.Property(c => c.UpdatedAt).IsRequired();

            builder.Property(c => c.Email);

            builder.Property(c => c.PhoneNumber);

            builder.HasOne(c => c.Restaurant)
                   .WithMany(p => p.Contacts)
                   .HasForeignKey(c => c.RestaurantId);

            builder.ToTable("Contacts");
        }
    }
}
