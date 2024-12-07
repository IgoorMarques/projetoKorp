using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities.Context
{
    public class ContextBase : IdentityDbContext<Usuario>
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options) { }
        public DbSet<AnuncioAnimal> AnunciosAnimais { get; set; }
        public DbSet<Conversa> Conversas { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }
        public DbSet<Midia> Midias { get; set; }
        public DbSet<UsuariosConversa> UsuariosConversas { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ObterStringConexao());
            }
        }

        private string ObterStringConexao()
        {
            return "Data Source=DESKTOP-4LE6SQB;Initial Catalog=SistemaAdocaoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UsuariosConversa>()
                .HasOne(uc => uc.Conversa)
                .WithMany(c => c.ParticipantesConversa)
                .HasForeignKey(uc => uc.ConversaId);

            modelBuilder.Entity<UsuariosConversa>()
                .HasOne(uc => uc.Participante1)
                .WithMany()
                .HasForeignKey(uc => uc.Participante1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UsuariosConversa>()
                .HasOne(uc => uc.Participante2)
                .WithMany()
                .HasForeignKey(uc => uc.Participante2Id)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Conversa>()
               .Property(c => c.DataHoraCriacao)
               .HasDefaultValueSql("GETUTCDATE()");
               


            modelBuilder.Entity<Conversa>()
              .HasMany(C => C.ParticipantesConversa)
              .WithOne(UC => UC.Conversa)
              .HasForeignKey(UC => UC.ConversaId)
              .IsRequired();

            modelBuilder.Entity<Conversa>()
               .HasMany(M => M.Mensagens)
               .WithOne(C => C.Conversa)
               .HasForeignKey(C => C.ConversaId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<AnuncioAnimal>()
                .HasMany(AA => AA.Midias)
                .WithOne(M => M.AnuncioAnimal)
                .HasForeignKey(M => M.AnuncioAnimalId)
                .IsRequired();


            modelBuilder.Entity<Usuario>()
                .HasMany(AA => AA.Anuncios)
                .WithOne(M => M.Anunciante)
                .HasForeignKey(M => M.AnuncianteId)
                .IsRequired();
        }

    }
}
