using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using desafio_mvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace desafio_mvc.Data.Mappings
{
    public class ReceitaMap : IEntityTypeConfiguration<Receita>
    {
        public void Configure(EntityTypeBuilder<Receita> builder)
        {
            builder.ToTable("Receitas");

            builder.HasKey(x => x.Id);

            // builder.Property(x => x.Id)
            //     .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.UrlImagem)
                .IsRequired()
                .HasColumnName("UrlImagem")
                .HasColumnType("VARCHAR")
                .HasMaxLength(1000);

            builder.Property(x => x.TempoPreparo)
                .IsRequired()
                .HasColumnName("TempoPreparo")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.ModoPreparo)
                .IsRequired()
                .HasColumnName("ModoPreparo")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(2000);

            builder.Property(x => x.DataCriacao)
                .IsRequired()
                .HasColumnName("DataCriacao")
                .HasColumnType("DATETIME")
                .HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.HasMany(x => x.Ingredientes)
                .WithOne(x => x.Receita)
                .IsRequired();
        }
    }
}