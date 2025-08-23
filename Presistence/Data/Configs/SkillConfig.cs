using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configs
{
    public class SkillConfig : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder
            .HasOne(s => s.Mentor)
            .WithMany(m => m.Skills)
            .HasForeignKey(s => s.MentorId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
