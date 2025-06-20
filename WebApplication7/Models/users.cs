namespace WebApplication7.Models
{
    public class Superuser
    {
        public int Id { get; set; }
        public required string surname { get; set; }
        public string name { get; set; } = string.Empty;
        public string? patronymic { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string Role { get; set; } = "student"; // Новое поле для роли
        public string? AvatarUrl { get; set; } // Добавляем URL аватара
    }
    public class supercourses
    {
        public int Id { get; set; }
        public required string title { get; set; }
        public string description { get; set; } = string.Empty;
        public int duration { get; set; }
        public int id_teacher { get; set; }

    }
    public class teacherP
    {
       public int Id { get; set; }
        public required string surname { get; set; }
        public string name { get; set; } = string.Empty;
        public string patronymic { get; set; } = string.Empty;
        public string telephone { get; set; } = string.Empty;
        


    }
    public class userscoursesP
    {
        public int Id { get; set; }
        public int id_users { get; set; }
        public int id_courses { get; set; }
        



    }
    public class quizzesP
    {
        public int Id { get; set; }
        public string name { get; set; } = string.Empty;
        public string the_survey { get; set; } = string.Empty;




    }
    public class quiz_resultsP
    {
        public int Id { get; set; }
        public int id_quizzes { get; set; } 
        public int id_users { get; set; } 
        public DateTime completeddate { get; set; } 
        public int totalscore { get; set; } 


    }
    public class superlesson
    {
        public int Id { get; set; }
        public int id_courses { get; set; }
        public string lessonname { get; set; } = string.Empty;
        public string lessondescription { get; set; } = string.Empty;
        public string lessoncontent { get; set; } = string.Empty;
        public int quantity { get; set; }


    }
    public class course_categoriesP
    {
        public int Id { get; set; }
        public string name { get; set; } = string.Empty;
        


    }
    public class course_categoriescP
    {
        public int Id { get; set; }
        public int  id_courses { get; set; } 
        public int id_course_categories { get; set; }


    }
    public class progressP
    {
        public int Id { get; set; }
        public int id_users { get; set; }
        public string name { get; set; } = string.Empty;
        public int lessonid { get; set; }

    }
    public class reviewsP
    {
        public int Id { get; set; }
        public string reviews { get; set; } = string.Empty;
        public DateTime date_of_review { get; set; } 
        public int id_users { get; set; }
        public int id_courses { get; set; }

    }
    public class superorder
    {
        public int Id { get; set; }
        public int id_users { get; set; } 
        public DateTime order_date { get; set; }
        public int price { get; set; }
        

    }
    public class purchasesP
    {
        public int Id { get; set; }
        public int id_courses { get; set; }
        public int id_orders { get; set; }


    }

    public class CourseWithTeacherDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string TeacherFIO { get; set; }
    }
    public class CourseWithTRewDTO
    {
        public int Id { get; set; }
        public string reviews { get; set; } = string.Empty;
        public DateTime date_of_review { get; set; }
        public string UserFIO { get; set; }
        public string Course { get; set; }
    }
    public class LessonWithTRewDTO
    {
        public int Id { get; set; }
        public string Course { get; set; }
        public string lessonname { get; set; } = string.Empty;
        public string lessondescription { get; set; } = string.Empty;
        public string lessoncontent { get; set; } = string.Empty;
        public int quantity { get; set; }
    }
    public class ProgressWithTRewDTO
    {
        public int Id { get; set; }
        public string UserFIO { get; set; }
        public string name { get; set; } = string.Empty;
        public string lesson { get; set; }
    }
    public class ResultWithTRewDTO
    {
        public int Id { get; set; }
        public string Test { get; set; }
        public string UserFIO { get; set; }
        public DateTime completeddate { get; set; }
        public int totalscore { get; set; }
    }
    public class OrdertWithTRewDTO
    {
        public int Id { get; set; }
        public string UserFIO { get; set; }
        public DateTime order_date { get; set; }
        public int price { get; set; }
    }
    public class PuchestWithTRewDTO
    {
        public int Id { get; set; }
        public string Course { get; set; }
        public int id_orders { get; set; }
    }
    public class UserCoursetWithTRewDTO
    {
        public int Id { get; set; }
        public string UserFIO { get; set; }
        public string Course { get; set; }
    }
    public class CoursecategoriatWithTRewDTO
    {
        public int Id { get; set; }
        public string Course { get; set; }
        public string Categoria { get; set; }
    }
    public class superuserdt
    {
        public int Id { get; set; }
        public required string surname { get; set; }
        public required string name { get; set; } = string.Empty;
        public required string patronymic { get; set; } = string.Empty;
        public required string password { get; set; } = string.Empty;
        public required string email { get; set; } = string.Empty;
    }
}
