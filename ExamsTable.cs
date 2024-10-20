using System.Data.SQLite;

namespace University
{
    public class ExamsTable
    {
        public static void CreateTableExams(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Exams (
                id INTEGER PRIMARY KEY AUTOINCREMENT, 
                date DATEONLY NOT NULL,
                max_score INTEGER NOT NULL,
                course_id INTEGER NOT NULL,
                FOREIGN KEY(course_id) REFERENCES Courses(id)
            );";
                command.ExecuteNonQuery();
            }
        }
        public static void InsertExam(SQLiteConnection connection, DateOnly date, int max_score, int course_id)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "INSERT INTO Exams (date, max_score, course_id) VALUES (@date, @max_score, @course_id)";
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@max_score", max_score);
                command.Parameters.AddWithValue("@course_id", course_id);
                command.ExecuteNonQuery();
                Console.WriteLine($"Экзамен на {date} добавлен.");
            }
        }
        public static void GetExams(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand("SELECT * FROM Exams", connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["id"]}, Дата: {reader["date"]}, Максимальный балл: {reader["max_score"]}, ID курса: {reader["course_id"]}");
                }
            }
        }

        public static void DeleteExam(SQLiteConnection connection, DateOnly date)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "DELETE FROM Exams WHERE date = @date";
                command.Parameters.AddWithValue("@date", date);
                command.ExecuteNonQuery();
                Console.WriteLine($"Экзамен '{date}' успешно удален.");
            }
        }

        public static void UpdateExamInfo(SQLiteConnection connection, DateOnly date, DateOnly newdate)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Exams SET date = @newdate WHERE date = @date";
                command.Parameters.AddWithValue("@newdate", newdate);
                command.Parameters.AddWithValue("@date", date);
                command.ExecuteNonQuery();
                Console.WriteLine($"Дата экзамена успешно изменена на '{date}'.");
            }
        }

        public static void UpdateExamInfo(SQLiteConnection connection, DateOnly date, string attribute, int newinfo)
        {
            switch (attribute)
            {
                case "course id":
                    break;
                case "max score":
                    UpdateExamScore(connection, date, newinfo);
                    break;
            }
        }

        private static void UpdateExamScore(SQLiteConnection connection, DateOnly date, int max_score)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Exams SET max_score = @max_score WHERE date = @date";
                command.Parameters.AddWithValue("@max_score", max_score);
                command.Parameters.AddWithValue("@date", date);
                command.ExecuteNonQuery();
                Console.WriteLine($"Максимальный балл экзамена '{date}' успешно обновлен.");
            }
        }

        private static void UpdateExamCourseID(SQLiteConnection connection, DateOnly date, int id)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Exams SET course_id = @id WHERE date = @date";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@date", date);
                command.ExecuteNonQuery();
                Console.WriteLine($"Курс экзамена '{date}' успешно обновлен.");
            }
        }
    }
}
