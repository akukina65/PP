using Microsoft.EntityFrameworkCore;

namespace WebApplication7.Models
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {


        }
        public DbSet<Superuser> superusers { get; set; }
        public DbSet<supercourses> supercourse { get; set; }
        public DbSet<teacherP> superteacherP { get; set; }
        public DbSet<userscoursesP> superuserscourses { get; set; }
        public DbSet<quizzesP> superquizzes { get; set; }
        public DbSet<quiz_resultsP> superquiz_results { get; set; }
        public DbSet<superlesson> superlessons { get; set; }
        public DbSet<course_categoriesP> supercourse_categories { get; set; }
        public DbSet<course_categoriescP> supercourse_categoriesc { get; set; }
        public DbSet<progressP> superprogress { get; set; }
        public DbSet<reviewsP> superreviews { get; set; }
        public DbSet<superorder> superorders { get; set; }
        public DbSet<purchasesP> superpurchases { get; set; }
       
        

    }
}
