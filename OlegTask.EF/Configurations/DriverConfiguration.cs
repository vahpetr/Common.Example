using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using OlegTask.Models;

namespace OlegTask.EF.Configurations
{
    public class DriverConfiguration : EntityTypeConfiguration<Driver>
    {
        public DriverConfiguration()
        {
            Property(p => p.PassportNumber)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("QIX_PassportNumber") { IsUnique = true }));
        }
    }
}