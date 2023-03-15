using UniExam.Repository.Models;

namespace UniExam.BusinessLogic.Interfaces
{
    public interface IDepartmentRepositoryService
    {
		bool DepartmentsDataExist();
        Department CreateNewDepartment();
		void AddRange(List<Department> departments);
        List<Department> GetDepartments();
        Department GetDepartmentById(int id);
        List<Department> GetDepartmentsExcept(int? id);
        Department GetSelectedDepartment();
        Department SelectDepartment(List<Department> departments);
        void Print(List<Department> departments);
        void Update(Department department);
        void Delete(Department department);
        void DeleteAllDepartments();
    }
}
