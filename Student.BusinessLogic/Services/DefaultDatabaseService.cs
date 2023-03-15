using UniExam.BusinessLogic.Interfaces;
using UniExam.Repository.Models;

namespace UniExam.BusinessLogic.Services
{
    public class DefaultDatabaseService
    {
        private IDepartmentRepositoryService _departmentRepositoryService;
        private ILectureRepositoryService _lectureRepositoryService;
        private IStudentRepositoryService _studentRepositoryService;

        public DefaultDatabaseService(IDepartmentRepositoryService departmentRepositoryService, ILectureRepositoryService lectureRepositoryService, IStudentRepositoryService studentRepositoryService)
        {
            _departmentRepositoryService = departmentRepositoryService;
            _lectureRepositoryService = lectureRepositoryService;
            _studentRepositoryService = studentRepositoryService;
        }

        private List<Lecture> DefaultLectures = new List<Lecture>()
        {
            new Lecture("Informacinės technologijos"),
            new Lecture("Įvadas į cheminę technologiją ir inžineriją"),
            new Lecture("Matematika"),
            new Lecture("Neorganinė chemija"),
            new Lecture("Dirbtinio intelekto etika"),
            new Lecture("Inžinerinė grafika"),
            new Lecture("Klasikinė fizika"),
            new Lecture("Programavimo įvadas inžinieriams"),
            new Lecture("Apskaitos pagrindai"),
            new Lecture("Informatika"),
            new Lecture("Įvadas į finansus"),
            new Lecture("Informatikos studijų įvadas"),
            new Lecture("Kompiuterinė grafika"),
            new Lecture("Objektinio programavimo pagrindai"),
            new Lecture("Bendroji chemija"),
            new Lecture("Informacinės technologijos inžinieriams"),
            new Lecture("Įvadas į aviacijos inžinerijos specialybę"),
            new Lecture("Įvadas į statybos inžineriją"),
            new Lecture("Statybinė grafika"),
            new Lecture("Įvadas į komunikaciją"),
            new Lecture("Komunikacijos teorijos"),
            new Lecture("Organizacijų komunikacija"),
        };

        private List<Department> DefaultDepartments = new List<Department>()
        {
            new Department("Cheminės technologijos fakultetas"),
            new Department("Elektros ir elektronikos fakultetas"),
            new Department("Ekonomikos ir verslo fakultetas"),
            new Department("Informatikos fakultetas"),
            new Department("Matematikos ir gamtos mokslų fakultetas"),
            new Department("Mechanikos inžinerijos ir dizaino fakultetas"),
            new Department("Statybos ir architektūros fakultetas"),
            new Department("Socialinių, humanitarinių mokslų ir menų fakultetas"),
        };

        private List<Student> DefaultStudents = new List<Student>()
        {
            
            new Student("Ugne", "Akamauskaitė", "60202150145"),
            new Student("Diana", "Alminavičiūtė", "60311103188"),
            new Student("Margarita", "Balsytė", "60002098093"),
            new Student("Marija", "Traponytė", "60202033155"),
            new Student("Darius", "Nitas", "50204203152"),
            new Student("Andrius", "Noreika", "50301093159"),
            new Student("Edgaras", "Vaičiukynas", "50307223156"),
            new Student("Lukas", "Daugelas", "50310103155"),
            new Student("Augustinas", "Daunora", "50310203153"),
            new Student("Robertas", "Pabalys", "50306153156"),
            new Student("Laurynas", "Gasaitis", "50307053157"),
            new Student("Justina", "Valkytė", "60204093157"),
            new Student("Julija", "Cikanaitė", "60204053151"),
            new Student("Darius", "Marcinkevičius", "38904070234"),
            new Student("Erikas", "Namajauskas", "38904070234"),
            new Student("Saulius", "Kazilionis", "50309165689"),
            new Student("Kostas", "Zabulevičius", "50211251456"),
        };

        public void CheckDefaultTables()
        {
            if (!_lectureRepositoryService.LecturesDataExist()) _lectureRepositoryService.AddRange(DefaultLectures);
            
            if (!_departmentRepositoryService.DepartmentsDataExist())
            {
                _departmentRepositoryService.AddRange(DefaultDepartments);
                CreateDefaultDepartmentsToLecturesRelations();
            }

            if (!_studentRepositoryService.StudentsDataExist())
            {
                _studentRepositoryService.AddRange(DefaultStudents);
                CreateDefaultStudentsToDepartmentAndLecturesRelation();
            }
            
        }

