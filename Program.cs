using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace TesteManyToManyErro
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
        }
    }
    public class ModelContextFactory : IDesignTimeDbContextFactory<ModelContext>
    {
        public ModelContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ModelContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<ModelContext>().UseSqlServer("Server=.\\sql2019;Database=AspNetIdentity;Trusted_Connection=false;user id=sa;pwd=*****;MultipleActiveResultSets=true;Encrypt=False");
            return new ModelContext(dbContextOptionsBuilder.Options);
        }
    }
    public class ModelContext : IdentityDbContext<Identidade, Perfil, int, IdentidadeClaim, IdentidadePerfil, IdentidadeLogin, PerfilClaim, IdentidadeToken>
    {
        public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Perfil>().ToTable("CFGPerfil");
            builder.Entity<Identidade>().ToTable("CFGIdentidade");
            builder.Entity<IdentityUserClaim<int>>().ToTable("CFGIdentidadeClaim").HasKey(x => new { x.Id });
            builder.Entity<IdentityUserRole<int>>().ToTable("CFGIdentidadePerfil").HasKey(x => new { x.UserId, x.RoleId });
            builder.Entity<IdentityUserLogin<int>>().ToTable("CFGIdentidadeLogin").HasKey(x => new { x.UserId, x.LoginProvider });
            builder.Entity<IdentityUserToken<int>>().ToTable("CFGIdentidadeToken").HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
            builder.Entity<IdentityRoleClaim<int>>().ToTable("CFGPerfilClaim").HasKey(x => new { x.Id });

            builder.Entity<IdentidadeClaim>().ToTable("CFGIdentidadeClaim");
            builder.Entity<IdentidadePerfil>().ToTable("CFGIdentidadePerfil");
            builder.Entity<IdentidadeLogin>().ToTable("CFGIdentidadeLogin");
            builder.Entity<IdentidadeToken>().ToTable("CFGIdentidadeToken");
            builder.Entity<PerfilClaim>().ToTable("CFGPerfilClaim");

        }
    }
    public class IdentidadeToken : IdentityUserToken<int> { }
    public class PerfilClaim : IdentityRoleClaim<int> { }
    public class IdentidadeLogin : IdentityUserLogin<int> { }
    public class IdentidadePerfil : IdentityUserRole<int> { }
    public class IdentidadeClaim : IdentityUserClaim<int> { }
    public class Perfil : IdentityRole<int> { }
    public class Identidade : IdentityUser<int> { }
}