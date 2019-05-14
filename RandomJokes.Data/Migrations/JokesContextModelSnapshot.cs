﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RandomJokes.Data;

namespace RandomJokes.Data.Migrations
{
    [DbContext(typeof(JokesContext))]
    partial class JokesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RandomJokes.Data.Joke", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OriginId");

                    b.Property<string>("Punchline");

                    b.Property<string>("Setup");

                    b.HasKey("Id");

                    b.ToTable("Jokes");
                });

            modelBuilder.Entity("RandomJokes.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("PasswordHash");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RandomJokes.Data.UserJokeLike", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("JokeId");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("Like");

                    b.HasKey("UserId", "JokeId");

                    b.HasIndex("JokeId");

                    b.ToTable("UserJokeLikes");
                });

            modelBuilder.Entity("RandomJokes.Data.UserJokeLike", b =>
                {
                    b.HasOne("RandomJokes.Data.Joke", "Joke")
                        .WithMany("UserJokeLikes")
                        .HasForeignKey("JokeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RandomJokes.Data.User", "User")
                        .WithMany("UserJokeLikes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}