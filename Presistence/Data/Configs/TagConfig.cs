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
    public class TagConfig : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder
            .HasOne(t => t.Mentor)
            .WithMany(m => m.Tags)
            .HasForeignKey(t => t.MentorId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
