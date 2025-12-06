using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configurations
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Plan> builder)
        {
            builder.Property(x => x.Name)
               .HasColumnType("varchar")
               .HasMaxLength(50);
            builder.Property(x => x.Description)
                .HasColumnType("varchar")
                .HasMaxLength(200);
            builder.Property(x => x.Price)
                .HasPrecision(10, 2);

            builder.ToTable(x =>
            {
                x.HasCheckConstraint("Plan_Duration_Check", "DurationDays BETWEEN 1 AND 365");
            });
        }
    }
}
