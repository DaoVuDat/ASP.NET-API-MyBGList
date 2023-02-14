using Microsoft.EntityFrameworkCore;

namespace MyBGList.Models;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<BoardGames_Domains>()
            .HasKey(i => new { i.BoardGameId, i.DomainId });

        modelBuilder.Entity<BoardGames_Domains>()
            .HasOne(x => x.BoardGame)
            .WithMany(y => y.BoardGamesDomainsCollection)
            .HasForeignKey(f => f.BoardGameId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BoardGames_Domains>()
            .HasOne(o => o.Domain)
            .WithMany(m => m.BoardGamesDomainsCollection)
            .HasForeignKey(f => f.DomainId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<BoardGames_Mechanics>()
            .HasKey(i => new { i.BoardGameId, i.MechanicId });
        
        modelBuilder.Entity<BoardGames_Mechanics>()
            .HasOne(x => x.BoardGame)
            .WithMany(y => y.BoardGamesMechanicsCollection)
            .HasForeignKey(f => f.BoardGameId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
 
        modelBuilder.Entity<BoardGames_Mechanics>()
            .HasOne(o => o.Mechanic)
            .WithMany(m => m.BoardGamesMechanicsCollection)
            .HasForeignKey(f => f.MechanicId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BoardGame>()
            .HasOne(x => x.Publisher)
            .WithMany(x => x.BoardGames)
            .HasForeignKey(x => x.PublisherId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BoardGames_Categories>()
            .HasKey(i => new { i.BoardGameId, i.CategoryId });

        modelBuilder.Entity<BoardGames_Categories>()
            .HasOne(x => x.BoardGame)
            .WithMany(x => x.BoardGamesCategoriesCollection)
            .HasForeignKey(x => x.BoardGameId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BoardGames_Categories>()
            .HasOne(x => x.Catergory)
            .WithMany(x => x.BoardGamesCategoriesCollection)
            .HasForeignKey(x => x.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

    }

    public DbSet<BoardGame> BoardGames => Set<BoardGame>();
    public DbSet<Domain> Domains => Set<Domain>();
    public DbSet<Mechanic> Mechanics => Set<Mechanic>();
    public DbSet<BoardGames_Domains> BoardGamesDomains => Set<BoardGames_Domains>();
    public DbSet<BoardGames_Mechanics> BoardGamesMechanics => Set<BoardGames_Mechanics>();
    
    public DbSet<Publisher> Publishers { get; set; }
}