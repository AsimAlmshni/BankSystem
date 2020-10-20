using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bank.Models
{
    public partial class BankContext : DbContext
    {
        public BankContext()
        {
        }

        public BankContext(DbContextOptions<BankContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountActionHistory> AccountActionHistory { get; set; }
        public virtual DbSet<AccountTypes> AccountTypes { get; set; }
        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Currencies> Currencies { get; set; }
        public virtual DbSet<AccountTypesDataSet> AccountTypesDataSet { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=ZAYED-PC;Initial Catalog=Bank;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountActionHistory>(entity =>
            {
                entity.HasKey(e => e.Aahid)
                    .HasName("PK__AccountA__B8998A68BCB599E0");

                entity.Property(e => e.Aahid).HasColumnName("AAHID");

                entity.Property(e => e.AccountsAccId).HasColumnName("AccountsAccID");

                entity.Property(e => e.ActionType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.FromAccount)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ToAccount)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.AccountsAcc)
                    .WithMany(p => p.AccountActionHistory)
                    .HasForeignKey(d => d.AccountsAccId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKAccountAct322473");
            });

            modelBuilder.Entity<AccountTypes>(entity =>
            {
                entity.HasKey(e => e.AccTypId)
                    .HasName("PK__AccountT__C8C4FAF870511EC0");

                entity.Property(e => e.AccTypId).HasColumnName("AccTypID");

                entity.Property(e => e.AccIdtyp).HasColumnName("AccIDTyp");

                entity.Property(e => e.AccountType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.AccIdtypNavigation)
                    .WithMany(p => p.AccountTypes)
                    .HasForeignKey(d => d.AccIdtyp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("account have many types");
            });

            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.HasKey(e => e.AccId)
                    .HasName("PK__Accounts__91CBC398AD69A424");

                entity.HasIndex(e => e.AccountNumber)
                    .HasName("UQ__Accounts__BE2ACD6F6CC32088")
                    .IsUnique();

                entity.Property(e => e.AccId).HasColumnName("AccID");

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customer could have many accounts");
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.Property(e => e.BankId).HasColumnName("BankID");

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.CliId)
                    .HasName("PK__Client__FA1E28BBEF86C4FB");

                entity.Property(e => e.CliId).HasColumnName("CliID");

                entity.Property(e => e.BankId).HasColumnName("BankID");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Bank has");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.MainAccountNumber)
                    .HasName("UQ__Customer__30AFE9E55ACD81E7")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.AccId).HasColumnName("AccID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MainAccountNumber)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MainCurrency)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("client serve more than one customer ");
            });

            modelBuilder.Entity<Currencies>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccountTypesDataSet>(entity =>
            {
                entity.HasKey(e => e.AcctypDs)
                    .HasName("PK__AccountT__0F8D552E6001683A");

                entity.Property(e => e.AcctypDs).HasColumnName("AcctypDS");

                entity.Property(e => e.AccountType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}