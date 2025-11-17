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
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Table
            builder.ToTable("Usuario");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Properties

            builder.Property(e => e.RoleId)
                .HasColumnName("RoleId")
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnName("Nombre")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.LastName)
                .HasColumnName("Apellido")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.IdCard)
                .HasColumnName("Cedula")
                .IsRequired()
                .HasColumnType("decimal(18,0)");

            builder.Property(e => e.UserName)
                .HasColumnName("Usuario_Nombre")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Password)
                .HasColumnName("Contrasena")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Birthday)
                .HasColumnName("Fecha_Nacimiento")
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(e => e.TransactionUser)
                .HasColumnName("Usuario_Transaccion")
                .IsRequired()
                .HasMaxLength(255)
                .HasDefaultValueSql("SUSER_NAME()");

            builder.Property(e => e.TransactionDate)
                .HasColumnName("Fecha_Transaccion")
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            // Indexes
            builder.HasIndex(e => e.IdCard)
                .IsUnique()
                .HasDatabaseName("UQ_Usuario_Cedula_01");

            builder.HasIndex(e => e.UserName)
                .IsUnique()
                .HasDatabaseName("UQ_Usuario_Nombre_02");

            builder.HasIndex(e => e.Birthday)
                .HasDatabaseName("IX_Usuario_FechaNacimiento");

            // Foreign Key Relationship
            builder.HasOne(e => e.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Usuario_Role");

        }
    }
}
