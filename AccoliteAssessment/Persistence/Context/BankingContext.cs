using AccoliteAssessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccoliteAssessment.Persistence.Context
{
    public class BankingContext : DbContext
    {

        public BankingContext(DbContextOptions<BankingContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().ToTable("User", "dbo");

            modelBuilder.Entity<UserAccountModel>().ToTable("UserAccount", "dbo")
                .HasOne(oc => oc.User).WithMany(oc => oc.UserAccounts).HasForeignKey(oc => oc.UserId).HasConstraintName("FK_UserAccount_User").OnDelete(DeleteBehavior.Cascade);

            Guid user1 = Guid.NewGuid();
            Guid user2 = Guid.NewGuid();
            Guid acc1 = Guid.NewGuid();
            Guid acc2 = Guid.NewGuid();

            modelBuilder.Entity<UserModel>().ToTable("User", "dbo").HasData(
                new UserModel
                {
                    Id = user1,
                    FirstName = "Srashti",
                    LastName = "Gupta",
                    Email = "test@gmail.com",
                    Age = 25,
                    Sex = "F",
                    Phone = 123456
                },
                new UserModel
                {
                    Id = user2,
                    FirstName = "Mukesh",
                    LastName = "Shukla",
                    Email = "test@outlook.com",
                    Age = 25,
                    Sex = "M",
                    Phone = 123456
                });

            modelBuilder.Entity<UserAccountModel>().ToTable("UserAccount", "dbo").HasData(
                new UserAccountModel()
                {
                    Id = acc1,
                    Balance = 100,
                    Currency = "USD",
                    UserId = user1,
                    CreatedAt = DateTime.UtcNow

                },
                new UserAccountModel()
                {
                    Id = acc2,
                    Balance = 1000,
                    Currency = "USD",
                    UserId = user2,
                    CreatedAt = DateTime.UtcNow
                });

        }
        public virtual DbSet<UserModel> User { get; set; }
        public virtual DbSet<UserAccountModel> UserAccount { get; set; }
    }
    }
