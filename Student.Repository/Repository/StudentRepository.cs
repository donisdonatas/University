using UniExam.Repository.Data;
using UniExam.Repository.Models;
using UniExam.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace UniExam.Repository.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly UniContext _uniContext;

        public StudentRepository(UniContext uniContext)
        {
            _uniContext = uniContext;
        }

        public Student GetStudentById(int id)
        {
            Student student = _uniContext.Students.Where<Student>(x=> x.Id == id).FirstOrDefault();
            return student;
        }

        public List<Student> GetStudents()
        {
            List<Student> students = _uniContext.Students.ToList();
            return students;
        }

        public List<Student> GetStudentsByDepartmentId(int? departmentId)
        {
            List<Student> students = _uniContext.Students.Where<Student>(x => x.DepartmentId == departmentId).ToList();
            return students;
        }

        public void Add(Student student)
        {
            _uniContext.Students.Add(student);
            _uniContext.SaveChanges();
        }

        public void AddRange(List<Student> students)
        {
            _uniContext.Students.AddRange(students);
            _uniContext.SaveChanges();
        }

        public bool StudentsDataExist()
        {
            Student student = _uniContext.Students.FirstOrDefault();
            return (student != null);
        }

        public void Update(Student student)
        {
            try
            {
                Student previousStudent = GetStudentById(student.Id);
                previousStudent.DepartmentId = student.DepartmentId;
                _uniContext.SaveChanges();
            }
            catch (DuplicateNameException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Duplicate Name Exception:");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (InvalidConstraintException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Constraint Exception:");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (DbUpdateException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Db Update Exception:");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There was arror trying update Department.");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void Delete(Student student)
        {
            _uniContext.Students.Remove(student);
            _uniContext.SaveChanges();
        }

        public void DeleteAll()
        {
            _uniContext.Students.RemoveRange(_uniContext.Students);
            _uniContext.SaveChanges();
        }
    }
}
