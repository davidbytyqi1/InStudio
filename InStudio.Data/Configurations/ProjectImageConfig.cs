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
    public class ProjectImageConfig : IEntityTypeConfiguration<ProjectImage>
    {
        public void Configure(EntityTypeBuilder<ProjectImage> entity)
        {
            entity.HasIndex(e => e.ProjectId, "IX_ProjectImages_ProjectId");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectImages)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_ProjectImages_Project");
        }
    }
}
