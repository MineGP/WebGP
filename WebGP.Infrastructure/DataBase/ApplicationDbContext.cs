using Microsoft.EntityFrameworkCore;
using WebGP.Application.Common.Interfaces;
using WebGP.Domain.Entities;

namespace WebGP.Infrastructure.DataBase;

public partial class ApplicationDbContext : DbContext, IContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Discord> Discords { get; set; }
    public virtual DbSet<Online> Onlines { get; set; }
    public virtual DbSet<OnlineLog> OnlineLogs { get; set; }
    public virtual DbSet<RoleWorkReadonly> RoleWorkReadonlies { get; set; }
    public virtual DbSet<User> Users { get; set; }

    IQueryable<Discord> IContext.Discords => Discords;
    IQueryable<Online> IContext.Onlines => Onlines;
    IQueryable<OnlineLog> IContext.OnlineLogs => OnlineLogs;
    IQueryable<RoleWorkReadonly> IContext.RoleWorkReadonlies => RoleWorkReadonlies;
    IQueryable<User> IContext.Users => Users;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Discord>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("discord");

            entity.HasIndex(e => e.DiscordId, "discord_id").IsUnique();

            entity.HasIndex(e => e.Uuid, "uuid").IsUnique();

            entity.Property(e => e.DiscordId)
                .HasColumnType("bigint(20)")
                .HasColumnName("discord_id");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("last_update");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<Online>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.ToTable("online");

            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasColumnName("uuid");
            entity.Property(e => e.IsOp).HasColumnName("is_op");
            entity.Property(e => e.SkinUrl)
                .HasColumnType("text")
                .HasColumnName("skin_url");
            entity.Property(e => e.TimedId)
                .HasColumnType("int(11)")
                .HasColumnName("timed_id");
            entity.Property(e => e.World)
                .HasColumnType("int(11)")
                .HasColumnName("world");
            entity.Property(e => e.X).HasColumnName("x");
            entity.Property(e => e.Y).HasColumnName("y");
            entity.Property(e => e.Z).HasColumnName("z");
        });

        modelBuilder.Entity<OnlineLog>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.Day })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("online_logs");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Day).HasColumnName("day");
            entity.Property(e => e.Sec)
                .HasColumnType("int(11)")
                .HasColumnName("sec");
        });

        modelBuilder.Entity<RoleWorkReadonly>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("role_work_readonly");

            entity.Property(e => e.Icon)
                .HasColumnType("mediumtext")
                .HasColumnName("icon");
            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasDefaultValueSql("''")
                .HasColumnType("mediumtext")
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(4)
                .HasDefaultValueSql("''")
                .HasColumnName("type");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Phone, "phone").IsUnique();

            entity.HasIndex(e => e.Uuid, "uuid").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.BirthdayDate)
                .HasColumnType("datetime")
                .HasColumnName("birthday_date");
            entity.Property(e => e.CardId)
                .HasColumnType("int(11)")
                .HasColumnName("card_id");
            entity.Property(e => e.ConnectDate)
                .HasColumnType("datetime")
                .HasColumnName("connect_date");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Exp)
                .HasColumnType("int(11)")
                .HasColumnName("exp");
            entity.Property(e => e.FirstName)
                .HasMaxLength(15)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(15)
                .HasColumnName("last_name");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("last_update");
            entity.Property(e => e.Male)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("male");
            entity.Property(e => e.Phone)
                .HasColumnType("int(11)")
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasColumnType("int(11)")
                .HasColumnName("role");
            entity.Property(e => e.RoleTime)
                .HasColumnType("datetime")
                .HasColumnName("role_time");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasColumnName("uuid");
            entity.Property(e => e.Work)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)")
                .HasColumnName("work");
            entity.Property(e => e.WorkTime)
                .HasColumnType("datetime")
                .HasColumnName("work_time");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}