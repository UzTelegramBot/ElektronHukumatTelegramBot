using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Data
{
    public class DefaultRegion : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasData(
                new Region
                {
                    Id = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
                    RegionIndex = 17,
                    UzName = "O‘zbekiston Respublikasi",
                    RuName = "O‘zbekiston Respublikasi",
                    EngName = "O‘zbekiston Respublikasi"          
                }
                );
        }
    }
}
