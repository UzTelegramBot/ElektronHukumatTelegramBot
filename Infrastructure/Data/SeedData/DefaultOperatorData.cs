
using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Data
{
    public class DefaultOperatorData : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasData(new Manager
            {
                Id = Guid.NewGuid(),
                FirstName = "Nodirxon",
                LastName = "Abdumurotov",
                Login = "Nodirkhan",
                Password = "12345",
                Email = "abdumurodovnodirxon@gmail.com",
                PhoneNumber = "+998900255013",
                Role = RoleManager.Admin,
                RegionId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
                OrganizationId = null
            });
        }
    }
}
