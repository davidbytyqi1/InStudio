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
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> entity)
        {

            entity.ToTable("Project");

            entity.HasIndex(e => e.DesignCategoryId, "IX_Project_DesignCategoryId");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.DesignerFeedback).IsUnicode(false);
            entity.Property(e => e.EmployeeFeedback).IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.DesignCategory).WithMany(p => p.Projects)
                .HasForeignKey(d => d.DesignCategoryId)
                .HasConstraintName("FK_Project_DesignCategory");

            entity.HasOne(d => d.UserProfile).WithMany(p => p.Projects)
                .HasForeignKey(d => d.UserProfileId)
                .HasConstraintName("FK_Project_UserProfile1");
        }
    }
}
