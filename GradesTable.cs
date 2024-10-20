using System.Data.SQLite;

namespace University
{
    public class GradesTable
    {
        public static void CreateTableExams(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Grades (
                id INTEGER PRIMARY KEY AUTOINCREMENT, 
                score INTEGER NOT NULL,
                exam_id INTEGER NOT NULL,
                student_id INTEGER NOT NULL,
                FOREIGN KEY(exam_id) REFERENCES Courses(id),
                FOREIGN KEY(student_id) REFERENCES Students(id)
            );";
                command.ExecuteNonQuery();
            }
        }

        public static void InsertGrade(SQLiteConnection connection, int score, int student_id, int exam_id)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "INSERT INTO Grades (score, exam_id, student_id) VALUES (@score, @exam_id, @student_id)";
                command.Parameters.AddWithValue("@score", score);
                command.Parameters.AddWithValue("@exam_id", exam_id);
                command.Parameters.AddWithValue("@student_id", student_id);
                command.ExecuteNonQuery();
                Console.WriteLine($"Оценка '{score}' ученику с ID {student_id} добавлена.");
            }
        }
        public static void GetGrades(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand("SELECT * FROM Grades", connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["id"]}, Оценка: {reader["score"]}, ID студента: {reader["student_id"]}, ID экзамена: {reader["exam_id"]}");
                }
            }
        }

        public static void GetGradesByCourse(SQLiteConnection connection, string course)
        {
            using (var command = new SQLiteCommand("SELECT * " +
                "FROM Grades g " +
                "JOIN Exams e on g.exam_id = e.id " +
                "JOIN Courses c on e.course_id = c.id " +
                "WHERE title = @title", connection))
            {
                command.Parameters.AddWithValue("@title", course);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Оценка: {reader["score"]}, ID студента: {reader["student_id"]}, ID экзамена: {reader["exam_id"]}");
                    }
                }
            }
            
        }
    }
}
