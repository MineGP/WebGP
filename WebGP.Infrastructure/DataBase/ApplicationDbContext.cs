using Microsoft.EntityFrameworkCore;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;
using WebGP.Domain.Entities;

namespace WebGP.Infrastructure.DataBase;

public partial class ApplicationDbContext : DbContext, IContextGPC, IContextGPO
{
    public ApplicationDbContext()
    {
        Console.WriteLine("ADC.0:" + this.Database.ProviderName);
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Console.WriteLine("ADC.1:" + this.Database.ProviderName);
    }

    public virtual DbSet<Discord> Discords { get; set; }

    public virtual DbSet<Online> Onlines { get; set; }

    public virtual DbSet<OnlineLog> OnlineLogs { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleWorkReadonly> RoleWorkReadonlies { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WorkReadonly> WorkReadonlies { get; set; }

    public DbContext DbContext => this;

    IQueryable<Discord> IContext.Discords => Discords;
    IQueryable<Online> IContext.Onlines => Onlines;
    IQueryable<OnlineLog> IContext.OnlineLogs => OnlineLogs;
    IQueryable<RoleWorkReadonly> IContext.RoleWorkReadonlies => RoleWorkReadonlies;
    IQueryable<User> IContext.Users => Users;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

        modelBuilder.Entity<Discord>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("discord")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.DiscordId, "discord_id").IsUnique();

            entity.HasIndex(e => e.Uuid, "uuid").IsUnique();

            entity.Property(e => e.DiscordId)
                .HasColumnType("bigint(20)")
                .HasColumnName("discord_id");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
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

            entity
                .ToTable("online")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasColumnName("uuid");
            entity.Property(e => e.DataIcon)
                .HasColumnType("text")
                .HasColumnName("data_icon");
            entity.Property(e => e.DataName)
                .HasColumnType("text")
                .HasColumnName("data_name");
            entity.Property(e => e.Die).HasColumnName("die");
            entity.Property(e => e.Gpose)
                .HasDefaultValueSql("'NONE'")
                .HasColumnType("enum('SIT','LAY','NONE')")
                .HasColumnName("gpose");
            entity.Property(e => e.Hide).HasColumnName("hide");
            entity.Property(e => e.IsOp).HasColumnName("is_op");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("last_update");
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
            entity.Property(e => e.ZoneSelector)
                .HasColumnType("text")
                .HasColumnName("zone_selector");
        });

        modelBuilder.Entity<OnlineLog>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.Day })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity
                .ToTable("online_logs")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Day).HasColumnName("day");
            entity.Property(e => e.Sec)
                .HasColumnType("int(11)")
                .HasColumnName("sec");
            entity.Property(e => e.SecAban)
                .HasColumnType("int(11)")
                .HasColumnName("sec_aban");
            entity.Property(e => e.SecAfk)
                .HasColumnType("int(11)")
                .HasColumnName("sec_afk");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("roles")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(6)
                .HasDefaultValueSql("'FFFFFF'")
                .HasColumnName("color");
            entity.Property(e => e.DiscordRole)
                .HasColumnType("bigint(20)")
                .HasColumnName("discord_role");
            entity.Property(e => e.HeadData)
                .HasColumnType("text")
                .HasColumnName("head_data");
            entity.Property(e => e.IdGroup)
                .HasColumnType("int(11)")
                .HasColumnName("id_group");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("last_update");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
            entity.Property(e => e.PermMenu)
                .HasColumnType("bigint(20)")
                .HasColumnName("perm_menu");
            entity.Property(e => e.PermMenuLocal)
                .HasColumnType("bigint(20)")
                .HasColumnName("perm_menu_local");
            entity.Property(e => e.Permissions)
                .HasColumnType("int(11)")
                .HasColumnName("permissions");
            entity.Property(e => e.Static)
                .HasColumnType("tinyint(4)")
                .HasColumnName("static");
        });

        modelBuilder.Entity<RoleWorkReadonly>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("role_work_readonly");

            entity.Property(e => e.Icon)
                .HasColumnType("mediumtext")
                .HasColumnName("icon")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasDefaultValueSql("''")
                .HasColumnType("mediumtext")
                .HasColumnName("name")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.Type)
                .HasMaxLength(4)
                .HasDefaultValueSql("''")
                .HasColumnName("type")
                .UseCollation("utf8mb4_unicode_ci");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("users")
                .UseCollation("utf8mb4_unicode_ci");

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
            entity.Property(e => e.CardRegen)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(11)")
                .HasColumnName("card_regen");
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
                .ValueGeneratedOnAddOrUpdate()
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
            entity.Property(e => e.PhoneRegen)
                .HasDefaultValueSql("'3'")
                .HasColumnType("int(11)")
                .HasColumnName("phone_regen");
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
            entity.Property(e => e.Wanted)
                .HasColumnType("int(11)")
                .HasColumnName("wanted");
            entity.Property(e => e.WantedId)
                .HasColumnType("int(11)")
                .HasColumnName("wanted_id");
            entity.Property(e => e.Work)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)")
                .HasColumnName("work");
            entity.Property(e => e.WorkTime)
                .HasColumnType("datetime")
                .HasColumnName("work_time");
        });

        modelBuilder.Entity<WorkReadonly>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.Type })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity
                .ToTable("work_readonly")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Type)
                .HasColumnType("enum('WORK')")
                .HasColumnName("type");
            entity.Property(e => e.Icon)
                .HasColumnType("text")
                .HasColumnName("icon");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");

        });
        /*modelBuilder.Entity<OnlineVm>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.TimedId)
                .HasColumnName("timed_id")
                .HasColumnType("int(11)");
            entity.Property(e => e.Uuid)
                .HasColumnName("uuid")
                .HasMaxLength(36);
            entity.Property(e => e.StaticId)
                .HasColumnName("id")
                .HasColumnType("int(11)");
            entity.Property(e => e.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(15);
            entity.Property(e => e.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(15);
            entity.Property(e => e.DiscordId)
                .HasColumnName("discord_id")
                .HasColumnType("bigint(20)");
            entity.Property(e => e.Role)
                .HasColumnName("role")
                .HasColumnType("mediumtext")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.Work)
                .HasColumnName("work")
                .HasColumnType("mediumtext")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.Level)
                .HasColumnName("Level")
                .HasColumnType("int(11)");
            entity.Property(e => e.SkinUrl)
                .HasColumnName("skin_url")
                .HasColumnType("text");
        });*/
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}