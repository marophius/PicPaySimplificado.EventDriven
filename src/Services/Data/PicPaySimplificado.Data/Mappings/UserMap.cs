using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PicPaySimplificado.Domain.Entities;
using PicPaySimplificado.Domain.Enums;
using PicPaySimplificado.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Document, doc =>
            {
                doc.Property(c => c.Number)
                .IsRequired()
                .HasMaxLength(Document.CNPJ_MAX_LENGTH)
                .HasColumnName("Document")
                .HasColumnType($"varchar({Document.CNPJ_MAX_LENGTH})");

                doc.HasIndex(c => c.Number);
            });

            builder.Property(x => x.UserType)
                .HasConversion(v => v.ToString(), v => (EUserType)Enum.Parse(typeof (EUserType), v));

            builder.OwnsOne(x => x.Email, email =>
            {
                email.Property(c => c.Address)
                    .IsRequired()
                    .HasMaxLength(Email.ADDRESS_MAX_LENGTH)
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.ADDRESS_MAX_LENGTH})");

                email.HasIndex(c => c.Address);
            });

            builder.OwnsOne(x => x.Name, name =>
            {
                name.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(Name.MAX_LENGTH)
                .HasColumnName("FirstName")
                .HasColumnType($"varchar({Name.MAX_LENGTH})");

                name.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(Name.MAX_LENGTH)
                .HasColumnName("LastName")
                .HasColumnType($"varchar({Name.MAX_LENGTH})");
            });

            builder.OwnsOne(x => x.Password, pass =>
            {
                pass.Property(c => c.Value)
                .IsRequired()
                .HasColumnName("Password")
                .HasColumnType("varchar(MAX)");
            });

            builder.HasMany(x => x.TransactionsAsPayer)
                .WithOne(c => c.PayerUser)
                .HasForeignKey(x => x.PayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.TransactionsAsPayee)
                .WithOne(c => c.PayeeUser)
                .HasForeignKey(x => x.PayeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
