using System.Data.SQLite;

namespace University.Commands
{
    public class AddCommand
    {
        public static void Add(SQLiteConnection connection)
        {
            Console.WriteLine("Что вы хотите добавить: student, teacher, course, exam, grade?");
            string? command = Console.ReadLine();
            switch (command)
            {
                case "student":
                    AddStudent(connection);
                    break;
                case "teacher":
                    AddTeacher(connection);
                    break;
                case "course":
                    AddCourse(connection);
                    break;
                case "exam":
                    AddExam(connection);
                    break;
                case "grade":
                    AddGrade(connection);  
                    break;
                default:
                    Console.WriteLine("Неизвестные данные.");
                    break;
            }
        }
        private static void AddStudent(SQLiteConnection connection)
        {
            Console.WriteLine("Введите фамилию, имя, факультет и дату рождения студента в формате DD-MM-YYYY. (в отдельных строчках)");
            string? surname = Console.ReadLine();
            string? name = Console.ReadLine();
            string? department = Console.ReadLine();
            string? date = Console.ReadLine();
            if (DateOnly.TryParse(date, out DateOnly valid_date) &&
                !string.IsNullOrWhiteSpace(surname) &&
                !string.IsNullOrWhiteSpace(name) &&
                !string.IsNullOrWhiteSpace(department))
            {
                StudentsTable.InsertStudent(connection, surname, name, department, valid_date);
            }
            else Console.WriteLine("Неправильно введенные данные.");
        }

        private static void AddTeacher(SQLiteConnection connection)
        {
            Console.WriteLine("Введите фамилию, имя и кафедру преподавателя. (в отдельных строчках)");
            string? surname = Console.ReadLine();
            string? name = Console.ReadLine();
            string? department = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(surname) &&
                !string.IsNullOrWhiteSpace(name) &&
                !string.IsNullOrWhiteSpace(department))
            {
                TeachersTable.InsertTeacher(connection, surname, name, department);
            }
            else Console.WriteLine("Неправильно введенные данные.");
        }

        private static void AddCourse(SQLiteConnection connection)
        {
            Console.WriteLine("Введите название и описание курса, а также ID преподавателя, ведущего курс. (в отдельных строчках)");
            string? title = Console.ReadLine();
            string? description = Console.ReadLine();
            string? teacher_id = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title) &&
                !string.IsNullOrWhiteSpace(description) &&
                int.TryParse(teacher_id, out int id))
            {
                CoursesTable.InsertCourse(connection, title, description, id);
            }
            else Console.WriteLine("Неправильно введенные данные.");
        }

        private static void AddExam(SQLiteConnection connection)
        {
            Console.WriteLine("Введите дату и максимальный балл экзамена, а также ID курса. (в отдельных строчках)");
            string? date = Console.ReadLine();
            string? max_score = Console.ReadLine();
            string? course_id = Console.ReadLine();
            if (DateOnly.TryParse(date, out DateOnly valid_date) &&
                int.TryParse(max_score, out int valid_score) &&
                int.TryParse(course_id, out int valid_id))
            {
                ExamsTable.InsertExam(connection, valid_date, valid_score, valid_id);
            }
            else Console.WriteLine("Неправильно введенные данные.");
        }

        private static void AddGrade(SQLiteConnection connection)
        {
            Console.WriteLine("Введите оценку, ID студента, а также ID экзамена. (в отдельных строчках)");
            string? score = Console.ReadLine();
            string? student = Console.ReadLine();
            string? exam = Console.ReadLine();
            if (int.TryParse(score, out int valid_score) &&
                int.TryParse(student, out int valid_student) &&
                int.TryParse(exam, out int valid_exam))
            {
                GradesTable.InsertGrade(connection, valid_score, valid_student, valid_exam);
            }
            else Console.WriteLine("Неправильно введенные данные.");
        }
    }
}
