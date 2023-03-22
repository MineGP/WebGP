using Microsoft.EntityFrameworkCore;
using WebGP.Domain.SelfEntities;

namespace WebGP.Infrastructure.SelfDatabase
{
    public class SelfDbContext : DbContext
    {
        public SelfDbContext() { }
        public SelfDbContext(DbContextOptions<SelfDbContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admins");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.RoleName)
                    .HasColumnName("role")
                    .IsRequired();

                entity.Property(e => e.Token)
                    .HasColumnName("token");

                entity.Property(e => e.Note)
                    .HasColumnName("note");

                entity.Property(e => e.RegistrationTime)
                    .HasColumnName("registration_time");

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id");

                entity.HasOne(e => e.CreatedBy)
                    .WithOne()
                    .HasForeignKey<Admin>(a => a.CreatedById);
            });
        }
    }
}
