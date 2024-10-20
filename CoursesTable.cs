using System.Data.SQLite;

namespace University
{
    public class CoursesTable
    {
        public static void CreateTableCourses(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Courses (
                id INTEGER PRIMARY KEY AUTOINCREMENT, 
                title TEXT NOT NULL,
                description TEXT NOT NULL,
                teacher_id INTEGER NOT NULL,
                FOREIGN KEY(teacher_id) REFERENCES Teachers(id)
            );";
                command.ExecuteNonQuery();
            }
        }
        public static void InsertCourse(SQLiteConnection connection, string title, string description, int teacher_id)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "INSERT INTO Courses (title, description, teacher_id) VALUES (@title, @description, @teacher_id)";
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@teacher_id", teacher_id);
                command.ExecuteNonQuery();
                Console.WriteLine($"Курс {title} добавлен.");
            }
        }
        public static void GetCourses(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand("SELECT * FROM Courses", connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["id"]}, Название: {reader["title"]}, Описание: {reader["description"]}, ID преподавателя: {reader["teacher_id"]}");
                }
            }
        }

        public static void GetByTeacher(SQLiteConnection connection, string surname, string name)
        {
            Console.WriteLine("Данные в таблице 'Courses':");
            using (var command = new SQLiteCommand("SELECT * FROM Courses as c " +
                "JOIN Teachers as t on c.teacher_id == t.id " +
                "WHERE surname = @surname and name = @name", connection))
            {
                command.Parameters.AddWithValue("@surname", surname);
                command.Parameters.AddWithValue("@name", name);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Название: {reader["title"]}, " +
                            $"Описание: {reader["description"]}, " +
                            $"ID преподавателя: {reader["teacher_id"]}, " +
                            $"Фамилия преподавателя: {reader["surname"]}, " +
                            $"Имя преподавателя: {reader["name"]}");
                    }
                }
            }
        }
        public static void DeleteCourse(SQLiteConnection connection, string title)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "DELETE FROM Courses WHERE title = @title";
                command.Parameters.AddWithValue("@title", title);
                command.ExecuteNonQuery();
                Console.WriteLine($"Курс '{title}' успешно удален.");
            }
        }

        public static void UpdateCourseInfo(SQLiteConnection connection, string title, string attribute, string newinfo)
        {
            switch (attribute)
            {
                case "title":
                    UpdateCourseTitle(connection, title, newinfo);
                    break;
                case "description":
                    UpdateCourseDescription(connection, title, newinfo);
                    break;
                default:
                    break;
            }
        }

        public static void UpdateCourseInfo(SQLiteConnection connection, string title, int id)
        {
            UpdateCourseTeacherID(connection, title, id);
        }

        private static void UpdateCourseTitle(SQLiteConnection connection, string title, string newtitle)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Courses SET title = @newtitle WHERE title = @title";
                command.Parameters.AddWithValue("@newtitle", newtitle);
                command.Parameters.AddWithValue("@title", title);
                command.ExecuteNonQuery();
                Console.WriteLine($"Название курса '{newtitle}' успешно обновлено.");
            }
        }

        private static void UpdateCourseDescription(SQLiteConnection connection, string title, string newdescription)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Courses SET description = @newdescription WHERE title = @title";
                command.Parameters.AddWithValue("@newdescription", newdescription);
                command.Parameters.AddWithValue("@title", title);
                command.ExecuteNonQuery();
                Console.WriteLine($"Описание курса '{title}' успешно обновлено.");
            }
        }

        private static void UpdateCourseTeacherID(SQLiteConnection connection, string title, int newid)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Courses SET teacher_id = @newid WHERE title = @title";
                command.Parameters.AddWithValue("@newid", newid);
                command.Parameters.AddWithValue("@title", title);
                command.ExecuteNonQuery();
                Console.WriteLine($"Преподаватель, читающий курс '{title}', успешно обновлен.");
            }
        }
    }
}
