using UniExam.Repository.Data;
using UniExam.Repository.Models;
using UniExam.Repository.Interfaces;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace UniExam.Repository.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly UniContext _uniContext;

        public DepartmentRepository(UniContext context)
        {
            _uniContext = context;
        }

        public Department GetDepartmentById(int id)
        {
            Department department = _uniContext.Departments.Where<Department>(x=> x.Id == id).FirstOrDefault();
            return department;
        }

        public List<Department> GetDepartments()
        {
            List<Department> departments = _uniContext.Departments.ToList();
            return departments;
        }

        public List<Department> GetDepartmentsExcpet(int? id)
        {
            List<Department> departments = _uniContext.Departments.Where<Department>(x => x.Id != id).ToList();
            return departments;
        }

        public void Add(Department department)
        {
            _uniContext.Departments.Add(department);
            _uniContext.SaveChanges();
        }

        public void AddRange(List<Department> departments)
        {
            _uniContext.Departments.AddRange(departments);
            _uniContext.SaveChanges();
        }

        public bool DepartmentsDataExist()
        {
            Department department = _uniContext.Departments.FirstOrDefault();
            return (department != null);
        }

        public void Update(Department department)
        {
            try
            {
                Department PreviousDepartment = GetDepartmentById(department.Id);
                PreviousDepartment.Lectures = department.Lectures;
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

        public void Delete(Department department)
        {
            _uniContext.Departments.Remove(department);
            _uniContext.SaveChanges();
        }

        public void DeleteAll()
        {
            _uniContext.Departments.RemoveRange(_uniContext.Departments);
            _uniContext.SaveChanges();
        }
    }
}
