using System.Data.SQLite;
using System.Xml.Linq;

namespace University.Commands
{
    public class AverageCommand
    {
        public static void Average(SQLiteConnection connection)
        {
            Console.WriteLine("Что вы хотите получить?\ncourse - средний балл студента по курсу\nstudent - средний балл студента\ndepartment - средний балл факультета");
            string? command = Console.ReadLine();
            switch (command)
            {
                case "course":
                    AverageByCourse(connection);
                    break;
                case "student":
                    AverageByStudent(connection);
                    break;
                case "department":
                    AverageByDepartment(connection);
                    break;
                default:
                    Console.WriteLine("Неизвестные данные.");
                    break;
            }
        }
        
        private static void AverageByCourse(SQLiteConnection connection)
        {
            Console.WriteLine("Введите название курса.");
            string? title = Console.ReadLine();
            if (!string.IsNullOrEmpty(title))
            {
                AverageByCourse(connection, title);
            }
            else Console.WriteLine("Неправильно введенные данные.");
        }
        private static void AverageByCourse(SQLiteConnection connection, string title)
        {
            using (var command = new SQLiteCommand("SELECT avg(score) " +
                "FROM Grades g " +
                "JOIN Students s on g.student_id = s.id " +
                "JOIN Exams e on e.id = g.exam_id " +
                "JOIN Courses c on c.id = e.course_id " +
                "WHERE c.title = @title", connection))
            {
                command.Parameters.AddWithValue("@title", title);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Средний балл по курсу: {reader["avg(score)"]}");
                    }
                }
            }
        }

        private static void AverageByStudent(SQLiteConnection connection)
        {
            Console.WriteLine("Введите фамилию и имя студента. (в отдельных строчках)");
            string? surname = Console.ReadLine();
            string? name = Console.ReadLine();
            if (!string.IsNullOrEmpty(surname) && !string.IsNullOrEmpty(name))
            {
                AverageByStudent(connection, surname, name);
            }
            else Console.WriteLine("Неправильно введенные данные.");
        }

        private static void AverageByStudent(SQLiteConnection connection, string surname, string name)
        {
            using (var command = new SQLiteCommand("SELECT avg(score) " +
                "FROM Grades g " +
                "JOIN Students s on g.student_id = s.id " +
                "WHERE s.name = @name AND s.surname = @surname", connection))
            {
                command.Parameters.AddWithValue("@surname", surname);
                command.Parameters.AddWithValue("@name", name);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Средний балл студента: {reader["avg(score)"]}");
                    }
                }
            }
        }

        private static void AverageByDepartment(SQLiteConnection connection)
        {
            Console.WriteLine("Введите название факультета.");
            string? department = Console.ReadLine();
            if (!string.IsNullOrEmpty(department))
            {
                AverageByDepartment(connection, department);
            }
            else Console.WriteLine("Неправильно введенные данные.");
        }

        private static void AverageByDepartment(SQLiteConnection connection, string department)
        {
            using (var command = new SQLiteCommand("SELECT avg(score) " +
                "FROM Grades g " +
                "JOIN Students s on g.student_id = s.id " +
                "WHERE s.department = @department", connection))
            {
                command.Parameters.AddWithValue("@department", department);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Средний балл по факультету: {reader["avg(score)"]}");
                    }
                }
            }
        }
    }
}
