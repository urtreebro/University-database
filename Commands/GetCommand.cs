using System.Data.SQLite;

namespace University.Commands
{
    public class GetCommand
    {
        public static void Get(SQLiteConnection connection)
        {
            Console.WriteLine("Какие данные вы хотите получить: students, teachers, courses, exams, grades?");
            string? command = Console.ReadLine();
            switch (command)
            {
                case "students":
                    GetStudent(connection);
                    break;
                case "teachers":
                    GetTeacher(connection);
                    break;
                case "courses":
                    GetCourse(connection);
                    break;
                case "exams":
                    GetExam(connection);
                    break;
                case "grades":
                    GetGrade(connection);
                    break;
                default:
                    Console.WriteLine("Неизвестные данные.");
                    break;
            }
        }
        private static void GetStudent(SQLiteConnection connection)
        {
            Console.WriteLine("По какому признаку?\nall - все студенты\ncourse - по курсу\ndepartment - по факультету");
            string? attribute = Console.ReadLine();
            if (attribute == "all")
            {
                StudentsTable.GetStudents(connection, attribute, attribute);
            }
            else if (attribute == "course" || attribute == "department")
            {
                Console.WriteLine("Введите название курса или факультета.");
                string? name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    StudentsTable.GetStudents(connection, attribute, name);
                }
                else
                {
                    Console.WriteLine("Неправильно введенное название.");
                }
            }
            else
            {
                Console.WriteLine("Неизвестный признак.");
            }
        }
        private static void GetTeacher(SQLiteConnection connection)
        {
            TeachersTable.GetTeachers(connection);
        }

        private static void GetCourse(SQLiteConnection connection)
        {
            Console.WriteLine("По какому признаку?\nall - все курсы\nteacher - по преподавателю, читающему курс");
            string? attribute = Console.ReadLine();
            if (attribute == "all")
            {
                CoursesTable.GetCourses(connection);
            }
            else if (attribute == "teacher")
            {
                Console.WriteLine("Введите фамилию и имя преподавателя. (в отдельных строчках)");
                string? surname = Console.ReadLine();
                string? name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Неправильно введенные данные.");
                    return;
                }
                CoursesTable.GetByTeacher(connection, surname, name);
            }
            else Console.WriteLine("Неизвестный признак.");
        }

        private static void GetExam(SQLiteConnection connection)
        {
            ExamsTable.GetExams(connection);
        }

        private static void GetGrade(SQLiteConnection connection)
        {
            Console.WriteLine("По какому признаку?\nall - все оценки\ncourse - по курсу");
            string? attribute = Console.ReadLine();
            if (attribute == "all")
            {
                GradesTable.GetGrades(connection);  
            }
            else if (attribute == "course")
            {
                Console.WriteLine("Введите название курса.");
                string? title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Неправильно введенные данные.");
                    return;
                }
                GradesTable.GetGradesByCourse(connection, title);
            }
            else Console.WriteLine("Неизвестный признак.");
        }
    }
}
