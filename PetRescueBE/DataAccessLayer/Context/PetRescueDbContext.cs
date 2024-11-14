using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Context
{
    public partial class PetRescueDbContext : DbContext
    {
        public PetRescueDbContext()
        {
        }

        public PetRescueDbContext(DbContextOptions<PetRescueDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdoptionApplication> AdoptionApplications { get; set; } = null!;
        public virtual DbSet<Donation> Donations { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<Pet> Pets { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Shelter> Shelters { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder.UseSqlServer(GetConnectionString());

        string GetConnectionString()
        {
            IConfiguration builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            return builder["ConnectionStrings:DefaultConnection"];
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdoptionApplication>(entity =>
            {
                entity.HasKey(e => e.ApplicationId)
                    .HasName("PK__Adoption__C93A4F79C7433110");

                entity.ToTable("AdoptionApplication");

                entity.Property(e => e.ApplicationId)
                    .HasColumnName("ApplicationID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.PetId).HasColumnName("PetID");

                entity.Property(e => e.RequestDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status).HasMaxLength(255);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.AdoptionApplications)
                    .HasForeignKey(d => d.PetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("adoptionapplication_petid_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AdoptionApplications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("adoptionapplication_userid_foreign");
            });

            modelBuilder.Entity<Donation>(entity =>
            {
                entity.ToTable("Donation");

                entity.Property(e => e.DonationId)
                    .HasColumnName("DonationID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.DonationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.PaymentMethod).HasMaxLength(50);

                entity.Property(e => e.ShelterId).HasColumnName("ShelterID");

                entity.Property(e => e.Status).HasMaxLength(255);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Donations)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("donation_eventid_foreign");

                entity.HasOne(d => d.Shelter)
                    .WithMany(p => p.Donations)
                    .HasForeignKey(d => d.ShelterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("donation_shelterid_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Donations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("donation_userid_foreign");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.Property(e => e.EventId)
                    .HasColumnName("EventID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.EndDateTime).HasColumnType("datetime");

                entity.Property(e => e.EventType).HasMaxLength(255);

                entity.Property(e => e.Goal).HasMaxLength(255);

                entity.Property(e => e.Location).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.ShelterId).HasColumnName("ShelterID");

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(255);

                entity.HasOne(d => d.Shelter)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.ShelterId)
                    .HasConstraintName("event_shelterid_foreign");
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.ToTable("Pet");

                entity.Property(e => e.PetId)
                    .HasColumnName("PetID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ArrivalDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.ShelterId).HasColumnName("ShelterID");

                entity.Property(e => e.Species).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.HasOne(d => d.Shelter)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.ShelterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pet_shelterid_foreign");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.HasIndex(e => e.RoleName, "role_rolename_unique")
                    .IsUnique();

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Shelter>(entity =>
            {
                entity.ToTable("Shelter");

                entity.Property(e => e.ShelterId)
                    .HasColumnName("ShelterID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.ShelterAddress).HasMaxLength(255);

                entity.Property(e => e.ShelterName).HasMaxLength(255);

                entity.Property(e => e.ShelterPhoneNumber).HasMaxLength(255);

                entity.Property(e => e.UsersId).HasColumnName("UsersID");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Shelters)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("shelter_usersid_foreign");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "user_email_unique")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.PasswordHash).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber).HasMaxLength(255);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Status).HasMaxLength(255);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_roleid_foreign");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
