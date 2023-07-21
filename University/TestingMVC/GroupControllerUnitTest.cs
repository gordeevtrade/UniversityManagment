
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
    public class GroupControllerUnitTest
    {
        Cours english;
        Groups group1;
        Groups group2;

        Groups group3;

        Students student1;

        List<Cours> courses;
        List<Groups> groups;
        List<Students> students;


        public GroupControllerUnitTest()
        {

            english = new Cours { Course_ID = 1, Name = "AmericandLiterature", Description = "Abba" };
            group1 = new Groups { Name = "Sr-1", Group_ID = 1, Course_ID = 1 };
            group2 = new Groups { Name = "Sr-2", Group_ID = 2, Course_ID = 1 };


            student1 = new Students { First_Name = "JAck", Last_Name = "MIlk", Student_ID = 1, Group_ID = 3 };

            groups = new List<Groups>();
            courses = new List<Cours>();
            students = new List<Students>();


            courses.Add(english);
            groups.Add(group1);
            groups.Add(group2);
        }

        private Mock<IGroupService> _groupService;
        private Mock<IMapper> _mapperMock;
        private GroupsController _controller;

        [TestInitialize]
        public void Setup()
        {
            _groupService = new Mock<IGroupService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new GroupsController(_groupService.Object, _mapperMock.Object);
        }


        [TestMethod]
        public void ShowGroups()
        {
            int courseID = 1;
            var groupsDTOs = new List<GroupDTO> { new GroupDTO { Group_ID = 1, Course_ID = 1, Name = "Sr-1" }, new GroupDTO { Group_ID = 2, Course_ID = 1, Name = "Sr-2" } };
            _groupService.Setup(u => u.CourseName(courseID)).Returns(english.Name);
            var listOfGrpoup = groups.FindAll(p => p.Course_ID == courseID);

            if (listOfGrpoup.Count > 0)
            {
                _groupService.Setup(u => u.ShowGroups(courseID)).Returns(groups);
                _mapperMock.Setup(m => m.Map<List<GroupDTO>>(groups)).Returns(groupsDTOs);
            }
            else
            {
                _groupService.Setup(u => u.ShowGroups(courseID)).Returns(value: null);
            }

            var result = _controller.ShowGroups(courseID) as ViewResult;
            var model = result.Model as List<GroupDTO>;
            Assert.AreEqual(model.Count, 2);
        }


        [TestMethod]
        public void CreateGroup()
        {
            Groups group3 = new Groups { Name = "Sr-3", Group_ID = 3, Course_ID = 1 };
            GroupDTO group3DTO = new GroupDTO { Name = "Sr-3", Group_ID = 3, Course_ID = 1 };

            _mapperMock.Setup(m => m.Map<Groups>(group3DTO)).Returns(group3);

            _groupService.Setup(u => u.CreateGroup(group3)).Callback((Groups group) =>
            {
                groups.Add(group);
            });
            _controller.AddGroup(group3DTO);
            CollectionAssert.Contains(groups, group3);
        }

        [TestMethod]
        public void EditGroup()
        {
            GroupDTO group3DTO = new GroupDTO { Name = "Sr-3", Group_ID = 3, Course_ID = 1 };
            Groups group3 = new Groups { Name = "Sr-3", Group_ID = 3, Course_ID = 1 };

            int id = 1;

            _mapperMock.Setup(m => m.Map<GroupDTO>(group3)).Returns(group3DTO);

            _groupService.Setup(u => u.GetById(id)).Returns((object id) =>
            {
                return groups.Find(p => p.Group_ID == (int)id);
            });

            _groupService.Setup(u => u.GetAllCourses()).Returns(courses);



            var result = _controller.EditGroup(group3DTO) as RedirectToActionResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("ShowGroups", result.ActionName);
            Assert.AreEqual("Groups", result.ControllerName);

        }

        [TestMethod]
        public void RemoveGroupIfContainStudents()
        {
            groups.Add(group1);
            int id = 1;
            var errorMessage = "В группе есть студенты";
            _groupService.Setup(s => s.RemoveGroup(id)).Throws(new ArgumentException(errorMessage));
            var result = _controller.RemoveGroup(id) as RedirectToActionResult;
            Assert.AreEqual(errorMessage, result.RouteValues["errorMessage"]);
        }
    }
}
