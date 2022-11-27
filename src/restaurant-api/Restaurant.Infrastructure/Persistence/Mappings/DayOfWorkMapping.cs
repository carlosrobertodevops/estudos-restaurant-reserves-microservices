namespace Restaurant.Infrastructure.Persistence.Mappings
{
    public class DayOfWorkMapping : IEntityTypeConfiguration<DayOfWork>
    {
        public void Configure(EntityTypeBuilder<DayOfWork> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id).IsRequired();

            builder.Property(d => d.CreatedAt).IsRequired();
            
            builder.Property(d => d.UpdatedAt).IsRequired();

            builder.Property(d => d.OpensAt).IsRequired();
            
            builder.Property(d => d.ClosesAt).IsRequired();

            builder.Property(d => d.DayOfWeek)
                   .HasMaxLength(50)
                   .HasConversion(d => Enum.GetName(d),
                                  d => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), d));

            builder.HasOne(d => d.Restaurant)
                   .WithMany(p => p.DaysOfWork)
                   .HasForeignKey(d => d.RestaurantId);

            builder.ToTable("DaysOfWork");
        }
    }
}
