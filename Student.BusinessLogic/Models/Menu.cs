using UniExam.BusinessLogic.Enumerables;
using UniExam.BusinessLogic.Interfaces;
using UniExam.BusinessLogic.Services;
using UniExam.BusinessLogic.Utilities;
using UniExam.Repository.Models;

namespace UniExam.BusinessLogic.Models
{
    public class Menu
    {
        private readonly IDepartmentRepositoryService _departmentRepositoryService;
        private readonly ILectureRepositoryService _lectureRepositoryService;
        private readonly IStudentRepositoryService _studentRepositoryService;

        public Menu(IDepartmentRepositoryService departmentRepositoryService, ILectureRepositoryService lectureRepositoryService, IStudentRepositoryService studentRepositoryService)
        {
            _departmentRepositoryService = departmentRepositoryService;
            _lectureRepositoryService = lectureRepositoryService;
            _studentRepositoryService = studentRepositoryService;
        }

        public void GetMainMenu()
        {
            Console.Clear();
            ConsoleColor.Green.WriteLine("[1] Create Management");
            ConsoleColor.Green.WriteLine("[2] Modify Management");
            ConsoleColor.Green.WriteLine("[3] Report Management");
            ConsoleColor.Cyan.WriteLine("[99] Admin Panel");
            ConsoleColor.Magenta.WriteLine("[X] Close console");
            
            GetSubmenuBySelection();
        }

        private void GetSubmenuBySelection()
        {
            while (true)
            {
                ConsoleColor.Green.WriteLine("--------------------");
                ConsoleColor.Green.WriteLine("Select option:");
                switch (Console.ReadLine().ToLower())
                {
                    case "1":
                        GetCreationMenu();
                        break;
                    case "2":
                        GetModificationMenu();
                        break;
                    case "3":
                        GetReportMenu();
                        break;
                    case "99":
                        GetAdminMenu();
                        break;
                    case "x":
                        Exit();
                        break;
                    default:
                        GetInputErrorMessage();
                        break;
                }
            }
        }

        private void GetCreationMenu()
        {
            Console.Clear();
            ConsoleColor.Green.WriteLine("[1] Create Department");
            ConsoleColor.Green.WriteLine("[2] Create Lecture"); // Čia dar reikės padaryti, kad jei kuria Lecture ir nori priskirti prie Departmento, kad jei nėra tokio deparmento iškristų pasirinkimas jį sukurti
            ConsoleColor.Green.WriteLine("[3] Create Student"); // Sukuriant Studentą priskirti jam depatmenetą su paskaitomis
            ConsoleColor.Magenta.WriteLine("[0] Back to Main Menu.");
            ConsoleColor.Magenta.WriteLine("[X] Close console");

            GetCreationFunctionBySelection();
        }

        private void GetModificationMenu()
        {
            Console.Clear();
            ConsoleColor.Green.WriteLine("[1] Assign Students to Department");
            ConsoleColor.Green.WriteLine("[2] Assign Lectures to Department");
            ConsoleColor.Green.WriteLine("[3] Assign Lectures to Student");
            ConsoleColor.Green.WriteLine("[4] Transfer Student to another Department");
            ConsoleColor.Magenta.WriteLine("[0] Back to Main Menu.");
            ConsoleColor.Magenta.WriteLine("[X] Close console");

            GetModificationFunctionBySelection();
        }

        public void GetReportMenu()
        {
            Console.Clear();
            ConsoleColor.Green.WriteLine("[1] Get all Studends by Department");
            ConsoleColor.Green.WriteLine("[2] Get all Lectures by Department");
            ConsoleColor.Green.WriteLine("[3] Get all Lectures by Student");
            ConsoleColor.Magenta.WriteLine("[0] Back to Main Menu.");
            ConsoleColor.Magenta.WriteLine("[X] Close console");

            GetReportBySelection();
        }

        public void GetAdminMenu()
        {
            Console.Clear();
            ConsoleColor.Magenta.WriteLine("[1] Reset Database");
            ConsoleColor.Magenta.WriteLine("[2] Delete Student");
            ConsoleColor.Magenta.WriteLine("[3] Delete All Students");
            ConsoleColor.Magenta.WriteLine("[4] Delete Lecture");
            ConsoleColor.Magenta.WriteLine("[5] Delete All Lectures");
            ConsoleColor.Magenta.WriteLine("[6] Delete Department");
            ConsoleColor.Magenta.WriteLine("[7] Delete All Departments");
            ConsoleColor.Magenta.WriteLine("[0] Back to Main Menu.");
            ConsoleColor.Magenta.WriteLine("[X] Close console");

            GetAdminFunctionBySelection();
        }

