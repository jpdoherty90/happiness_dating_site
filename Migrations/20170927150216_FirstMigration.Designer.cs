using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Match.Models;
using System.Collections.Generic;

namespace Match.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20170927150216_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Match.Models.Like", b =>
                {
                    b.Property<int>("LikeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PersonLikedId");

                    b.Property<int>("PersonLikingId");

                    b.HasKey("LikeId");

                    b.HasIndex("PersonLikedId");

                    b.HasIndex("PersonLikingId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Match.Models.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<int?>("RecieverId");

                    b.Property<int>("RevieverId");

                    b.Property<int>("SenderId");

                    b.Property<DateTime>("SentAt");

                    b.HasKey("MessageId");

                    b.HasIndex("RecieverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Match.Models.Preference", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<List<string>>("Trump");

                    b.Property<List<string>>("build");

                    b.Property<bool>("chipotle");

                    b.Property<List<string>>("cigarettes");

                    b.Property<List<string>>("diet");

                    b.Property<List<string>>("drinking");

                    b.Property<List<string>>("education");

                    b.Property<List<string>>("ethnicity");

                    b.Property<List<string>>("exercise");

                    b.Property<List<string>>("future_kids");

                    b.Property<List<int>>("height");

                    b.Property<List<string>>("history");

                    b.Property<List<string>>("horoscope");

                    b.Property<List<string>>("interests");

                    b.Property<List<string>>("marijuana");

                    b.Property<int>("max_age");

                    b.Property<int>("memes");

                    b.Property<int>("min_age");

                    b.Property<List<string>>("netflix");

                    b.Property<List<string>>("pets");

                    b.Property<List<string>>("present_kids");

                    b.Property<List<string>>("religion");

                    b.Property<List<int>>("salary");

                    b.Property<List<string>>("sex");

                    b.Property<bool>("tattoos");

                    b.HasKey("id");

                    b.ToTable("Preferences");
                });

            modelBuilder.Entity("Match.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<List<int>>("MatchIds");

                    b.Property<List<int>>("MatchPercentages");

                    b.Property<int>("PreferenceId");

                    b.Property<string>("Trump");

                    b.Property<int>("age");

                    b.Property<string>("build");

                    b.Property<bool>("chipotle");

                    b.Property<string>("cigarettes");

                    b.Property<string>("diet");

                    b.Property<string>("drinking");

                    b.Property<string>("education");

                    b.Property<string>("email");

                    b.Property<string>("ethnicity");

                    b.Property<string>("exercise");

                    b.Property<string>("future_kids");

                    b.Property<string>("gender");

                    b.Property<int>("height");

                    b.Property<string>("history");

                    b.Property<string>("horoscope");

                    b.Property<List<string>>("interests");

                    b.Property<string>("marijuana");

                    b.Property<int>("memes");

                    b.Property<string>("name");

                    b.Property<List<string>>("netflix");

                    b.Property<string>("password");

                    b.Property<string>("pets");

                    b.Property<string>("present_kids");

                    b.Property<string>("religion");

                    b.Property<int>("salary");

                    b.Property<string>("seeking");

                    b.Property<string>("sex");

                    b.Property<bool>("tattoos");

                    b.Property<string>("username");

                    b.Property<int>("zipcode");

                    b.HasKey("UserId");

                    b.HasIndex("PreferenceId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Match.Models.Like", b =>
                {
                    b.HasOne("Match.Models.User", "PersonLiked")
                        .WithMany("likers")
                        .HasForeignKey("PersonLikedId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Match.Models.User", "PersonLiking")
                        .WithMany("likes")
                        .HasForeignKey("PersonLikingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Match.Models.Message", b =>
                {
                    b.HasOne("Match.Models.User", "Reciever")
                        .WithMany("messagesRecieved")
                        .HasForeignKey("RecieverId");

                    b.HasOne("Match.Models.User", "Sender")
                        .WithMany("messagesSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Match.Models.User", b =>
                {
                    b.HasOne("Match.Models.Preference", "Preference")
                        .WithMany()
                        .HasForeignKey("PreferenceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
