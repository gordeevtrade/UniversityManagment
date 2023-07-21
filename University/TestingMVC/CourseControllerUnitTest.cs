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
    public class CourseControllerUnitTest
    {
        Cours english;
        Cours frontennd;
        Cours backend;
        Cours literature;
        Groups group1;
        List<Cours> courses;
        List<Groups> groups;


        public CourseControllerUnitTest()
        {


            english = new Cours { Course_ID = 4, Name = "AmericandLiterature", Description = "Abba" };
            frontennd = new Cours { Course_ID = 2, Name = "JavaScript", Description = "Abbasdv" };
            backend = new Cours { Course_ID = 3, Name = "Java", Description = "dfdf" };

            literature = new Cours { Course_ID = 4, Name = "Literature", Description = "dfdf" };

            group1 = new Groups { Name = "Sr-1", Group_ID = 1, Course_ID = 1 };

            groups = new List<Groups>();
            courses = new List<Cours>();

            courses.Add(frontennd);
            courses.Add(english);
            courses.Add(backend);
        }

        private Mock<ICourseService> _courseServiceMock;
        private Mock<IMapper> _mapperMock;
        private CourseController _controller;

        [TestInitialize]
        public void Setup()
        {
            _courseServiceMock = new Mock<ICourseService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new CourseController(_courseServiceMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public void GetCourseList()
        {
            var courseDTOs = new List<CourseDTO> { new CourseDTO { Course_ID = 1, Name = "Course 1" }, new CourseDTO { Course_ID = 2, Name = "Course 2" }, new CourseDTO { Course_ID = 3, Name = "Course 3" } };
            _courseServiceMock.Setup(s => s.GetAllCourses()).Returns(courses);
            var courseDTO = new List<CourseDTO>();
            _mapperMock.Setup(m => m.Map<List<Cours>, List<CourseDTO>>(courses)).Returns(courseDTOs);
            var result = _controller.GetAllCourse() as ViewResult;
            var model = result.Model as List<CourseDTO>;
            Assert.AreEqual(model.Count, 3);
        }



        [TestMethod]
        public void CreateCourse()
        {
            var lita = new CourseDTO { Course_ID = 4, Name = "Literature", Description = "dfdf" };
            _mapperMock.Setup(m => m.Map<Cours>(lita)).Returns(literature);
            _courseServiceMock.Setup(u => u.CreateCourse(literature)).Callback((Cours course) =>
            {
                courses.Add(course);
            });
            _controller.CreateCourse(lita);
            CollectionAssert.Contains(courses, literature);
        }


        [TestMethod]
        public void RemoveCourseIfContainGroups()
        {
            groups.Add(group1);
            int id = 1;
            var errorMessage = "В курсе есть групы";
            _courseServiceMock.Setup(s => s.RemoveCourse(id)).Throws(new ArgumentException(errorMessage));
            var result = _controller.RemoveCourse(id) as RedirectToActionResult;
            Assert.AreEqual(errorMessage, result.RouteValues["errorMessage"]);
        }


        [TestMethod]
        public void EditCourseGet()
        {
            var lita = new CourseDTO { Course_ID = 4, Name = "Literature", Description = "dfdf" };
            int id = 4;
            _courseServiceMock.Setup(u => u.GetById(id)).Returns((object id) =>
            {
                return courses.Find(p => p.Course_ID == (int)id);
            });

            _mapperMock.Setup(m => m.Map<CourseDTO>(english)).Returns(lita);
            var result = _controller.EditCourse(id) as ViewResult;
            var model = result.Model as CourseDTO;
            Assert.AreEqual(lita, model);

        }
    }

}
