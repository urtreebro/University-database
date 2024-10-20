using System.Data.SQLite;
using University;
using University.Commands;

class Program
{
    static void Main()
    {
        string connectionString = "Data Source=example.db;Version=3;";
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            StudentsTable.CreateTableStudents(connection);
            TeachersTable.CreateTableTeachers(connection);
            CoursesTable.CreateTableCourses(connection);
            ExamsTable.CreateTableExams(connection);
            GradesTable.CreateTableExams(connection);

            bool exit = false;
            while (!exit) {
                Console.WriteLine("\nВведите команду или 'help' без кавычек для просмотра доступных команд.");
                string? command = Console.ReadLine();
                switch(command) {
                    case "help":
                        ShowCommands();
                        break;
                    case "get":
                        GetCommand.Get(connection);
                        break;
                    case "add":
                        AddCommand.Add(connection);
                        break;
                    case "delete":
                        DeleteCommand.Delete(connection);
                        break;
                    case "update":
                        UpdateCommand.Update(connection);
                        break;
                    case "average":
                        AverageCommand.Average(connection);
                        break;
                    case "exit":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
            connection.Close();
            Console.WriteLine("Соединение с базой данных закрыто.");
        }
    }

    public static void ShowCommands()
    {
        Console.WriteLine("get - получить список студентов, преподавателей, курсов, экзаменов или оценок");
        Console.WriteLine("add - добавить студента, предподавателя, курс, экзамен или оценку");
        Console.WriteLine("delete - удалить студента, предподавателя, курс, экзамен или оценку");
        Console.WriteLine("update - изменить информацию о студенте, преподавателе, курсе, экзамене или оценке");
        Console.WriteLine("average - получить средний балл студента по курсу или в целом, средний балл всего факультета");
        Console.WriteLine("exit - выйти из программы");
    }
}
