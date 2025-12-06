using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configurations
{
    public class GymUserConfiguration : IEntityTypeConfiguration<GymUser>
    {
        public void Configure(EntityTypeBuilder<GymUser> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(x => x.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);
            builder.Property(x => x.Phone)
                .HasColumnType("varchar")
                .HasMaxLength(11);
            builder.OwnsOne(x => x.Address, address =>
    {
                address.Property(a => a.BuildingNumber)
                    .HasColumnName("BuildingNumber");

                address.Property(a => a.City)
                    .HasColumnName("City")
                    .HasColumnType("varchar")
                    .HasMaxLength(30);

                address.Property(a => a.Street)
                    .HasColumnName("Street")
                    .HasColumnType("varchar")
                    .HasMaxLength(30);
            });

            builder.HasIndex(x=> x.Email).IsUnique();
            builder.HasIndex(x=> x.Phone).IsUnique();

            builder.ToTable(x=>
            {
                x.HasCheckConstraint("GymUser_Email_Check", "Email LIKE '_%@_%._%'");
                x.HasCheckConstraint("GymUser_Phone_Check", "Phone LIKE '01%' AND Phone NOT LIKE '%[^0-9]%'");
            });

        }
    }
}
