using UniExam.Repository.Models;

namespace UniExam.Repository.Interfaces
{
    public interface IDepartmentRepository
    {
        Department GetDepartmentById(int id);
        List<Department> GetDepartments();
        List<Department> GetDepartmentsExcpet(int? id);
        void Add(Department department);
        void AddRange(List<Department> departments);
        bool DepartmentsDataExist();
        void Update(Department department);
        void Delete(Department department);
        void DeleteAll();
    }
}
