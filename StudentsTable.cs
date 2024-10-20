using System.Data.SQLite;
using System.Xml.Linq;

namespace University
{
    public class StudentsTable
    {
        public static void CreateTableStudents(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Students (
                id INTEGER PRIMARY KEY AUTOINCREMENT,  -- id студента, первичный ключ с автоинкрементом
                name TEXT NOT NULL,                     -- Имя студента
                surname TEXT NOT NULL,                  -- Фамилия студента
                department TEXT NOT NULL,               -- Факультет студента
                date_of_birth DATEONLY NOT NULL            -- Дата рождения студента
            );";
                command.ExecuteNonQuery();
            }
        }

        public static void InsertStudent(SQLiteConnection connection, string surname, string name, string department, DateOnly date_of_birth)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "INSERT INTO Students (name, surname, department, date_of_birth) VALUES (@name, @surname, @department, @date_of_birth)";
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@surname", surname);
                command.Parameters.AddWithValue("@department", department);
                command.Parameters.AddWithValue("@date_of_birth", date_of_birth);
                command.ExecuteNonQuery();
                Console.WriteLine($"Студент {surname} {name} добавлен.");
            }
        }
        public static void GetStudents(SQLiteConnection connection, string attribute, string name) {
            switch (attribute) {
                case "all":
                    GetAllStudents(connection); break;
                case "course":
                    GetByCourse(connection, name); break;
                case "department":
                    GetByDepartment(connection, name); break;
                default:
                    break;
            }
        }

        private static void GetAllStudents(SQLiteConnection connection)
        {
            Console.WriteLine("Данные в таблице 'Students':");
            using (var command = new SQLiteCommand("SELECT * FROM Students", connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["id"]}, Фамилия: {reader["surname"]}, Имя: {reader["name"]}, Факультет: {reader["department"]}, Дата рождения: {reader["date_of_birth"]}");
                }
            }
        }

        private static void GetByCourse(SQLiteConnection connection, string courseTitle) 
        {
            Console.WriteLine("Данные в таблице 'Students':");
            using (var command = new SQLiteCommand("SELECT * FROM Students s " +
                "JOIN Grades g ON g.student_id = s.id " +
                "JOIN Exams e ON e.id = g.exam_id " +
                "JOIN Courses c ON c.id = e.course_id " +
                "WHERE c.title = @title", connection))
            {
                command.Parameters.AddWithValue("@title", courseTitle);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Фамилия: {reader["surname"]}, Имя: {reader["name"]}, Факультет: {reader["department"]}, Дата рождения: {reader["date_of_birth"]}, Название курса: {courseTitle}");
                    }
                }
            }
        }

        private static void GetByDepartment(SQLiteConnection connection, string department)
        {
            Console.WriteLine("Данные в таблице 'Students':");
            using (var command = new SQLiteCommand("SELECT * FROM Students WHERE @department = department", connection))
            {
                command.Parameters.AddWithValue("@department", department);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Фамилия: {reader["surname"]}, Имя: {reader["name"]}, Факультет: {reader["department"]}, Дата рождения: {reader["date_of_birth"]}");
                    }
                }
            }
        }
        public static void DeleteStudent(SQLiteConnection connection, string surname, string name)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "DELETE FROM Students WHERE name = @name and surname = @surname";
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@surname", surname);
                command.ExecuteNonQuery();
                Console.WriteLine($"Студент '{surname} {name}' успешно удален.");
            }
        }

        public static void UpdateStudentInfo(SQLiteConnection connection, string surname, string name, string attribute, string newinfo)
        {
            switch (attribute) 
            {
                case "name":
                    UpdateStudentName(connection, surname, name, newinfo);
                    break;
                case "surname":
                    UpdateStudentSurname(connection, surname, name, newinfo);
                    break;
                case "department":
                    UpdateStudentDepartment(connection, surname, name, newinfo);
                    break;
                default:
                    break;
            }
        }

        public static void UpdateStudentInfo(SQLiteConnection connection, string surname, string name, DateOnly newdate)
        {
            UpdateStudentDate(connection, surname, name, newdate);
        }

        private static void UpdateStudentName(SQLiteConnection connection, string surname, string name, string newname)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Students SET name = @newname WHERE name = @name and surname = @surname";
                command.Parameters.AddWithValue("@newname", newname);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@surname", surname);
                command.ExecuteNonQuery();
                Console.WriteLine($"Имя студента '{surname} {newname}' успешно обновлено.");
            }
        }

        private static void UpdateStudentSurname(SQLiteConnection connection, string surname, string name, string newsurname)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Students SET surname = @newsurname WHERE name = @name and surname = @surname";
                command.Parameters.AddWithValue("@newsurname", newsurname);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@surname", surname);
                command.ExecuteNonQuery();
                Console.WriteLine($"Фамилия студента '{newsurname} {name}' успешно обновлена.");
            }
        }

        private static void UpdateStudentDepartment(SQLiteConnection connection, string surname, string name, string newdepartment)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Students SET department = @newdepartment WHERE name = @name and surname = @surname";
                command.Parameters.AddWithValue("@newdepartment", newdepartment);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@surname", surname);
                command.ExecuteNonQuery();
                Console.WriteLine($"Факультет студента '{surname} {name}' успешно обновлен.");
            }
        }

        private static void UpdateStudentDate(SQLiteConnection connection, string surname, string name, DateOnly newdate)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Students SET date_of_birth = @newdate WHERE name = @name and surname = @surname";
                command.Parameters.AddWithValue("@newdate", newdate);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@surname", surname);
                command.ExecuteNonQuery();
                Console.WriteLine($"Дата рождения студента '{surname} {name}' успешно обновлена.");
            }
        }
    }
}
