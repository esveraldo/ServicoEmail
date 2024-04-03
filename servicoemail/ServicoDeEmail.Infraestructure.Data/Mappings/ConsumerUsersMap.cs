using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicoDeEmail.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoDeEmail.Infraestructure.Data.Mappings
{
    public class ConsumerUsersMap : IEntityTypeConfiguration<ConsumerUsers>
    {
        public void Configure(EntityTypeBuilder<ConsumerUsers> builder)
        {
            builder.HasOne(s => s.ServiceUsers)
                .WithMany(c => c.ConsumerUsers)
                .HasForeignKey(p => p.ServiceUsersId);
        }
    }
}