        private void GetCreationFunctionBySelection()
        {
            while (true)
            {
                ConsoleColor.Green.WriteLine("--------------------");
                ConsoleColor.Green.WriteLine("Select option from menu by number::");
                switch (Console.ReadLine().ToLower())
                {
                    case "1":
                        InitiateNewDepartment();
                        break;
                    case "2":
                        InitiateNewLecture();
                        break;
                    case "3":
                        InitiateNewStudent();
                        break;
                    case "0":
                        GetMainMenu();
                        break;
                    case "x":
                        Exit();
                        break;
                    default:
                        GetInputErrorMessage();
                        break;
                }
            }
        }

        private void InitiateNewDepartment()
        {
            Department department = _departmentRepositoryService.CreateNewDepartment();

            ConsoleColor.Green.WriteLine("Do you want assign lectures for Department from List (y/n):");
            Answer answer = Question();

            if(answer == Answer.Yes)
            {
                List<Lecture> lectures = _lectureRepositoryService.GetSelectedLectures();
                department.Lectures = lectures;
                _departmentRepositoryService.Update(department);
                ConsoleColor.Blue.WriteLine($"Lectures was assigned to {department.Name}");
            }
        }

        private Answer Question()
        {
            return Console.ReadLine().ToLower() switch
            {
                "y" => Answer.Yes,
                "n" => Answer.No,
                _ => Question(),
            };
        }

        private void InitiateNewLecture()
        {
            Lecture lecture = _lectureRepositoryService.CreateNewLecture();
        }

        private void InitiateNewStudent()
        {
            Student student = _studentRepositoryService.CreateNewStudent();
            ConsoleColor.Green.WriteLine("Do you want assign Department from List (y/n):");
            Answer answer = Question();
            if(answer == Answer.Yes )
            {
                Department department = _departmentRepositoryService.GetSelectedDepartment();
                student.Department = department;
                //_studentRepositoryService.Update(student);

                ConsoleColor.Green.WriteLine("Do you want assign Lectures from List (y/n):");
                Answer answer1 = Question();
                if(answer1 == Answer.Yes )
                {
                    List<Lecture> lectures = _lectureRepositoryService.GetLecturesByDepartmentId(department.Id);
                    _lectureRepositoryService.Print(lectures);
                    List<Lecture> SelectedLectures = _lectureRepositoryService.SelectLectures(lectures);
                    student.Lectures = lectures;
                    //_studentRepositoryService.Update(student);
                }

                _studentRepositoryService.Update(student);
            }
        }

        private void GetModificationFunctionBySelection()
        {
            while (true)
            {
                ConsoleColor.Green.WriteLine("--------------------");
                ConsoleColor.Green.WriteLine("Select option from menu by number:");
                switch (Console.ReadLine().ToLower())
                {
                    case "1":
                        AssignStudentsToDepartment();
                        break;
                    case "2":
                        AssignLecturesToDepartment();
                        break;
                    case "3":
                        AssignLecturesToStudent();
                        break;
                    case "4":
                        TransferStudentBetweenDepartments();
                        break;
                    case "0":
                        GetMainMenu();
                        break;
                    case "x":
                        Exit();
                        break;
                    default:
                        GetInputErrorMessage();
                        break;
                }
            }
        }

        private void AssignStudentsToDepartment()
        {
            Department department = _departmentRepositoryService.GetSelectedDepartment();

            int? IdIsNull = null;
            List<Student> students = _studentRepositoryService.GetStudentsByDepartmentId(IdIsNull);
            if(students.Any())
            {
                _studentRepositoryService.Print(students);
                ConsoleColor.Green.WriteLine("Select Students separated by comma (\",\"):");
                List<Student> SelectedStudents = _studentRepositoryService.GetSelectedStudents(students);
                foreach (Student student in SelectedStudents)
                {
                    student.DepartmentId = department.Id;
                    _studentRepositoryService.Update(student);
                    ConsoleColor.Blue.WriteLine($"{student.FirstName} {student.LastName} was assigned to {department.Name}.");
                }
            }
            else
            {
                ConsoleColor.Blue.WriteLine("There is no Students withoud assignet Deparment");
            }
        }

        private void AssignLecturesToDepartment()
        {
            Department department = _departmentRepositoryService.GetSelectedDepartment();

            List<Lecture> SelectedLectures = _lectureRepositoryService.GetSelectedLectures();

            department.Lectures = SelectedLectures;
            _departmentRepositoryService.Update(department);
            ConsoleColor.Blue.WriteLine($"Lectures was assigned to {department.Name}");
        }

        private void AssignLecturesToStudent()
        {
            Student student = _studentRepositoryService.GetSelectedStudent();
            List<Lecture> lectures = _lectureRepositoryService.GetLecturesByDepartmentId(student.DepartmentId);
            _lectureRepositoryService.Print(lectures);
            List<Lecture> SelectedLectures = _lectureRepositoryService.SelectLectures(lectures);
            student.Lectures = SelectedLectures;
            _studentRepositoryService.Update(student);
        }

