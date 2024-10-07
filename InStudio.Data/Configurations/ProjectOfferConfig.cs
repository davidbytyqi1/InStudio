using InStudio.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Data.Configurations
{
    public class ProjectOfferConfig : IEntityTypeConfiguration<ProjectOffer>
    {
        public void Configure(EntityTypeBuilder<ProjectOffer> entity)
        {
            entity.HasIndex(e => e.ProjectId, "IX_ProjectOffers_ProjectId");

            entity.Property(e => e.Comments)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CoverLetter)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Feedback)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectOffers)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_ProjectOffers_Project");
        }
    }
}
