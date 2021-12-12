
using KwanProperty.IdentityServer4.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KwanProperty.IdentityServer4.DbContexts
{
    public class IdentityDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserClaim> UserClaims { get; set; }

        public DbSet<UserLogin> UserLogins { get; set; }

        public DbSet<UserSecret> UserSecrets { get; set; }

        public IdentityDbContext(
          DbContextOptions<IdentityDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Subject)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            //modelBuilder.Entity<User>().HasData(
            //    new User()
            //    {
            //        Id = new Guid("00000000-0000-0000-0000-000000000001"),
            //        Password = "12345678",
            //        Subject = "00000000-0000-0000-0000-000000000001",
            //        Username = "Quan",
            //        Active = true
            //    },
            //    new User()
            //    {
            //        Id = new Guid("00000000-0000-0000-0000-000000000002"),
            //        Password = "password",
            //        Subject = "00000000-0000-0000-0000-000000000002",
            //        Username = "Mai",
            //        Active = true
            //    }
            //);

            //modelBuilder.Entity<UserClaim>().HasData(
            //    new UserClaim()
            //    {
            //        Id = Guid.NewGuid(),
            //        UserId = new Guid("00000000-0000-0000-0000-000000000001"),
            //        Type = "given_name",
            //        Value = "Quan"
            //    },
            //    new UserClaim()
            //    {
            //        Id = Guid.NewGuid(),
            //        UserId = new Guid("00000000-0000-0000-0000-000000000001"),
            //        Type = "family_name",
            //        Value = "Tran Ngoc Quan"
            //    },
            //    new UserClaim()
            //    {
            //        Id = Guid.NewGuid(),
            //        UserId = new Guid("00000000-0000-0000-0000-000000000001"),
            //        Type = "email",
            //        Value = "tranngocquan95vn@gmail.com"
            //    },
            //    new UserClaim()
            //    {
            //        Id = Guid.NewGuid(),
            //        UserId = new Guid("00000000-0000-0000-0000-000000000001"),
            //        Type = "address",
            //        Value = "Kham Thien"
            //    },
            //    new UserClaim()
            //    {
            //        Id = Guid.NewGuid(),
            //        UserId = new Guid("00000000-0000-0000-0000-000000000001"),
            //        Type = "country",
            //        Value = "vn"
            //    }
            //);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // get updated entries
            var updatedConcurrencyAwareEntries = ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Modified)
                    .OfType<IConcurrencyAware>();

            foreach (var entry in updatedConcurrencyAwareEntries)
            {
                entry.ConcurrencyStamp = Guid.NewGuid().ToString();
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