        private void TransferStudentBetweenDepartments()
        {
            Student student = _studentRepositoryService.GetSelectedStudent();
            List<Department> departments = _departmentRepositoryService.GetDepartmentsExcept(student.DepartmentId);
            _departmentRepositoryService.Print(departments);
            ConsoleColor.Green.WriteLine("Select Department where you wanna transfer Student");
            Department department =_departmentRepositoryService.SelectDepartment(departments);

            _lectureRepositoryService.DeleteConnections(student.Id);
            student.DepartmentId = department.Id;

            List<Lecture> lectures = _lectureRepositoryService.GetLecturesByDepartmentId(department.Id);
            _lectureRepositoryService.Print(lectures);
            List<Lecture> SelectedLectures = _lectureRepositoryService.SelectLectures(lectures);
            student.Lectures = SelectedLectures;

            _studentRepositoryService.Update(student);
            ConsoleColor.Blue.WriteLine($"{student.FirstName} {student.LastName} was transferd to {department.Name}");
        }

        private void GetReportBySelection()
        {
            while (true)
            {
                ConsoleColor.Green.WriteLine("--------------------");
                ConsoleColor.Green.WriteLine("Select option by number:");
                switch (Console.ReadLine().ToLower())
                {
                    case "1":
                        InitializeGetStudentsByDepartment();
                        break;
                    case "2":
                        InitializeGetLecturesByDepartment();
                        break;
                    case "3":
                        InitializeGetLecturesByStudent();
                        break;
                    case "0":
                        GetMainMenu();
                        break;
                    case "x":
                        Exit();
                        break;
                    default:
                        GetInputErrorMessage();
                        break;
                }
            }
        }

        private void GetAdminFunctionBySelection()
        {
            while (true)
            {
                ConsoleColor.Green.WriteLine("--------------------");
                ConsoleColor.Green.WriteLine("Select option by number:");
                switch (Console.ReadLine().ToLower())
                {
                    case "1":
                        InitializeResetDatabase();
                        break;
                    case "2":
                        DeleteStudent();
                        break;
                    case "3":
                        DeleteAllStudents();
                        break;
                    case "4":
                        DeleteLecture();
                        break;
                    case "5":
                        DeleteAllLectures();
                        break;
                    case "6":
                        DeleteDepartment();
                        break;
                    case "7":
                        DeleteAllDepartments();
                        break;
                    case "0":
                        GetMainMenu();
                        break;
                    case "x":
                        Exit();
                        break;
                    default:
                        GetInputErrorMessage();
                        break;
                }
            }
        }

        private void InitializeGetStudentsByDepartment()
        {
            Department department = _departmentRepositoryService.GetSelectedDepartment();

            List<Student> students = _studentRepositoryService.GetStudentsByDepartmentId(department.Id);
            if(students.Any())
            {
                _studentRepositoryService.Print(students);
            }
            else
            {
                ConsoleColor.Blue.WriteLine($"There is no students in department: {department.Name}.");
            }
        }

        private void InitializeGetLecturesByDepartment()
        {
            Department department = _departmentRepositoryService.GetSelectedDepartment();

            List<Lecture> lectures = _lectureRepositoryService.GetLecturesByDepartmentId(department.Id);
            if(lectures.Any())
            {
                _lectureRepositoryService.Print(lectures);
            }
            else
            {
                ConsoleColor.Blue.WriteLine($"There is no asigned lectures for department: {department.Name}.");
            }
        }

        private void InitializeGetLecturesByStudent()
        {
            Student student = _studentRepositoryService.GetSelectedStudent();

            List<Lecture> lectures = _lectureRepositoryService.GetLecturesByStudentId(student.Id);
            if (lectures.Any())
            {
                _lectureRepositoryService.Print(lectures);
            }
            else
            {
                ConsoleColor.Blue.WriteLine($"There is no asigned lectures for student: {student.FirstName} {student.LastName}.");
            }
        }

        private void InitializeResetDatabase()
        {
            DeleteAllLectures();
            DeleteAllStudents();
            DeleteAllDepartments();
        }

        private void DeleteDepartment()
        {
            Department department = _departmentRepositoryService.GetSelectedDepartment();
            _departmentRepositoryService.Delete(department);
        }

        private void DeleteLecture()
        {
            Lecture lecture = _lectureRepositoryService.GetSelectedLecture();
            _lectureRepositoryService.Delete(lecture);
        }

        private void DeleteStudent()
        {
            Student student = _studentRepositoryService.GetSelectedStudent();
            _studentRepositoryService.Delete(student);
        }

        private void DeleteAllLectures()
        {
            _lectureRepositoryService.DeleteAllLectures();
        }

        private void DeleteAllStudents()
        {
            _studentRepositoryService.DeleteAllStudents();
        }

        private void DeleteAllDepartments()
        {
            _departmentRepositoryService.DeleteAllDepartments();
        }

        private void Exit()
        {
            Console.Clear();
            ConsoleColor.Yellow.WriteLine("Good Bye!");
            Thread.Sleep(1000);
            Environment.Exit(0);
        }

        private void GetInputErrorMessage()
        {
            ConsoleColor.Red.WriteLine("Wrong selection. Select option by typing value in keyboard.");
        }
    }
}
