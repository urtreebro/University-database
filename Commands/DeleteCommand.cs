using System.Data.SQLite;

namespace University.Commands
{
    public class DeleteCommand
    {
        public static void Delete(SQLiteConnection connection)
        {
            Console.WriteLine("Что вы хотите удалить: student, teacher, course, exam?");
            string? command = Console.ReadLine();
            switch (command)
            {
                case "student":
                    DeleteStudent(connection);
                    break;
                case "teacher":
                    DeleteTeacher(connection);
                    break;
                case "course":
                    DeleteCourse(connection);
                    break;
                case "exam":
                    DeleteExam(connection);
                    break;
                default:
                    Console.WriteLine("Неизвестные данные.");
                    break;
            }
        }
        private static void DeleteStudent(SQLiteConnection connection)
        {
            Console.WriteLine("Введите фамилию и имя студента, которого вы хотите удалить. (в отдельных строчках)");
            string? surname = Console.ReadLine();
            string? name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(surname) &&
                !string.IsNullOrWhiteSpace(name))
            {
                StudentsTable.DeleteStudent(connection, surname, name);
            }
            else Console.WriteLine("Неправильно введенные данные.");
        }

        private static void DeleteTeacher(SQLiteConnection connection)
        {
            Console.WriteLine("Введите фамилию и имя преподавателя, которого вы хотите удалить. (в отдельных строчках)");
            string? surname = Console.ReadLine();
            string? name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(surname) &&
                !string.IsNullOrWhiteSpace(name))
            {
                TeachersTable.DeleteTeacher(connection, surname, name);
            }
            else Console.WriteLine("Неправильно введенные данные.");
        }
        private static void DeleteCourse(SQLiteConnection connection)
        {
            Console.WriteLine("Введите название курса, которого вы хотите удалить.");
            string? title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                CoursesTable.DeleteCourse(connection, title);
            }
            else Console.WriteLine("Неправильно введенные данные.");
        }

        private static void DeleteExam(SQLiteConnection connection)
        {
            Console.WriteLine("Введите дату экзамена, которого вы хотите удалить.");
            string? date = Console.ReadLine();
            if (DateOnly.TryParse(date, out DateOnly valid_date))
            {
                ExamsTable.DeleteExam(connection, valid_date);
            }
            else Console.WriteLine("Неправильно введенные данные.");
        }
    }
}
