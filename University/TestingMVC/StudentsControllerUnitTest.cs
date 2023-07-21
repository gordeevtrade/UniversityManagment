using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using University.BuisnessLogic.DTO;
using University.BuisnessLogic.Interface;
using University.Controllers;
using University.DAL;

namespace TestingMVC
{
    [TestClass]
    public class StudentsControllerUnitTest
    {
        Cours english;
        Groups group1;
        Groups group2;

        Groups group3;

        Students student1;
        Students student2;
        Students student4;
        Students student3;

        List<Cours> courses;
        List<Groups> groups;
        List<Students> students;

        Mock<IStudentsService> _studentsService;
        Mock<IMapper> _mapperMock;
        StudentController _controller;

        public StudentsControllerUnitTest()
        {

            english = new Cours { Course_ID = 1, Name = "AmericandLiterature", Description = "Abba" };
            group1 = new Groups { Name = "Sr-1", Group_ID = 1, Course_ID = 1 };
            group2 = new Groups { Name = "Sr-2", Group_ID = 2, Course_ID = 1 };

            group3 = new Groups { Name = "Sr-3", Group_ID = 3, Course_ID = 1 };

            student1 = new Students { First_Name = "JAck", Last_Name = "MIlk", Student_ID = 1, Group_ID = 1 };
            student2 = new Students { First_Name = "JAcke", Last_Name = "Poll", Student_ID = 2, Group_ID = 1 };
            student3 = new Students { First_Name = "Floid", Last_Name = "Maivezer", Student_ID = 3, Group_ID = 1 };

            groups = new List<Groups>();
            courses = new List<Cours>();

            students = new List<Students>();

            courses.Add(english);
            groups.Add(group1);
            groups.Add(group2);

        }



        [TestInitialize]
        public void Setup()
        {
            _studentsService = new Mock<IStudentsService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new StudentController(_studentsService.Object, _mapperMock.Object);
        }


        [TestMethod]
        public void ShowStudents()
        {
            GroupDTO group1DTO = new GroupDTO { Name = "Sr-1", Group_ID = 1, Course_ID = 1 };

            var studentsDTOs = new List<StudentDTO> { new StudentDTO { Group_ID = 1, First_Name = "JAck", Last_Name = "MIlk", Student_ID = 1 }, new StudentDTO { First_Name = "JAcke", Last_Name = "Poll", Student_ID = 2, Group_ID = 1 } };
            students.Add(student1);
            students.Add(student2);
            int id = 1;
            _studentsService.Setup(u => u.GetGroups(id)).Returns(group1);
            _mapperMock.Setup(m => m.Map<List<StudentDTO>>(students)).Returns(studentsDTOs);
            _mapperMock.Setup(m => m.Map<Groups, GroupDTO>(group1)).Returns(group1DTO);

            var listOfStudents = students.FindAll(p => p.Group_ID == id);

            if (listOfStudents.Count > 0)
            {
                _studentsService.Setup(u => u.GetAllStudents(id)).Returns(students);
            }
            else
            {
                _studentsService.Setup(u => u.GetAllStudents(id)).Returns(value: null);
            }

            var result = _controller.ShowStudents(id) as ViewResult;
            var model = result.Model as List<StudentDTO>;
            Assert.AreEqual(model.Count, 2);
        }

        [TestMethod]
        public void CreateStudent()
        {
            var newStudentDTO = new StudentDTO { Group_ID = 1, First_Name = "Papai", Last_Name = "MIlk", Student_ID = 4 };
            var newStudentEntity = new Students { Group_ID = 1, First_Name = "Papai", Last_Name = "MIlk", Student_ID = 4 };

            _mapperMock.Setup(m => m.Map<Students>(newStudentDTO)).Returns(newStudentEntity);
            _studentsService.Setup(u => u.StudentCreate(newStudentEntity)).Callback((Students newStudent) =>
            {
                students.Add(newStudent);
            });
            _controller.CreateStudent(newStudentDTO);
            CollectionAssert.Contains(students, newStudentEntity);

        }


        [TestMethod]
        public void EditStudent()
        {

            StudentDTO studentDTO = new StudentDTO { Group_ID = 1, First_Name = "Papai", Last_Name = "MIlk", Student_ID = 4 };
            Students stud3 = new Students { Group_ID = 1, First_Name = "Papai", Last_Name = "MIlk", Student_ID = 4 };

            int id = 1;

            _mapperMock.Setup(m => m.Map<StudentDTO>(stud3)).Returns(studentDTO);

            _studentsService.Setup(u => u.GetById(id)).Returns((object id) =>
            {
                return students.Find(p => p.Student_ID == (int)id);
            });

            _studentsService.Setup(u => u.GetAllGroups()).Returns(groups);

            // Act
            var result = _controller.EditStudent(studentDTO) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ShowStudents", result.ActionName);
            Assert.AreEqual("Student", result.ControllerName);
        }

        [TestMethod]
        public void RemoveStudent()
        {
            int id = 4;
            student4 = new Students { First_Name = "Manny", Last_Name = "PAciao", Student_ID = 4, Group_ID = 1 };
            students.Add(student4);

            _studentsService.Setup(u => u.Remove(id)).Callback((int cid) =>
            {
                var stud = students.Find(p => p.Student_ID == cid);
                students.Remove(stud);
            });

            _studentsService.Setup(u => u.GetById(id)).Returns(student4);
            _controller.RemoveStudent(id);
            CollectionAssert.DoesNotContain(students, student4);
        }
    }
}