        private List<Lecture> GetLectures(List<string> names)
        {
            List<Lecture> lectures = _lectureRepositoryService.GetLectures().Where(x => names.Contains(x.Name)).ToList();
            return lectures;
        }

        private Department GetDepartmet(string departmentName)
        {
            Department department = _departmentRepositoryService.GetDepartments().Where(x => departmentName.Contains(x.Name)).FirstOrDefault();
            return department;
        }

        private void CreateDefaultDepartmentsToLecturesRelations()
        {
            List<string> Chtf = new List<string> { "Informacinės technologijos",
                                                   "Įvadas į cheminę technologiją ir inžineriją" ,
                                                   "Matematika",
                                                   "Neorganinė chemija", };
            List<Lecture> lectures0 = GetLectures(Chtf);
            Department department0 = _departmentRepositoryService.GetDepartments()[0];
            department0.Lectures = lectures0;
            _departmentRepositoryService.Update(department0);

            List<string> Eef = new List<string> { "Dirbtinio intelekto etika",
                                                  "Inžinerinė grafika",
                                                  "Klasikinė fizika",
                                                  "Matematika",
                                                  "Programavimo įvadas inžinieriams", };
            List<Lecture> lectures1 = GetLectures(Eef);
            Department department1 = _departmentRepositoryService.GetDepartments()[1];
            department1.Lectures = lectures1;
            _departmentRepositoryService.Update(department1);

            List<string> Ev = new List<string> { "Apskaitos pagrindai",
                                                 "Informatika",
                                                 "Įvadas į finansus",
                                                 "Matematika", };
            List<Lecture> lectures2 = GetLectures(Ev);
            Department department2 = _departmentRepositoryService.GetDepartments()[2];
            department2.Lectures = lectures2;
            _departmentRepositoryService.Update(department2);

            List<string> Inf = new List<string> { "Informatikos studijų įvadas",
                                                  "Kompiuterinė grafika",
                                                  "Matematika",
                                                  "Objektinio programavimo pagrindai", };
            List<Lecture> lectures3 = GetLectures(Inf);
            Department department3 = _departmentRepositoryService.GetDepartments()[3];
            department3.Lectures = lectures3;
            _departmentRepositoryService.Update(department3);

            List<string> Mgm = new List<string> { "Bendroji chemija",
                                                  "Informacinės technologijos",
                                                  "Matematika",
                                                  "Klasikinė fizika", };
            List<Lecture> lectures4 = GetLectures(Mgm);
            Department department4 = _departmentRepositoryService.GetDepartments()[4];
            department4.Lectures = lectures4;
            _departmentRepositoryService.Update(department4);

            List<string> Mid = new List<string> { "Informacinės technologijos inžinieriams",
                                                  "Inžinerinė grafika",
                                                  "Įvadas į aviacijos inžinerijos specialybę",
                                                  "Matematika", };
            List<Lecture> lectures5 = GetLectures(Mid);
            Department department5 = _departmentRepositoryService.GetDepartments()[5];
            department5.Lectures = lectures5;
            _departmentRepositoryService.Update(department5);

            List<string> Star = new List<string> { "Informacinės technologijos",
                                                   "Įvadas į statybos inžineriją",
                                                   "Matematika",
                                                   "Statybinė grafika", };
            List<Lecture> lectures6 = GetLectures(Star);
            Department department6 = _departmentRepositoryService.GetDepartments()[6];
            department6.Lectures = lectures6;
            _departmentRepositoryService.Update(department6);

            List<string> Sohu = new List<string> { "Informatika",
                                                   "Įvadas į komunikaciją",
                                                   "Komunikacijos teorijos",
                                                   "Matematika",
                                                   "Organizacijų komunikacija", };
            List<Lecture> lectures7 = GetLectures(Sohu);
            Department department7 = _departmentRepositoryService.GetDepartments()[7];
            department7.Lectures = lectures7;
            _departmentRepositoryService.Update(department7);
        }

