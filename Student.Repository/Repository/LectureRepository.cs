using UniExam.Repository.Data;
using UniExam.Repository.Models;
using UniExam.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq;

namespace UniExam.Repository.Repository
{
    public class LectureRepository : ILectureRepository
    {
        private readonly UniContext _uniContext;

        public LectureRepository(UniContext uniContext)
        {
            _uniContext = uniContext;
        }

        public Lecture GetLectureById(int id)
        {
            Lecture lecture = _uniContext.Lectures.Where<Lecture>(x => x.Id == id).FirstOrDefault();
            return lecture;
        }

        public List<Lecture> GetLectures()
        {
            List<Lecture> lectures = _uniContext.Lectures.ToList();
            return lectures;
        }

        public List<Lecture> GetLecturesByDepartmentId(int departmentId)
        {
            return _uniContext.Departments.Where(d => d.Id == departmentId).SelectMany(d => d.Lectures).ToList();
        }

        public List<Lecture> GetLecturesByDepartmentId(int? departmentId)
        {
            return _uniContext.Departments.Where(d => d.Id == departmentId).SelectMany(d => d.Lectures).ToList();
        }

        public List<Lecture> GetLecturesByStudentId(int studentId)
        {
            return _uniContext.Students.Where(s => s.Id == studentId).SelectMany(p => p.Lectures).ToList();
        }

        public void Add(Lecture lecture)
        {
            _uniContext.Lectures.Add(lecture);
            _uniContext.SaveChanges();
        }

        public void AddRange(List<Lecture> lectures)
        {
            _uniContext.Lectures.AddRange(lectures);
            _uniContext.SaveChanges();
        }

        public bool LecturesDataExist()
        {
            Lecture lecture = _uniContext.Lectures.FirstOrDefault();
            return (lecture != null);
        }

        public void Delete(Lecture lecture)
        {
            _uniContext.Lectures.Remove(lecture);
            _uniContext.SaveChanges();
        }

        public void DeleteRange(int studentId)
        {
            _uniContext.Students.Where(s => s.Id == studentId).SelectMany(p => p.Lectures).ExecuteDelete();
            //_uniContext.Lectures.RemoveRange(lectures);
            _uniContext.SaveChanges();
        }

        public void DeleteAll()
        {
            _uniContext.Lectures.RemoveRange(_uniContext.Lectures);
            _uniContext.SaveChanges();
        }
    }
}
