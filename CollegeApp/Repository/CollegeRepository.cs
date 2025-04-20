using CollegeApp.Models;

namespace CollegeApp.Repository
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>(){
                new Student
                {
                    Id = 1,
                    StudentName = "Mohamed",
                    Email = "student1@gmail.com",
                    Address = "Egypt, Cairo",
                },
                new Student
                {
                    Id = 2,
                    StudentName = "Ahmed",
                    Email = "student2@gmail.com",
                    Address = "Egypt, Cairo",
                },
                new Student
                {
                    Id = 3,
                    StudentName = "Ali",
                    Email = "student3@gmail.com",
                    Address = "Egypt, Cairo",
                },
        };
    }
}
