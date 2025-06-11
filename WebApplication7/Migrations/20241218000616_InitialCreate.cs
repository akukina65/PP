using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication7.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "supercourse",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        title = table.Column<string>(type: "text", nullable: false),
            //        description = table.Column<string>(type: "text", nullable: false),
            //        duration = table.Column<int>(type: "integer", nullable: false),
            //        id_teacher = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_supercourse", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "supercourse_categories",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        name = table.Column<string>(type: "text", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_supercourse_categories", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "supercourse_categoriesc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_courses = table.Column<int>(type: "integer", nullable: false),
                    id_course_categories = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supercourse_categoriesc", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "superlessons",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        id_courses = table.Column<int>(type: "integer", nullable: false),
            //        lessonname = table.Column<string>(type: "text", nullable: false),
            //        lessondescription = table.Column<string>(type: "text", nullable: false),
            //        lessoncontent = table.Column<string>(type: "text", nullable: false),
            //        quantity = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_superlessons", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "superorders",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        id_users = table.Column<int>(type: "integer", nullable: false),
            //        order_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //        price = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_superorders", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "superprogress",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        id_users = table.Column<int>(type: "integer", nullable: false),
            //        name = table.Column<string>(type: "text", nullable: false),
            //        lessonid = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_superprogress", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "superpurchases",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        id_courses = table.Column<int>(type: "integer", nullable: false),
            //        id_orders = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_superpurchases", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "superquiz_results",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        id_quizzes = table.Column<int>(type: "integer", nullable: false),
            //        id_users = table.Column<int>(type: "integer", nullable: false),
            //        completeddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //        totalscore = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_superquiz_results", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "superquizzes",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        name = table.Column<string>(type: "text", nullable: false),
            //        the_survey = table.Column<string>(type: "text", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_superquizzes", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "superreviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reviews = table.Column<string>(type: "text", nullable: false),
                    date_of_review = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    id_users = table.Column<int>(type: "integer", nullable: false),
                    id_courses = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_superreviews", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "superteacherP",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        surname = table.Column<string>(type: "text", nullable: false),
            //        name = table.Column<string>(type: "text", nullable: false),
            //        patronymic = table.Column<string>(type: "text", nullable: false),
            //        telephone = table.Column<string>(type: "text", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_superteacherP", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "superusers",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        surname = table.Column<string>(type: "text", nullable: false),
            //        name = table.Column<string>(type: "text", nullable: false),
            //        patronymic = table.Column<string>(type: "text", nullable: false),
            //        password = table.Column<string>(type: "text", nullable: false),
            //        email = table.Column<string>(type: "text", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_superusers", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "superuserscourses",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        id_users = table.Column<int>(type: "integer", nullable: false),
            //        id_courses = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_superuserscourses", x => x.Id);
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "supercourse");

            //migrationBuilder.DropTable(
            //    name: "supercourse_categories");

            migrationBuilder.DropTable(
                name: "supercourse_categoriesc");

            //migrationBuilder.DropTable(
            //    name: "superlessons");

            //migrationBuilder.DropTable(
            //    name: "superorders");

            //migrationBuilder.DropTable(
            //    name: "superprogress");

            //migrationBuilder.DropTable(
            //    name: "superpurchases");

            //migrationBuilder.DropTable(
            //    name: "superquiz_results");

            //migrationBuilder.DropTable(
            //    name: "superquizzes");

            migrationBuilder.DropTable(
                name: "superreviews");

            //migrationBuilder.DropTable(
            //    name: "superteacherP");

            //migrationBuilder.DropTable(
            //    name: "superusers");

            //migrationBuilder.DropTable(
            //    name: "superuserscourses");
        }
    }
}
