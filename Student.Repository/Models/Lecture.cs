using System.ComponentModel.DataAnnotations;

namespace UniExam.Repository.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Department> Departments { get; set;}
        public List<Student> Students { get; set;}

        public Lecture(string name)
        {
            Name = name;
        }
    }
}
