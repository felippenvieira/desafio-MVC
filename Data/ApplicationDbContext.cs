using System;
using System.Collections.Generic;
using desafio_mvc.Data.Mappings;
using desafio_mvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace desafio_mvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Medida> Medidas { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<Receita> Receitas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MedidaMap());
            builder.ApplyConfiguration(new IngredienteMap());
            builder.ApplyConfiguration(new ReceitaMap());

            base.OnModelCreating(builder);
            this.SeedUsers(builder);
            this.SeedRoles(builder);
            this.SeedUserRoles(builder);

            var xicara = new Medida
            {
                Id = Guid.Parse("159be04a-3fb4-4fc2-b6cc-410774d30b37"),
                Nome = "1 Xícara"
            };
            var colherDeSopa = new Medida
            {
                Id = Guid.Parse("ab417aa0-2235-4e42-b137-02371d78465d"),
                Nome = "1 Colher de Sopa"
            };
            var colherDeCha = new Medida
            {
                Id = Guid.Parse("a6c46952-993c-4f0f-833c-75eaeb1ecbc5"),
                Nome = "1 Colher de Chá"
            };
            var duasUnidades = new Medida
            {
                Id = Guid.Parse("af2b7783-2762-4069-8627-cd2479d4bd3c"),
                Nome = "2 unidades"
            };
            var grama = new Medida
            {
                Id = Guid.Parse("e81b1812-1bd8-43af-a64a-d466845ebb48"),
                Nome = "1 grama(s)"
            };
            var kilo = new Medida
            {
                Id = Guid.Parse("1c04a897-891a-4bf5-b375-d4d0e40b7004"),
                Nome = "1 kilo(s)"
            };

            // var leiteCondensado = new
            // {
            //     Id = Guid.Parse("15e51d93-bca6-4f9f-9207-e0cfcf110a84"),
            //     Nome = "Leite Condensado",
            //     MedidaId = xicara.Id,
            //     ReceitaId = Guid.NewGuid()
            // };

            var leiteCondensado = new
            {
                Id = Guid.Parse("15e51d93-bca6-4f9f-9207-e0cfcf110a84"),
                Nome = "Leite Condensado",
                MedidaId = xicara.Id,
                ReceitaId = Guid.Parse("6b3f3c3e-bc09-4d72-a964-b2bd0ec939ea")
            };

            var ovo = new
            {
                Id = Guid.Parse("2429f727-8a11-4d2f-a807-8455b65944aa"),
                Nome = "Ovo(s)",
                MedidaId = duasUnidades.Id,
                ReceitaId = Guid.Parse("6b3f3c3e-bc09-4d72-a964-b2bd0ec939ea")
            };

            var manteiga = new
            {
                Id = Guid.Parse("98f59f2a-617f-40e6-81f6-78bc2dd641c4"),
                Nome = "Manteiga",
                MedidaId = colherDeSopa.Id,
                ReceitaId = Guid.Parse("6b3f3c3e-bc09-4d72-a964-b2bd0ec939ea")
            };

            var margarina = new
            {
                Id = Guid.Parse("85fc6382-d271-4824-9f05-06159427588f"),
                Nome = "Margarina",
                MedidaId = colherDeSopa.Id,
                ReceitaId = Guid.Parse("6b3f3c3e-bc09-4d72-a964-b2bd0ec939ea")
            };

            var leite = new
            {
                Id = Guid.Parse("2216c916-2522-4635-9ff8-ed85c2f2fb13"),
                Nome = "Leite",
                MedidaId = xicara.Id,
                ReceitaId = Guid.Parse("6b3f3c3e-bc09-4d72-a964-b2bd0ec939ea")
            };

            var cremeDeLeite = new
            {
                Id = Guid.Parse("c9aa80cc-c3b9-45c2-8f69-e3b19ad47ebd"),
                Nome = "Creme de Leite",
                MedidaId = duasUnidades.Id,
                ReceitaId = Guid.Parse("6b3f3c3e-bc09-4d72-a964-b2bd0ec939ea")
            };

            var acucar = new
            {
                Id = Guid.Parse("fd1f8a0f-3041-49b3-9a6b-b943bbf0b8c7"),
                Nome = "Açucar",
                MedidaId = colherDeSopa.Id,
                ReceitaId = Guid.Parse("6b3f3c3e-bc09-4d72-a964-b2bd0ec939ea")
            };

            var fermento = new
            {
                Id = Guid.Parse("c85f2923-d360-49e5-80bf-9e78737d0a78"),
                Nome = "Fermento",
                MedidaId = colherDeSopa.Id,
                ReceitaId = Guid.Parse("6b3f3c3e-bc09-4d72-a964-b2bd0ec939ea")
            };

            var cacauEmPo = new
            {
                Id = Guid.Parse("80cfe11b-32ff-47c2-99c9-b53fab1f8f96"),
                Nome = "Cacau em pó",
                MedidaId = colherDeSopa.Id,
                ReceitaId = Guid.Parse("4c6ff2e4-885a-4e4c-9572-2c6223ad77ed")
            };

            var nescau = new
            {
                Id = Guid.Parse("384cfb72-365b-4fdd-8451-1df76be6747a"),
                Nome = "Nescau",
                MedidaId = colherDeSopa.Id,
                ReceitaId = Guid.Parse("4c6ff2e4-885a-4e4c-9572-2c6223ad77ed")
            };

            var toddy = new
            {
                Id = Guid.Parse("f3acd12f-ea97-4742-be03-c946be66f0a7"),
                Nome = "Toddy",
                MedidaId = colherDeSopa.Id,
                ReceitaId = Guid.Parse("4c6ff2e4-885a-4e4c-9572-2c6223ad77ed")
            };

            var farinha = new
            {
                Id = Guid.Parse("1c42df66-ea79-4871-904c-4fcca1d1ef74"),
                Nome = "Farinha",
                MedidaId = kilo.Id,
                ReceitaId = Guid.Parse("4c6ff2e4-885a-4e4c-9572-2c6223ad77ed")
            };

            var boloDeCenoura = new
            {
                Id = Guid.Parse("6b3f3c3e-bc09-4d72-a964-b2bd0ec939ea"),
                Nome = "Bolo de Cenoura",
                UrlImagem = "https://s2.glbimg.com/lIk4hcrgBYNsdy88WiC1VRDz2-E=/1200x/smart/filters:cover():strip_icc()/i.s3.glbimg.com/v1/AUTH_e84042ef78cb4708aeebdf1c68c6cbd6/internal_photos/bs/2020/J/Z/aaiU9jSmi1euyq2oVFzQ/bolo-de-cenoura-invertido.jpg",
                TempoPreparo = "25 minutos",
                ModoPreparo = "Bata tudo no liquidificador",
                Ingredientes = new { leite, fermento, acucar, leiteCondensado }
            };

            var boloDeChocolate = new
            {
                Id = Guid.Parse("4c6ff2e4-885a-4e4c-9572-2c6223ad77ed"),
                Nome = "Bolo de Chocolate",
                UrlImagem = "https://i.ytimg.com/vi/QFMxJWh3mqE/maxresdefault.jpg",
                TempoPreparo = "25 minutos",
                ModoPreparo = "Bata tudo no fuê",
                Ingredientes = new { leite, fermento, acucar, cacauEmPo, leiteCondensado }
            };

            builder.Entity<Medida>().HasData(
                xicara,
                colherDeSopa,
                colherDeCha,
                duasUnidades,
                grama,
                kilo);

            builder.Entity<Ingrediente>().HasData(
                leiteCondensado,
                ovo,
                manteiga,
                margarina,
                leite,
                cremeDeLeite,
                acucar,
                fermento,
                cacauEmPo,
                nescau,
                toddy,
                farinha);

            builder.Entity<Receita>().HasData(
                boloDeCenoura,
                boloDeChocolate);
        }

        private void SeedUsers(ModelBuilder builder)
        {
            IdentityUser admin = new IdentityUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "admin@gft.com",
                NormalizedUserName = "ADMIN@GFT.COM",
                Email = "admin@gft.com",
                NormalizedEmail = "ADMIN@GFT.COM",
                LockoutEnabled = true,
                EmailConfirmed = true
            };

            PasswordHasher<IdentityUser> passwordHasherAdmin = new PasswordHasher<IdentityUser>();
            admin.PasswordHash = passwordHasherAdmin.HashPassword(admin, "Gft@1234");

            IdentityUser user = new IdentityUser()
            {
                Id = "e10330c4-b8a1-4045-8965-afffba36fab6",
                UserName = "usuario@gft.com",
                NormalizedUserName = "USUARIO@GFT.COM",
                Email = "usuario@gft.com",
                NormalizedEmail = "USUARIO@GFT.COM",
                LockoutEnabled = true,
                EmailConfirmed = true
            };

            PasswordHasher<IdentityUser> passwordHasherUser = new PasswordHasher<IdentityUser>();
            user.PasswordHash = passwordHasherUser.HashPassword(user, "Gft@1234");

            builder.Entity<IdentityUser>().HasData(
                admin,
                user);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = "User", ConcurrencyStamp = "2", NormalizedName = "USER" }
                );
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    UserId = "b74ddd14-6340-4840-95c2-db12554843e5",
                    RoleId = "fab4fac1-c546-41de-aebc-a14da6895711"
                },
                new IdentityUserRole<string>()
                {
                    UserId = "e10330c4-b8a1-4045-8965-afffba36fab6",
                    RoleId = "c7b013f0-5201-4317-abd8-c211f91b7330"
                }
                );
        }
    }
}

