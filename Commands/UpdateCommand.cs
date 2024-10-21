using System.Data.SQLite;

namespace University.Commands
{
    public class UpdateCommand
    {
        public static void Update(SQLiteConnection connection)
        {
            Console.WriteLine("Что вы хотите изменить: student, teacher, course, exam, grade?");
            string? command = Console.ReadLine();
            switch (command)
            {
                case "student":
                    UpdateStudent(connection);
                    break;
                case "teacher":
                    UpdateTeacher(connection);
                    break;
                case "course":
                    UpdateCourse(connection);
                    break;
                case "exam":
                    UpdateExam(connection);
                    break;
                default:
                    Console.WriteLine("Неизвестные данные.");
                    break;
            }
        }

        private static void UpdateStudent(SQLiteConnection connection)
        {
            Console.WriteLine("Введите фамилию и имя студента, информацию о котором хотите изменить. (в отдельных строчках)");
            string? surname = Console.ReadLine();
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Неправильно введенные данные.");
                return;
            }
            Console.WriteLine("Введите какие данные вы хотите изменить.");
            string? attribute = Console.ReadLine();
            string[] attributes = ["surname", "name", "department"];
            if (attribute == "date")
            {
                Console.WriteLine("Введите измененные данные.");
                string? newinfo = Console.ReadLine();
                if (DateOnly.TryParse(newinfo, out DateOnly newdate))
                {
                    StudentsTable.UpdateStudentInfo(connection, surname, name, newdate);
                }
                else Console.WriteLine("Неправильно введенные данные.");
            }
            else if (attributes.Contains(attribute) && !string.IsNullOrWhiteSpace(attribute))
            {
                Console.WriteLine("Введите изменненные данные.");
                string? newinfo = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newinfo))
                {
                    StudentsTable.UpdateStudentInfo(connection, surname, name, attribute, newinfo);
                }
                else Console.WriteLine("Неправильно введенные данные.");
            }
            else Console.WriteLine("Неизвестные данные.");
        }

        private static void UpdateTeacher(SQLiteConnection connection)
        {
            Console.WriteLine("Введите фамилию и имя преподавателя, информацию о котором хотите изменить. (в отдельных строчках)");
            string? surname = Console.ReadLine();
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Неправильно введенные данные.");
                return;
            }
            Console.WriteLine("Введите какие данные вы хотите изменить.");
            string? attribute = Console.ReadLine();
            string[] attributes = ["surname", "name", "department"];
            if (attributes.Contains(attribute) && !string.IsNullOrWhiteSpace(attribute))
            {
                Console.WriteLine("Введите измененные данные.");
                string? newinfo = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newinfo))
                {
                    TeachersTable.UpdateTeacherInfo(connection, surname, name, attribute, newinfo);
                }
                else Console.WriteLine("Неправильно введенные данные.");
            }
            else Console.WriteLine("Неизвестные данные.");
        }
        private static void UpdateCourse(SQLiteConnection connection)
        {
            Console.WriteLine("Введите название курса, информацию о котором хотите изменить.");
            string? title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Неправильно введенные данные.");
                return;
            }
            Console.WriteLine("Введите какие данные вы хотите изменить.");
            string? attribute = Console.ReadLine();
            string[] attributes = ["title", "description"];
            if (attribute == "teacher_id")
            {
                Console.WriteLine("Введите измененные данные.");
                string? newinfo = Console.ReadLine();
                if (int.TryParse(newinfo, out int id))
                {
                    CoursesTable.UpdateCourseInfo(connection, title, id);
                }
                else Console.WriteLine("Неправильно введенные данные.");
            }
            else if (attributes.Contains(attribute) && !string.IsNullOrWhiteSpace(attribute))
            {
                Console.WriteLine("Введите измененные данные.");
                string? newinfo = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newinfo))
                {
                    CoursesTable.UpdateCourseInfo(connection, title, attribute, newinfo);
                }
                else Console.WriteLine("Неправильно введенные данные.");
            }
            else Console.WriteLine("Неизвестные данные.");
        }

        private static void UpdateExam(SQLiteConnection connection)
        {
            Console.WriteLine("Введите дату экзамена, информацию о котором хотите изменить.");
            string? date = Console.ReadLine();
            if (!DateOnly.TryParse(date, out DateOnly valid_date))
            {
                Console.WriteLine("Неправильно введенные данные.");
                return;
            }
            Console.WriteLine("Введите какие данные вы хотите изменить.");
            string? attribute = Console.ReadLine();
            if (attribute == "date")
            {
                Console.WriteLine("Введите измененные данные.");
                string? newinfo = Console.ReadLine();
                if (DateOnly.TryParse(newinfo, out DateOnly newdate))
                {
                    ExamsTable.UpdateExamInfo(connection, valid_date, newdate);
                }
                else Console.WriteLine("Неправильно введенные данные.");
            }
            else if (attribute == "max score" || attribute == "course id")
            {
                Console.WriteLine("Введите измененные данные.");
                string? newinfo = Console.ReadLine();
                if (int.TryParse(newinfo, out int validinfo))
                {
                    ExamsTable.UpdateExamInfo(connection, valid_date, attribute, validinfo);
                }
                else Console.WriteLine("Неправильно введенные данные.");
            }
            else Console.WriteLine("Неизвестные данные.");
        }
    }
}
