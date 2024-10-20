using System.Data.SQLite;


namespace University
{
    public class TeachersTable
    {
        public static void CreateTableTeachers(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Teachers (
                id INTEGER PRIMARY KEY AUTOINCREMENT, 
                name TEXT NOT NULL,
                surname TEXT NOT NULL,
                department TEXT NOT NULL
            );";
                command.ExecuteNonQuery();
            }
        }

        public static void InsertTeacher(SQLiteConnection connection, string surname, string name, string department)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "INSERT INTO Teachers (name, surname, department) VALUES (@name, @surname, @department)";
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@surname", surname);
                command.Parameters.AddWithValue("@department", department);
                command.ExecuteNonQuery();
                Console.WriteLine($"Преподаватель {name} {surname} добавлен.");
            }
        }

        public static void GetTeachers(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand("SELECT * FROM Teachers", connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["id"]}, Фамилия: {reader["surname"]}, Имя: {reader["name"]}, Кафедра: {reader["department"]}");
                }
            }
        }
        public static void DeleteTeacher(SQLiteConnection connection, string surname, string name)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "DELETE FROM Teachers WHERE name = @name and surname = @surname";
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@surname", surname);
                command.ExecuteNonQuery();
                Console.WriteLine($"Преподаватель '{surname} {name}' успешно удален.");
            }
        }
        public static void UpdateTeacherInfo(SQLiteConnection connection, string surname, string name, string attribute, string newinfo)
        {
            switch (attribute)
            {
                case "name":
                    UpdateTeacherName(connection, surname, name, newinfo);
                    break;
                case "surname":
                    UpdateTeacherSurname(connection, surname, name, newinfo);
                    break;
                case "department":
                    UpdateTeacherDepartment(connection, surname, name, newinfo);
                    break;
                default:
                    break;
            }
        }

        private static void UpdateTeacherName(SQLiteConnection connection, string surname, string name, string newname)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Teachers SET name = @newname WHERE name = @name and surname = @surname";
                command.Parameters.AddWithValue("@newname", newname);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@surname", surname);
                command.ExecuteNonQuery();
                Console.WriteLine($"Имя преподавателя '{surname} {newname}' успешно обновлено.");
            }
        }

        private static void UpdateTeacherSurname(SQLiteConnection connection, string surname, string name, string newsurname)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Teachers SET surname = @newsurname WHERE name = @name and surname = @surname";
                command.Parameters.AddWithValue("@newsurname", newsurname);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@surname", surname);
                command.ExecuteNonQuery();
                Console.WriteLine($"Фамилия преподавателя '{newsurname} {name}' успешно обновлена.");
            }
        }

        private static void UpdateTeacherDepartment(SQLiteConnection connection, string surname, string name, string newdepartment)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Teachers SET department = @newdepartment WHERE name = @name and surname = @surname";
                command.Parameters.AddWithValue("@newdepartment", newdepartment);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@surname", surname);
                command.ExecuteNonQuery();
                Console.WriteLine($"Кафедра преподавателя '{surname} {name}' успешно обновлена.");
            }
        }
    }
}
