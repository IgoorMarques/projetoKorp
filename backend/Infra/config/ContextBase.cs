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

            // Propriedade default para DataHoraCriacao
            modelBuilder.Entity<Conversa>()
                .Property(c => c.DataHoraCriacao)
                .HasDefaultValueSql("GETUTCDATE()");

            // Relacionamento entre Participante1 e Conversa
            modelBuilder.Entity<Conversa>()
                .HasOne(c => c.Participante1)
                .WithMany(u => u.ConversasComoParticipante1)
                .HasForeignKey(c => c.Participante1Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento entre Participante2 e Conversa
            modelBuilder.Entity<Conversa>()
                .HasOne(c => c.Participante2)
                .WithMany(u => u.ConversasComoParticipante2)
                .HasForeignKey(c => c.Participante2Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento entre Conversa e Mensagem
            modelBuilder.Entity<Conversa>()
                .HasMany(c => c.Mensagens)
                .WithOne(m => m.Conversa)
                .HasForeignKey(m => m.ConversaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento para Mensagem (DataHoraEnvio e StatusLeitura)
            modelBuilder.Entity<Mensagem>()
                .Property(m => m.DataHoraEnvio)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Mensagem>()
                .Property(m => m.StatusLeitura)
                .IsRequired()
                .HasDefaultValue(true);

            modelBuilder.Entity<Mensagem>()
                .HasOne(u => u.Remetente)
                .WithMany()
                .HasForeignKey(m => m.RemetendeId);


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
