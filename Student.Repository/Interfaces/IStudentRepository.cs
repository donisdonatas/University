using UniExam.Repository.Models;

namespace UniExam.Repository.Interfaces
{
    public interface IStudentRepository
    {
        Student GetStudentById(int id);
        List<Student> GetStudents();
        List<Student> GetStudentsByDepartmentId(int? departmentId);
        void Add(Student student);
        void AddRange(List<Student> students);
        bool StudentsDataExist();
        void Update(Student student);
        void Delete(Student student);
        void DeleteAll();
    }
}
