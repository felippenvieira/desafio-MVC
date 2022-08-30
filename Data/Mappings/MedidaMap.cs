using desafio_mvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace desafio_mvc.Data.Mappings
{
    public class MedidaMap : IEntityTypeConfiguration<Medida>
    {
        public void Configure(EntityTypeBuilder<Medida> builder)
        {
            builder.ToTable("Medidas");

            builder.HasKey(x => x.Id);

            // builder.Property(x => x.Id)
            //     .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);
        }
    }
}