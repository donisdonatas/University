using System.ComponentModel.DataAnnotations;

namespace UniExam.Repository.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Lecture>? Lectures { get; set; }
        public List<Student>? Students { get; set; }

        public Department(string name)
        {
            Name = name;
        }
    }
}
