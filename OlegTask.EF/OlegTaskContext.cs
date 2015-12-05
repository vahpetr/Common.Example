using System.Data.Entity;
using Common.EF.DataAccess;
using OlegTask.EF.Configurations;
using OlegTask.Helpers;
using OlegTask.Models;

namespace OlegTask.EF
{
    public class OlegTaskContext : BaseContext
    {
        public static readonly string Schema = "OlegTask";

        public OlegTaskContext() : base("OlegTaskConnection")
        {
        }

        /// <summary>
        /// Водители
        /// </summary>
        public virtual DbSet<Driver> Drivers { get; set; }
        /// <summary>
        /// Машины
        /// </summary>
        public virtual DbSet<Car> Cars { get; set; }
        /// <summary>
        /// Оценки
        /// </summary>
        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(Schema);

            modelBuilder.Configurations.Add(new DriverConfiguration());

            modelBuilder.Entity<Driver>()
                .HasMany(p => p.Cars)
                .WithMany(p => p.Drivers)
                .Map(c =>
                {
                    c.MapLeftKey(nameof(DriversCars.DriverId));
                    c.MapRightKey(nameof(DriversCars.CarId));
                    c.ToTable(typeof(DriversCars).Name);
                });
        }
    }
}