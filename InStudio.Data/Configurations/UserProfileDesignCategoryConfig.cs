using InStudio.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Data.Configurations
{
    public class UserProfileDesignCategoryConfig : IEntityTypeConfiguration<UserProfileDesignCategory>
    {
        public void Configure(EntityTypeBuilder<UserProfileDesignCategory> entity)
        {
            entity.ToTable("UserProfileDesignCategory");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.DesignCategory).WithMany(p => p.UserProfileDesignCategories)
                .HasForeignKey(d => d.DesignCategoryId)
                .HasConstraintName("FK_UserProfileDesignCategory_DesignCategory");
            entity.HasOne(d => d.UserProfile)
                .WithMany(p => p.UserProfileDesignCategory)
                .HasForeignKey(d => d.UserProfileId)
                .HasConstraintName("FK_UserProfileDesignCategory_UserProfile");
        }
    }
}
