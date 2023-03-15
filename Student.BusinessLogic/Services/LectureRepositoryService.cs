using Microsoft.EntityFrameworkCore.Query;
using UniExam.BusinessLogic.Extensions;
using UniExam.BusinessLogic.Interfaces;
using UniExam.BusinessLogic.Utilities;
using UniExam.Repository.Interfaces;
using UniExam.Repository.Models;
using UniExam.Repository.Repository;

namespace UniExam.BusinessLogic.Services
{
    public class LectureRepositoryService : ILectureRepositoryService
    {
        private readonly ILectureRepository _lectureRepository;

        public LectureRepositoryService(ILectureRepository lectureRepository)
        {
            _lectureRepository = lectureRepository;
        }

        public bool LecturesDataExist()
        {
            return _lectureRepository.LecturesDataExist();
        }

        public Lecture CreateNewLecture()
        {
            ConsoleColor.Green.WriteLine("Write Lecture name:");
            string LectureName = Console.ReadLine();
            Lecture lecture = new Lecture(LectureName);
            _lectureRepository.Add(lecture);
            ConsoleColor.Blue.WriteLine("Lecture created.");
            return lecture;
        }

        public void AddRange(List<Lecture> lectures)
        {
            _lectureRepository.AddRange(lectures);
        }

        public List<Lecture> GetLectures()
        {
            return _lectureRepository.GetLectures();
        }

        public List<Lecture> GetLecturesByDepartmentId(int departmentId)
        {
            return _lectureRepository.GetLecturesByDepartmentId(departmentId);
        }

        public List<Lecture> GetLecturesByDepartmentId(int? departmentId)
        {
            return _lectureRepository.GetLecturesByDepartmentId(departmentId);
        }

        public List<Lecture> GetLecturesByStudentId(int studentId)
        {
            return _lectureRepository.GetLecturesByStudentId(studentId);
        }

        public Lecture GetSelectedLecture()
        {
            List<Lecture> lectures = GetLectures();
            Print(lectures);
            ConsoleColor.Green.WriteLine("Select Lecture:");
            Lecture SelectedLecture = SelectLecture(lectures);
            return SelectedLecture;
        }

        public List<Lecture> GetSelectedLectures()
        {
            List<Lecture> lectures = _lectureRepository.GetLectures();
            Print(lectures);
            ConsoleColor.Green.WriteLine("Select Lectures separated by comma (\",\"):");
            List<Lecture> SelectedLectures = SelectLectures(lectures);
            return SelectedLectures;
        }

        public Lecture SelectLecture(List<Lecture> lectures)
        {
            bool isNumber = Int32.TryParse(Console.ReadLine(), out int LectureNumber);
            if (!(isNumber && LectureNumber.IsInRange(lectures.Count)))
            {
                Messages.GetIdErrorMessage();
                SelectLecture(lectures);
            }
            return lectures[LectureNumber - 1];
        }

        public List<Lecture> SelectLectures(List<Lecture> lectures)
        {
            string SelectedNumbers = Console.ReadLine();
            List<int> LecturesNumbers = Converter.StringToListInt(SelectedNumbers);
            List<Lecture> selectedLectures = new List<Lecture>();
            foreach (int number in LecturesNumbers)
            {
                if (number.IsInRange(lectures.Count))
                {
                    selectedLectures.Add(lectures[number - 1]);
                }
                else
                {
                    Messages.GetNumberErrorMessage(number);
                }
            }
            return selectedLectures;
        }

        public void Print(List<Lecture> lectures)
        {
            int Number = 0;
            ConsoleColor.Blue.WriteLine("+------+----------------------------------------------+");
            ConsoleColor.Blue.WriteLine(string.Format("| {0,-5}| {1,-45}|", "Nr.", "Lecture"));
            ConsoleColor.Blue.WriteLine("+------+----------------------------------------------+");
            foreach (Lecture lecture in lectures)
            {
                ++Number;
                ConsoleColor.Blue.WriteLine(string.Format("| {0,-5}| {1,-45}|", Number, lecture.Name));
            }
            ConsoleColor.Blue.WriteLine("+------+----------------------------------------------+");
        }

        public void Delete(Lecture lecture)
        {
            _lectureRepository.Delete(lecture);
        }

        public void DeleteConnections(int studentId)
        {
            _lectureRepository.DeleteRange(studentId);
        }

        public void DeleteAllLectures()
        {
            _lectureRepository.DeleteAll();
        }
    }
}
