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
    internal class RateConfig : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.HasMany(t => t.trainees)
                .WithOne(r => r.rate)
                .HasForeignKey(t=> t.TraineeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.mentors)
                .WithOne(r => r.rate)
                .HasForeignKey(t=> t.MentorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
