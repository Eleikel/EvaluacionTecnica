using EvaluacionTecnica.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Persistence.Configurations
{
    public class RolesConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //Table
            builder.ToTable("Role");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Properties

            builder.Property(e => e.Name)
                .HasColumnName("Nombre")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.TransactionUser)
                .HasColumnName("Usuario_Transaccion")
                .IsRequired()
                .HasMaxLength(255)
                .HasDefaultValueSql("SUSER_NAME()");

            builder.Property(e => e.TransactionDate)
                .HasColumnName("Fecha_Transaccion")
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();

            // Indexes
            builder.HasIndex(e => e.Name)
                .IsUnique()
                .HasDatabaseName("UQ_Role_Nombre");

        }
    }
}
