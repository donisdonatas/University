using UniExam.Repository.Models;

namespace UniExam.BusinessLogic.Interfaces
{
    public interface ILectureRepositoryService
    {
		bool LecturesDataExist();
        Lecture CreateNewLecture();
        void AddRange(List<Lecture> lectures);
        List<Lecture> GetLectures();
        List<Lecture> GetLecturesByDepartmentId(int departmentId);
		List<Lecture> GetLecturesByDepartmentId(int? departmentId);
        List<Lecture> GetLecturesByStudentId(int studentId);
        Lecture GetSelectedLecture();
        List<Lecture> GetSelectedLectures();
        Lecture SelectLecture(List<Lecture> lectures);
		List<Lecture> SelectLectures(List<Lecture> lectures);
        void Print(List<Lecture> lectures);
        void Delete(Lecture lectures);
        void DeleteConnections(int studentId);
        void DeleteAllLectures();
    }
}
