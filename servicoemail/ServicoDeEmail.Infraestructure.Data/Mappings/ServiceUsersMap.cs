using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ServicoDeEmail.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoDeEmail.Infraestructure.Data.Mappings
{
    public class ServiceUsersMap : IEntityTypeConfiguration<ServiceUsers>
    {
        public void Configure(EntityTypeBuilder<ServiceUsers> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Smtp)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(u => u.Port)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(u => u.Active)
                .IsRequired()
                .HasColumnType("BIT")
                .HasConversion<bool>();

            builder.Property(u => u.NameUser)
                .HasColumnType("varchar(255)");
        }
    }
}
