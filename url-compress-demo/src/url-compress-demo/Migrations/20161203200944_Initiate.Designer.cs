using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using url_compress_demo.Persistance;

namespace urlcompressdemo.Migrations
{
    [DbContext(typeof(CompressorContext))]
    [Migration("20161203200944_Initiate")]
    partial class Initiate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("url_compress_demo.Models.CompressedUrl", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("ClickCount");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("SourceUrl");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Urls");
                });
        }
    }
}
