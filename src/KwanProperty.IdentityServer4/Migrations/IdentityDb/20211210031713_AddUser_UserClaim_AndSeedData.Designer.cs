// <auto-generated />
using System;
using KwanProperty.IdentityServer4.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace KwanProperty.IdentityServer4.Migrations.IdentityDb
{
    [DbContext(typeof(IdentityDbContext))]
    [Migration("20211210031713_AddUser_UserClaim_AndSeedData")]
    partial class AddUser_UserClaim_AndSeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("KwanProperty.IdentityServer4.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Password")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("SecurityCode")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("SecurityCodeExpirationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Username")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.HasIndex("Subject")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Active = true,
                            ConcurrencyStamp = "7a8be433-fefa-40a5-a34e-be0496c26fde",
                            Password = "12345678",
                            SecurityCodeExpirationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Subject = "00000000-0000-0000-0000-000000000001",
                            Username = "Quan"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            Active = true,
                            ConcurrencyStamp = "00054514-c746-45ac-8dd2-f8ac0bcc6e89",
                            Password = "password",
                            SecurityCodeExpirationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Subject = "00000000-0000-0000-0000-000000000002",
                            Username = "Mai"
                        });
                });

            modelBuilder.Entity("KwanProperty.IdentityServer4.Entities.UserClaim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");

                    b.HasData(
                        new
                        {
                            Id = new Guid("572234ac-eaba-4499-be3c-37505f5cdafd"),
                            ConcurrencyStamp = "f24786aa-9548-4949-bd76-18a6cee1b9f5",
                            Type = "given_name",
                            UserId = new Guid("00000000-0000-0000-0000-000000000001"),
                            Value = "Quan"
                        },
                        new
                        {
                            Id = new Guid("a9ad0b31-15ca-4b5e-9ed7-38edaa14bab8"),
                            ConcurrencyStamp = "4f598f92-1115-4386-b1ce-6456b39bb406",
                            Type = "family_name",
                            UserId = new Guid("00000000-0000-0000-0000-000000000001"),
                            Value = "Tran Ngoc Quan"
                        },
                        new
                        {
                            Id = new Guid("14bb7184-9e57-42f5-b0d7-c4dbd380b740"),
                            ConcurrencyStamp = "9a8561fd-dc5a-4042-aaee-fb06288b777d",
                            Type = "email",
                            UserId = new Guid("00000000-0000-0000-0000-000000000001"),
                            Value = "tranngocquan95vn@gmail.com"
                        },
                        new
                        {
                            Id = new Guid("f0e8d4b9-de15-4a86-9523-7614c4bdac9f"),
                            ConcurrencyStamp = "ce30917c-184b-4d5a-a14b-fdf0ce6e805f",
                            Type = "address",
                            UserId = new Guid("00000000-0000-0000-0000-000000000001"),
                            Value = "Kham Thien"
                        },
                        new
                        {
                            Id = new Guid("13806fad-e083-4e71-9565-f2dcf702b012"),
                            ConcurrencyStamp = "a3b0a429-15ba-44ec-9f6f-c7037b6a47b9",
                            Type = "country",
                            UserId = new Guid("00000000-0000-0000-0000-000000000001"),
                            Value = "vn"
                        });
                });

            modelBuilder.Entity("KwanProperty.IdentityServer4.Entities.UserLogin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("ProviderIdentityKey")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("KwanProperty.IdentityServer4.Entities.UserSecret", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Secret")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserSecrets");
                });

            modelBuilder.Entity("KwanProperty.IdentityServer4.Entities.UserClaim", b =>
                {
                    b.HasOne("KwanProperty.IdentityServer4.Entities.User", "User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("KwanProperty.IdentityServer4.Entities.UserLogin", b =>
                {
                    b.HasOne("KwanProperty.IdentityServer4.Entities.User", "User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("KwanProperty.IdentityServer4.Entities.UserSecret", b =>
                {
                    b.HasOne("KwanProperty.IdentityServer4.Entities.User", "User")
                        .WithMany("Secrets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("KwanProperty.IdentityServer4.Entities.User", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("Logins");

                    b.Navigation("Secrets");
                });
#pragma warning restore 612, 618
        }
    }
}
