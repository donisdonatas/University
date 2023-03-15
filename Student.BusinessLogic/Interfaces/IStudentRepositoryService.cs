using UniExam.Repository.Models;

namespace UniExam.BusinessLogic.Interfaces
{
    public interface IStudentRepositoryService
    {
		bool StudentsDataExist();
        Student CreateNewStudent();
		void AddRange(List<Student> students);
        Student GetStudentById(int id);
		List<Student> GetStudents();
        List<Student> GetStudentsByDepartmentId(int? departmentId);
        Student GetSelectedStudent();
        Student SelectStudent(List<Student> students);
        List<Student> GetSelectedStudents(List<Student> students);
		void Print(List<Student> students);
        void Update(Student student);
        void Delete(Student student);
        void DeleteAllStudents();
    }
}
