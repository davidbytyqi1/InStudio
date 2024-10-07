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
    public class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> entity)
        {

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Text)
                .HasMaxLength(500)
                .IsUnicode(false);


        }
    }
}
