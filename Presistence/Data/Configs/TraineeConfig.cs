using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configs
{
    public class TraineeConfig : IEntityTypeConfiguration<Trainee>
    {
        public void Configure(EntityTypeBuilder<Trainee> builder)
        {
            builder.HasOne(t => t.RoadMap)
                   .WithOne(rm => rm.Trainee)
                   .HasForeignKey<RoadMap>(RoadMap => RoadMap.TraineeId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
