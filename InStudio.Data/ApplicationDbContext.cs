using InStudio.Data.Configurations;
using InStudio.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<DesignCategory> DesignCategories { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<UserSubscriptionType> UserSubscriptionTypes { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<ProjectImage> ProjectImages { get; set; }

        public virtual DbSet<ProjectOffer> ProjectOffers { get; set; }

        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        public virtual DbSet<UserProfileDesignCategory> UserProfileDesignCategories { get; set; }

        public virtual DbSet<UserEducation> UserEducations { get; set; }

        public virtual DbSet<UserExperience> UserExperiences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new IdentityConfig());
            modelBuilder.ApplyConfiguration(new DesignCategoryConfig());
            modelBuilder.ApplyConfiguration(new UserSubscriptionTypeConfig());
            modelBuilder.ApplyConfiguration(new ProjectConfig());
            modelBuilder.ApplyConfiguration(new MessageConfig());
            modelBuilder.ApplyConfiguration(new ProjectImageConfig());
            modelBuilder.ApplyConfiguration(new ProjectOfferConfig());
            modelBuilder.ApplyConfiguration(new UserProfileConfig());
            modelBuilder.ApplyConfiguration(new UserProfileDesignCategoryConfig());
            modelBuilder.ApplyConfiguration(new UserEducationConfig());
            modelBuilder.ApplyConfiguration(new UserExperienceConfig());
        }
    }
}
