using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServicoDeEmail.Domain.Entities;
using ServicoDeEmail.Infraestructure.Data.Mappings;
using System.ComponentModel;

namespace ServicoDeEmail.Infraestructure.Data.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Email> Emails { get; set; }
        public DbSet<ServiceUsers> ServiceUsers { get; set; }
        public DbSet<ConsumerUsers> ConsumerUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmailMap());
            modelBuilder.ApplyConfiguration(new ServiceUsersMap());
            modelBuilder.ApplyConfiguration(new ConsumerUsersMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>();

            //configurationBuilder.Properties<TimeOnly>()
            //    .HaveConversion<TimeOnlyConverter>();
        }
    }

    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter() : base(
            dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
            dateTime => DateOnly.FromDateTime(dateTime))
        { }
    }

    public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
    {
        public TimeOnlyConverter() : base(
            timeOnly => timeOnly.ToTimeSpan(),
            timeSpan => TimeOnly.FromTimeSpan(timeSpan))
        { }
    }
}
