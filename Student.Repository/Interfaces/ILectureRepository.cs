using UniExam.Repository.Models;

namespace UniExam.Repository.Interfaces
{
    public interface ILectureRepository
    {
        Lecture GetLectureById(int id);
        List<Lecture> GetLectures();
        List<Lecture> GetLecturesByDepartmentId(int departmentId);
        List<Lecture> GetLecturesByDepartmentId(int? departmentId);
        List<Lecture> GetLecturesByStudentId(int studentId);
        void Add(Lecture lecture);
        void AddRange(List<Lecture> lectures);
        bool LecturesDataExist();
        void Delete(Lecture lecture);
        void DeleteRange(int studentId);
        void DeleteAll();
    }
}
