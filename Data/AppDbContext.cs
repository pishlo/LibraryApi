// Library_AppContext.cs

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ApiProject.Models;

namespace ApiProject.Data
{
    public class Library_AppContext : IdentityDbContext<IdentityUser>
    {
        public Library_AppContext(DbContextOptions<Library_AppContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<BorrowRecord> BorrowRecords { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // --- Author Config ---
            builder.Entity<Author>()
                .Property(a => a.Name)
                .HasMaxLength(20)
                .IsRequired();

            // --- Book Config ---
            builder.Entity<Book>()
                .Property(b => b.Title)
                .HasMaxLength(50)
                .IsRequired();

            // Relationship: one Author → many Books
            builder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany()
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            // --- Member Config ---
            builder.Entity<Member>()
                .Property(m => m.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Entity<Member>()
                .Property(m => m.Email)
                .HasMaxLength(100)
                .IsRequired();

            // --- BorrowRecord Config ---
            builder.Entity<BorrowRecord>()
                .HasOne(br => br.Book)
                .WithMany()
                .HasForeignKey(br => br.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<BorrowRecord>()
                .HasOne(br => br.Member)
                .WithMany(m => m.BorrowRecords)
                .HasForeignKey(br => br.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
