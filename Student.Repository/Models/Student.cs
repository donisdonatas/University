using System.ComponentModel.DataAnnotations;

namespace UniExam.Repository.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [StringLength(11)]
        public string PersonalNumber { get; set; }
        public int Course { get; set; }
        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }
        public List<Lecture>? Lectures { get; set; }

        public Student(string firstName, string lastName, string personalNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            PersonalNumber = personalNumber;
        }
    }
}
