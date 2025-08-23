using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence.Data.Configs
{
    internal class PhaseConfig : IEntityTypeConfiguration<Phase>
    {
        public void Configure(EntityTypeBuilder<Phase> builder)
        {
            builder
                .HasOne(r => r.RoadMap)
                .WithMany(ph => ph.Phases)
                .HasForeignKey(r => r.RoadMapId);
            builder
                .HasMany(sk=> sk.Skills_Gained)
                .WithMany(p=> p.Phase)
                .UsingEntity(j=> j.ToTable("Phase_GainedSkills"));
            builder
                .HasMany(pr=> pr.Prerequisites)
                .WithMany(p=> p.Phase)
                .UsingEntity(j=> j.ToTable("Phase_Prerequisites"));
            /* builder
                 .Property(sk=>sk.Skills_Gained)
                 .HasColumnType("nvarchar(max)")
                 .HasConversion(
                     v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                     v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)
                 );*/
        }
    }
}
