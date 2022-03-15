using DesafioGerenciadorCursos.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioGerenciadorCursos;

namespace DesafioGerenciadorCursos.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Curso>(e =>
            {
                e.ToTable("Cursos");
                e.HasKey(p => p.Id);
                e.Property(p => p.Titulo).HasColumnType("varchar(40)").IsRequired();

                /*Estou supondo aqui que o curso vale por 90 dias. Nesse caso, a duração do curso
                 * será a data que eu pegar quando adicionar o curso + 90 dias. */
                e.Property(p => p.Duracao).HasDefaultValueSql("dateadd(day,90,getdate())").ValueGeneratedOnAdd();
                
                e.Property(p => p.Status).HasConversion<string>();

            });

            modelBuilder.Entity<Pessoa>(e =>
            {
                e.ToTable("Pessoas");
                e.HasKey(p => p.Id);
                e.Property(p => p.Nome).HasColumnType("varchar(40)").IsRequired();
                e.Property(p => p.Login).HasColumnType("varchar(40)").IsRequired();
                e.Property(p => p.Senha).HasColumnType("varchar(40)").IsRequired();
                e.Property(p => p.TipoPessoa).HasConversion<string>();

            });

        }


        /*pelo fato de estar um pouco confuso com a parte de chave estrangeira com fluent api, 
          eu optei por utilizar dessa forma a tabela matricula*/
        public DbSet<Matricula> Matricula { get; set; }


        /*pelo fato de estar um pouco confuso com a parte de chave estrangeira com fluent api, 
          eu optei por utilizar dessa forma a tabela matricula*/
        public DbSet<DesafioGerenciadorCursos.Curso> Curso { get; set; }


        /*pelo fato de estar um pouco confuso com a parte de chave estrangeira com fluent api, 
          eu optei por utilizar dessa forma a tabela matricula*/
        public DbSet<DesafioGerenciadorCursos.Pessoa> Pessoa { get; set; }
    }
}
