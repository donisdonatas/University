using UniExam.Repository.Models;
using UniExam.Repository.Interfaces;
using UniExam.BusinessLogic.Interfaces;
using UniExam.BusinessLogic.Utilities;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using UniExam.BusinessLogic.Extensions;
using UniExam.Repository.Repository;

namespace UniExam.BusinessLogic.Services
{
    public class DepartmentRepositoryService : IDepartmentRepositoryService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentRepositoryService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public bool DepartmentsDataExist()
        {
            return _departmentRepository.DepartmentsDataExist();
        }

        public Department CreateNewDepartment()
        {
            ConsoleColor.Green.WriteLine("Write Department Name:");
            string Name = Console.ReadLine();
            Department department = new Department(Name);
            _departmentRepository.Add(department);
            ConsoleColor.Blue.WriteLine("Department created.");
            return department;
        }

        public void AddRange(List<Department> departments)
        {
            _departmentRepository.AddRange(departments);
        }

        public List<Department> GetDepartments()
        {
            List<Department> departments = _departmentRepository.GetDepartments();
            return departments;
        }

        public Department GetDepartmentById(int id)
        {
            return _departmentRepository.GetDepartmentById(id);
        }
        
        public List<Department> GetDepartmentsExcept(int? id)
        {
            List<Department> departments = _departmentRepository.GetDepartmentsExcpet(id);
            return departments;
        }

        public Department GetSelectedDepartment()
        {
            List<Department> departments = GetDepartments();
            Print(departments);
            ConsoleColor.Green.WriteLine("Select Department:");
            Department SelectedDepartment = SelectDepartment(departments);
            return SelectedDepartment;
        }

        public Department SelectDepartment(List<Department> departments)
        {
            string Selection = Console.ReadLine();
            bool isNumber = Int32.TryParse(Selection, out int DepartmentNumber);
            if (!(isNumber && DepartmentNumber.IsInRange(departments.Count)))
            {
                Messages.GetIdErrorMessage();
                SelectDepartment(departments);
            }
            return departments[DepartmentNumber - 1];
        }

        public void Print(List<Department> departments)
        {
            int Number = 0;
            ConsoleColor.Blue.WriteLine("+-------+--------------------------------------------------------+");
            ConsoleColor.Blue.WriteLine(string.Format("| {0,-5}| {1,-56}|", "Nr.", "Department Name"));
            ConsoleColor.Blue.WriteLine("+-------+--------------------------------------------------------+");
            foreach (Department department in departments)
            {
                ++Number;
                ConsoleColor.Blue.WriteLine(string.Format("| {0,-5}| {1,-56}|", Number, department.Name));
            }
            ConsoleColor.Blue.WriteLine("+-------+--------------------------------------------------------+");
        }

        public void Update(Department department)
        {
            _departmentRepository.Update(department);
        }

        public void Delete(Department department)
        {
            _departmentRepository.Delete(department);
        }

        public void DeleteAllDepartments()
        {
            _departmentRepository.DeleteAll();
        }
    }
}
