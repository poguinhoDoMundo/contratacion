using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace advantage.API.Models
{
    public partial class providersBankContext : DbContext
    {
        public DbSet<scalarInt> scalarInt{get;set;} 
        public  DbSet<Carga> Carga { get; set; }
        public DbSet<Documento> Documento { get; set; }
        public DbSet<documento_inner> documento_inner { get; set; }
        public DbSet<EstadoCarga> EstadoCarga { get; set; }
        public DbSet<EstadoRevision> EstadoRevision { get; set; }
        public DbSet<Organizacion> Organizacion { get; set; }
        public DbSet<PersonaTipo> PersonaTipo { get; set; }
        public DbSet<Revision> Revision { get; set; }
        public DbSet<TipoAnexo> TipoAnexo { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioAdmon> UsuarioAdmon { get; set; }
        public DbSet<UsuarioAnexo> UsuarioAnexo { get; set; }

        public DbSet<OrganizacionMain> OrganizacionMain {get;set;}

        public DbSet<vDocumento> vDocumento{get;set;}

        public DbSet<vDocumentosPersona> vDocumentosPersona{get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=localhost;Port=1521;Database=providersBank;User Id=pDev;Password=asiDoCma2006;Search Path=pbank,public; Integrated Security=true;Pooling=true;");


    }
}
