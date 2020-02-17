﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Social.Api.Data;

namespace Social.Api.Data.Migrations
{
    [DbContext(typeof(SocialApiContext))]
    partial class SocialApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Social.Api.Data.Model.AssetEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CommentEntityId")
                        .HasColumnType("int");

                    b.Property<string>("Hash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PostEntityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommentEntityId");

                    b.HasIndex("PostEntityId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("Social.Api.Data.Model.CommentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CommentEntityId")
                        .HasColumnType("int");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.Property<int?>("PostEntityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommentEntityId");

                    b.HasIndex("PostEntityId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Social.Api.Data.Model.PostEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Social.Api.Data.Model.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Social.Api.Data.Model.AssetEntity", b =>
                {
                    b.HasOne("Social.Api.Data.Model.CommentEntity", null)
                        .WithMany("Assets")
                        .HasForeignKey("CommentEntityId");

                    b.HasOne("Social.Api.Data.Model.PostEntity", null)
                        .WithMany("Assets")
                        .HasForeignKey("PostEntityId");
                });

            modelBuilder.Entity("Social.Api.Data.Model.CommentEntity", b =>
                {
                    b.HasOne("Social.Api.Data.Model.CommentEntity", null)
                        .WithMany("Replies")
                        .HasForeignKey("CommentEntityId");

                    b.HasOne("Social.Api.Data.Model.PostEntity", null)
                        .WithMany("Comments")
                        .HasForeignKey("PostEntityId");
                });

            modelBuilder.Entity("Social.Api.Data.Model.PostEntity", b =>
                {
                    b.HasOne("Social.Api.Data.Model.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
