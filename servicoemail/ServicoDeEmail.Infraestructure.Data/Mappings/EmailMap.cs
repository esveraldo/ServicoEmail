using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicoDeEmail.Domain.Entities;
using Thrift.Protocol.Entities;

namespace ServicoDeEmail.Infraestructure.Data.Mappings
{
    public class EmailMap : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.From)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(e => e.InvalidEmails)
                .HasColumnType("varchar(255)");

            builder.Property(e => e.Subject)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(e => e.Message)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(e => e.Status)
                .HasColumnType("varchar(255)");

            builder.Property(e => e.AccessKey)
                .HasColumnType("varchar(150)");

            builder.Property(e => e.Obs)
                .HasColumnType("varchar(255)");

            builder.Property(e => e.SendDate)
                .IsRequired(false)
                .HasColumnType("datetime2")
                .HasDefaultValue(null);

            builder.Property(e => e.ScheduleDate)
                .IsRequired(false)
                .HasColumnType("datetime2")
                .HasDefaultValue(null);

            builder.Property(e => e.AttachmentsDocs)
                .IsRequired(false)
                .HasColumnType("text")
                .HasDefaultValue(null);

            builder.OwnsMany(e => e.Attachments, map =>
            {
                map.ToTable("Attachments").HasKey("Id");
                map.Property<int>("Id");
                map.WithOwner().HasForeignKey("EmailId");
                map.Property(x => x.FileNameSend).HasColumnType("varchar(255)");
                map.Property(x => x.Extension).HasColumnType("varchar(255)");
                map.Property(x => x.Code).HasColumnType("text");
            });
        }
    }
}
