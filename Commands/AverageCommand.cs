using System.Data.SQLite;

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
                    break;
                case "student":
                    break;
                case "department":
                    break;
                default:
                    Console.WriteLine("Неизвестные данные.");
                    break;
            }
        }
    }
}