        private void CreateDefaultStudentsToDepartmentAndLecturesRelation()
        {
            List<Student> students = _studentRepositoryService.GetStudents();

            Student student0 = students[0];
            Department department0 = GetDepartmet("Cheminės technologijos fakultetas");
            student0.DepartmentId = department0.Id;
            List<string> Chtf0 = new List<string> { "Įvadas į cheminę technologiją ir inžineriją" ,
                                                    "Matematika",
                                                    "Neorganinė chemija", };
            List<Lecture> lectures0 = GetLectures(Chtf0);
            student0.Lectures = lectures0;
            _studentRepositoryService.Update(student0);

            Student student1 = students[1];
            Department department1 = GetDepartmet("Cheminės technologijos fakultetas");
            student1.DepartmentId = department1.Id;
            List<string> Chtf1 = new List<string> { "Informacinės technologijos",
                                                    "Matematika",
                                                    "Neorganinė chemija", };
            List<Lecture> lectures1 = GetLectures(Chtf1);
            student1.Lectures = lectures1;
            _studentRepositoryService.Update(student1);

            Student student2 = students[2];
            Department department2 = GetDepartmet("Elektros ir elektronikos fakultetas");
            student2.DepartmentId = department2.Id;
            List<string> Eef2 = new List<string> { "Dirbtinio intelekto etika",
                                                   "Inžinerinė grafika",
                                                   "Klasikinė fizika",
                                                   "Matematika", };
            List<Lecture> lectures2 = GetLectures(Eef2);
            student2.Lectures = lectures2;
            _studentRepositoryService.Update(student2);

            Student student3 = students[3];
            Department department3 = GetDepartmet("Elektros ir elektronikos fakultetas");
            student3.DepartmentId = department3.Id;
            List<string> Eef3 = new List<string> { "Dirbtinio intelekto etika",
                                                   "Inžinerinė grafika",
                                                   "Matematika",
                                                   "Programavimo įvadas inžinieriams", };
            List<Lecture> lectures3 = GetLectures(Eef3);
            student3.Lectures = lectures3;
            _studentRepositoryService.Update(student3);

            Student student4 = students[4];
            Department department4 = GetDepartmet("Ekonomikos ir verslo");
            student4.DepartmentId = department4.Id;
            List<string> Ev4 = new List<string> { "Apskaitos pagrindai",
                                                  "Informatika",
                                                  "Įvadas į finansus",
                                                  "Matematika", };
            List<Lecture> lectures4 = GetLectures(Ev4);
            student4.Lectures = lectures4;
            _studentRepositoryService.Update(student4);

            Student student5 = students[5];
            Department department5 = GetDepartmet("Ekonomikos ir verslo");
            student5.DepartmentId = department5.Id;
            List<string> Ev5 = new List<string> { "Informatika",
                                                  "Įvadas į finansus",
                                                  "Matematika", };
            List<Lecture> lectures5 = GetLectures(Ev5);
            student5.Lectures = lectures5;
            _studentRepositoryService.Update(student5);

            Student student6 = students[6];
            Department department6 = GetDepartmet("Informatikos fakultetas");
            student6.DepartmentId = department6.Id;
            List<string> Inf6 = new List<string> { "Informatikos studijų įvadas",
                                                   "Kompiuterinė grafika",
                                                   "Objektinio programavimo pagrindai", };
            List<Lecture> lectures6 = GetLectures(Inf6);
            student6.Lectures = lectures6;
            _studentRepositoryService.Update(student6);

            Student student7 = students[7];
            Department department7 = GetDepartmet("Informatikos fakultetas");
            student7.DepartmentId = department7.Id;
            List<string> Inf7 = new List<string> { "Informatikos studijų įvadas",
                                                   "Kompiuterinė grafika",
                                                   "Matematika",
                                                   "Objektinio programavimo pagrindai", };
            List<Lecture> lectures7 = GetLectures(Inf7);
            student7.Lectures = lectures7;
            _studentRepositoryService.Update(student7);

            Student student8 = students[8];
            Department department8 = GetDepartmet("Matematikos ir gamtos mokslų fakultetas");
            student8.DepartmentId = department8.Id;
            List<string> Mgm8 = new List<string> { "Bendroji chemija",
                                                   "Informacinės technologijos",
                                                   "Matematika",
                                                   "Klasikinė fizika", };
            List<Lecture> lectures8 = GetLectures(Mgm8);
            student8.Lectures = lectures8;
            _studentRepositoryService.Update(student8);

            Student student9 = students[9];
            Department department9 = GetDepartmet("Matematikos ir gamtos mokslų fakultetas");
            student9.DepartmentId = department9.Id;
            List<string> Mgm9 = new List<string> { "Bendroji chemija",
                                                   "Matematika",
                                                   "Klasikinė fizika", };
            List<Lecture> lectures9 = GetLectures(Mgm9);
            student9.Lectures = lectures9;
            _studentRepositoryService.Update(student9);

            Student student10 = students[10];
            Department department10 = GetDepartmet("Mechanikos inžinerijos ir dizaino fakultetas");
            student10.DepartmentId = department10.Id;
            List<string> Mid10 = new List<string> { "Informacinės technologijos inžinieriams",
                                                    "Inžinerinė grafika",
                                                    "Įvadas į aviacijos inžinerijos specialybę",
                                                    "Matematika", };
            List<Lecture> lectures10 = GetLectures(Mid10);
            student10.Lectures = lectures10;
            _studentRepositoryService.Update(student10);

            Student student11 = students[11];
            Department department11 = GetDepartmet("Mechanikos inžinerijos ir dizaino fakultetas");
            student11.DepartmentId = department11.Id;
            List<string> Mid11 = new List<string> { "Informacinės technologijos inžinieriams",
                                                    "Inžinerinė grafika",
                                                    "Matematika", };
            List<Lecture> lectures11 = GetLectures(Mid11);
            student11.Lectures = lectures11;
            _studentRepositoryService.Update(student11);

            Student student12 = students[12];
            Department department12 = GetDepartmet("Statybos ir architektūros fakultetas");
            student12.DepartmentId = department12.Id;
            List<string> Star12 = new List<string> { "Informacinės technologijos",
                                                     "Įvadas į statybos inžineriją",
                                                     "Matematika",
                                                     "Statybinė grafika", };
            List<Lecture> lectures12 = GetLectures(Star12);
            student12.Lectures = lectures12;
            _studentRepositoryService.Update(student12);

            Student student13 = students[13];
            Department department13 = GetDepartmet("Statybos ir architektūros fakultetas");
            student13.DepartmentId = department13.Id;
            List<string> Star13 = new List<string> { "Informacinės technologijos",
                                                     "Matematika",
                                                     "Statybinė grafika", };
            List<Lecture> lectures13 = GetLectures(Star13);
            student13.Lectures = lectures13;
            _studentRepositoryService.Update(student13);

            Student student14 = students[14];
            Department department14 = GetDepartmet("Cheminės technologijos fakultetas");
            student1.DepartmentId = department14.Id;
            List<string> Chtf14 = new List<string> { "Įvadas į cheminę technologiją ir inžineriją" ,
                                                     "Matematika",
                                                     "Neorganinė chemija", };
            List<Lecture> lectures14 = GetLectures(Chtf14);
            student14.Lectures = lectures14;
            _studentRepositoryService.Update(student14);

            Student student15 = students[15];
            Department department15 = GetDepartmet("Elektros ir elektronikos fakultetas");
            student15.DepartmentId = department15.Id;
            List<string> Eef15 = new List<string> { "Dirbtinio intelekto etika",
                                                    "Inžinerinė grafika",
                                                    "Matematika",
                                                    "Programavimo įvadas inžinieriams", };
            List<Lecture> lectures15 = GetLectures(Eef15);
            student15.Lectures = lectures15;
            _studentRepositoryService.Update(student15);

            Student student16 = students[16];
            Department department16 = GetDepartmet("Ekonomikos ir verslo");
            student16.DepartmentId = department16.Id;
            List<string> Ev16 = new List<string> { "Apskaitos pagrindai",
                                                   "Informatika",
                                                   "Įvadas į finansus", };
            List<Lecture> lectures16 = GetLectures(Ev16);
            student16.Lectures = lectures16;
            _studentRepositoryService.Update(student16);
        }
    }
}
