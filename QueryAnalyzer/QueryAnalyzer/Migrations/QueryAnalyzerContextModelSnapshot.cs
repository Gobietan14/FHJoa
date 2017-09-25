using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using QueryAnalyzer.Models;

namespace QueryAnalyzer.Migrations
{
    [DbContext(typeof(QueryAnalyzerContext))]
    partial class QueryAnalyzerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QueryAnalyzer.Models.Credential", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("ProjectKey");

                    b.Property<string>("Uri");

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.ToTable("Credential");
                });

            modelBuilder.Entity("QueryAnalyzer.Models.Issue", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int?>("ProjectID");

                    b.HasKey("ID");

                    b.HasIndex("ProjectID");

                    b.ToTable("Issue");
                });

            modelBuilder.Entity("QueryAnalyzer.Models.Project", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CredentialID");

                    b.HasKey("ID");

                    b.HasIndex("CredentialID");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("QueryAnalyzer.Models.Issue", b =>
                {
                    b.HasOne("QueryAnalyzer.Models.Project")
                        .WithMany("Issues")
                        .HasForeignKey("ProjectID");
                });

            modelBuilder.Entity("QueryAnalyzer.Models.Project", b =>
                {
                    b.HasOne("QueryAnalyzer.Models.Credential", "Credential")
                        .WithMany()
                        .HasForeignKey("CredentialID");
                });
        }
    }
}
