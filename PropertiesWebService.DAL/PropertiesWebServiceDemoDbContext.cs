using Microsoft.EntityFrameworkCore;

using PropertiesWebService.DAL.Entities;
using PropertiesWebService.DAL.Entities.Dictionaries;
using PropertiesWebService.DAL.Extensions;

namespace PropertiesWebService.DAL
{
    public class PropertiesWebServiceDemoDbContext : DbContext
    {
        public PropertiesWebServiceDemoDbContext(DbContextOptions<PropertiesWebServiceDemoDbContext> options)
            : base(options)
        {
        }

        public PropertiesWebServiceDemoDbContext()
        {
        }

        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Space> Spaces { get; set; }
        public virtual DbSet<DictPropertyType> DictPropertyTypes { get; set; }
        public virtual DbSet<DictSpaceType> DictSpaceTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();

            // Seed 15 properties with nice addresses and descriptions
            var propertySeed = new List<Property>
            {
                new() { Id = 1, Address = "1 Ocean View Drive", TypeId = 1, Price = 1200.0M, Description = "Modern apartment with stunning ocean views." },
                new() { Id = 2, Address = "22 Maple Street", TypeId = 2, Price = 950.0M, Description = "Cozy house in a quiet suburban neighborhood." },
                new() { Id = 3, Address = "5 City Center Plaza", TypeId = 3, Price = 1800.0M, Description = "Luxury condo in the heart of downtown." },
                new() { Id = 4, Address = "77 Mountain Retreat", TypeId = 4, Price = 2500.0M, Description = "Spacious villa nestled in the mountains." },
                new() { Id = 5, Address = "9 Lakeside Lane", TypeId = 5, Price = 1100.0M, Description = "Charming cottage by the lake." },
                new() { Id = 6, Address = "101 Riverside Avenue", TypeId = 1, Price = 1300.0M, Description = "Apartment with river views and modern amenities." },
                new() { Id = 7, Address = "33 Forest Path", TypeId = 2, Price = 1050.0M, Description = "House surrounded by beautiful forest scenery." },
                new() { Id = 8, Address = "8 Grand Boulevard", TypeId = 3, Price = 2000.0M, Description = "Condo with access to city attractions." },
                new() { Id = 9, Address = "12 Sunny Meadows", TypeId = 4, Price = 2300.0M, Description = "Villa with large garden and sunny rooms." },
                new() { Id = 10, Address = "44 Hilltop Crescent", TypeId = 5, Price = 1150.0M, Description = "Cottage with panoramic hilltop views." },
                new() { Id = 11, Address = "2 Seaside Promenade", TypeId = 1, Price = 1400.0M, Description = "Apartment steps from the beach." },
                new() { Id = 12, Address = "18 Country Lane", TypeId = 2, Price = 980.0M, Description = "House with spacious backyard in the countryside." },
                new() { Id = 13, Address = "6 Urban Heights", TypeId = 3, Price = 1750.0M, Description = "Condo with skyline views." },
                new() { Id = 14, Address = "55 Vineyard Road", TypeId = 4, Price = 2600.0M, Description = "Villa surrounded by vineyards." },
                new() { Id = 15, Address = "3 Harbor Point", TypeId = 5, Price = 1200.0M, Description = "Cottage with harbor access." }
            };

            modelBuilder.Entity<Property>(entity =>
            {
                entity.Navigation(e => e.Type).AutoInclude();

                entity.HasOne(x => x.Type)
                    .WithMany(x => x.Properties)
                    .HasForeignKey(x => x.TypeId)
                    .HasConstraintName("FK_Property_TypeId")
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasMany(x => x.Spaces)
                    .WithOne(x => x.Property)
                    .HasForeignKey(x => x.PropertyId)
                    .HasConstraintName("FK_Property_SpaceId")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasData(propertySeed);
            });

            var spaceSeed = new List<Space>();
            int spaceId = 1;
            var spaceTypes = new[] { 1, 2, 3, 4, 5, 6, 7  };

            var spaceDescriptions = new[]
            {
                "Spacious living room with natural light.",
                "Cozy master bedroom with ensuite bathroom.",
                "Modern kitchen with high-end appliances.",
                "Home office with inspiring views.",
                "Secure garage with extra storage.",
                "Beautiful garden with seasonal flowers.",
                "Balcony perfect for morning coffee.",
                "Guest bedroom with comfortable furnishings."
            };

            var random = new Random(15);

            for (int propertyId = 1; propertyId <= 15; propertyId++)
            {
                int spaceCount = random.Next(5, 9); 
                for (int j = 0; j < spaceCount; j++)
                {
                    int typeIdx = j % spaceTypes.Length;
                    spaceSeed.Add(new Space
                    {
                        Id = spaceId++,
                        Size = 20.0M + (j * 10) + propertyId,
                        Description = $"{spaceDescriptions[typeIdx]} (Property {propertyId})",
                        PropertyId = propertyId,
                        TypeId = spaceTypes[typeIdx]
                    });
                }
            }

            modelBuilder.Entity<Space>(entity =>
            {
                entity.Navigation(e => e.Type).AutoInclude();

                entity.HasOne(x => x.Property)
                    .WithMany(x => x.Spaces)
                    .HasForeignKey(x => x.PropertyId)
                    .HasConstraintName("FK_Space_PropertyId")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Type)
                    .WithMany(x => x.Spaces)
                    .HasForeignKey(x => x.TypeId)
                    .HasConstraintName("FK_Space_TypeId")
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasData(spaceSeed);
            });

            modelBuilder.Entity<DictPropertyType>(entity =>
            {
                entity.HasMany(x => x.Properties)
                    .WithOne(x => x.Type)
                    .HasForeignKey(x => x.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasData(
                    new DictPropertyType { Id = 1, Name = "Apartment", IsActive = true },
                    new DictPropertyType { Id = 2, Name = "House", IsActive = true },
                    new DictPropertyType { Id = 3, Name = "Condo", IsActive = true },
                    new DictPropertyType { Id = 4, Name = "Villa", IsActive = true },
                    new DictPropertyType { Id = 5, Name = "Cottage", IsActive = true }
                );
            });

            modelBuilder.Entity<DictSpaceType>(entity =>
            {
                entity.HasMany(x => x.Spaces)
                    .WithOne(x => x.Type)
                    .HasForeignKey(x => x.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasData(
                    new DictSpaceType { Id = 1, Name = "Living Room", IsActive = true },
                    new DictSpaceType { Id = 2, Name = "Bedroom", IsActive = true },
                    new DictSpaceType { Id = 3, Name = "Kitchen", IsActive = true },
                    new DictSpaceType { Id = 4, Name = "Office room", IsActive = true },
                    new DictSpaceType { Id = 5, Name = "Garage", IsActive = true },
                    new DictSpaceType { Id = 6, Name = "Garden", IsActive = true },
                    new DictSpaceType { Id = 7, Name = "Balcony", IsActive = true }
                );
            });
        }
    }
}
