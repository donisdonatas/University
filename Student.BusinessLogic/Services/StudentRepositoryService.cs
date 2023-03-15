using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UniExam.BusinessLogic.Extensions;
using UniExam.BusinessLogic.Interfaces;
using UniExam.BusinessLogic.Utilities;
using UniExam.Repository.Interfaces;
using UniExam.Repository.Models;
using UniExam.Repository.Repository;

namespace UniExam.BusinessLogic.Services
{
    public class StudentRepositoryService : IStudentRepositoryService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentRepositoryService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public bool StudentsDataExist()
        {
            return _studentRepository.StudentsDataExist();
        }

        public Student CreateNewStudent()
        {
            ConsoleColor.Green.WriteLine("Enter Student First Name:");
            string FirstName = Console.ReadLine();
            ConsoleColor.Green.WriteLine("Enter Student Last Name:");
            string LastName = Console.ReadLine();
            ConsoleColor.Green.WriteLine("Enter Student Personal Number (National ID):");
            string PersonalNumber = Console.ReadLine();
            Student student = new Student(FirstName, LastName, PersonalNumber);
            _studentRepository.Add(student);
            ConsoleColor.Blue.WriteLine("Student created.");
            return student;
        }

        public void AddRange(List<Student> students)
        {
            _studentRepository.AddRange(students);
        }

        public Student GetStudentById(int studentId)
        {
            return _studentRepository.GetStudentById(studentId);
        }

        public List<Student> GetStudents()
        {
            List<Student> students = _studentRepository.GetStudents();
            return students;
        }

        public List<Student> GetStudentsByDepartmentId(int? departmentId)
        {
            List<Student> students = _studentRepository.GetStudentsByDepartmentId(departmentId);
            return students;
        }

        public Student GetSelectedStudent()
        {
            List<Student> students = GetStudents();
            Print(students);
            ConsoleColor.Green.WriteLine("Select Student:");
            Student SelectedStudent = SelectStudent(students);
            return SelectedStudent;
        }

        public Student SelectStudent(List<Student> students)
        {
            string Number = Console.ReadLine();
            bool isNumber = int.TryParse(Number, out int StudentNumber);
            if (!(isNumber && StudentNumber.IsInRange(students.Count)))
            {
                Messages.GetIdErrorMessage();
                SelectStudent(students);
            }
            return students[StudentNumber - 1];
            // Su šita funkcija iškilo problemos:
            // Jei pasirenkant reikšmę pirma įvedama reikšmė, kuri yra OutOfRange, tada išmeta klaidą ir grįžtama atgal į tą pačią funkciją.
            // Antraą kartą įvedus gerą reikšmę, atrodo iki return kaip ir viskas gerai, bet tada kažkodėl programa grįžta dar kartą į pradžią
            // ir pasiima prieš tai buvusią blogą reikšmę, tada ateina prie retur ir sako, kad reikšmė yra OutOfRange.
        }

        public List<Student> GetSelectedStudents(List<Student> students)
        {
            string SelectedNumbers = Console.ReadLine();
            List<int> StudentsNumbers = Converter.StringToListInt(SelectedNumbers);
            List<Student> SelectedStudents = new List<Student>();
            foreach (int studentNumber in StudentsNumbers)
            {
                if (studentNumber.IsInRange(students.Count))
                {
                    SelectedStudents.Add(students[studentNumber - 1]);
                }
            }
            return SelectedStudents;
        }

        public void Print(List<Student> students)
        {
            int Number = 0;
            ConsoleColor.Blue.WriteLine("+-------+---------------------+---------------------+-----------------+");
            ConsoleColor.Blue.WriteLine(string.Format("|  {0,-5}| {1,-20}| {2,-20}| {3, -16}|", "Nr.", "First Name", "Last name", "Personal Number"));
            ConsoleColor.Blue.WriteLine("+-------+---------------------+---------------------+-----------------+"); ;
            foreach (Student student in students)
            {
                ++Number;
                ConsoleColor.Blue.WriteLine(string.Format("|  {0,-5}| {1,-20}| {2,-20}|   {3, -14}|", Number, student.FirstName, student.LastName, student.PersonalNumber));
            }
            ConsoleColor.Blue.WriteLine("+-------+---------------------+---------------------+-----------------+");
        }

        public void Update(Student student)
        {
            _studentRepository.Update(student);
        }

        public void Delete(Student student)
        {
            _studentRepository.Delete(student);
        }

        public void DeleteAllStudents()
        {
            _studentRepository.DeleteAll();
        }
    }
}
