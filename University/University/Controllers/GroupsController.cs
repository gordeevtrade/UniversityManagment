using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.BuisnessLogic.DTO;
using University.BuisnessLogic.Interface;
using University.DAL;
namespace University.Controllers
{
    public class GroupsController : Controller
    {
        private IGroupService _groupService;
        private IMapper _mapper;
        private Groups groupEntity;

        public GroupsController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }
        public IActionResult ShowGroups(int id)
        {
            ViewBag.CourseName = _groupService.CourseName(id);
            var groupsEntityList = _groupService.ShowGroups(id);
            var groupsDTOList = _mapper.Map<List<GroupDTO>>(groupsEntityList);
            return View(groupsDTOList);
        }

        public IActionResult ShowGroup(int id)
        {
            var entityGroup = _groupService.RetGroup(id);
            var groupDTO = _mapper.Map<GroupDTO>(entityGroup);
            return View(groupDTO);
        }

        [HttpGet]
        public IActionResult EditGroup(int id)
        {
            groupEntity = _groupService.GetById(id);
            var groupDTO = _mapper.Map<GroupDTO>(groupEntity);
            ViewData["Course_Id"] = GetCourses();
            return View(groupDTO);
        }

        [HttpPost]
        public IActionResult EditGroup(GroupDTO groupDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Course_Id"] = GetCourses();
                return View(groupDTO);
            }
            groupEntity = _mapper.Map<Groups>(groupDTO);
            _groupService.UpdateGroup(groupEntity);
            return RedirectToAction("ShowGroups", "Groups", new { id = groupDTO.Course_ID });
        }

        public IActionResult RemoveGroup(int id)
        {
            try
            {
                _groupService.RemoveGroup(id);
                return RedirectToAction("ShowGroups", "Groups", new { id = groupEntity.Course_ID });
            }
            catch (Exception ex)
            {
                return RedirectToAction("RemoveGroupMessage", "Groups", new { errorMessage = ex.Message, id = id });
            }
        }

        public IActionResult RemoveGroupMessage(int id)
        {
            ViewData["Group_Id"] = id;
            return View();
        }

        [HttpGet]
        public IActionResult AddGroup()
        {
            ViewData["Course_Id"] = GetCourses();
            return View();
        }

        [HttpPost]
        public IActionResult AddGroup(GroupDTO groupDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Course_Id"] = GetCourses();
                return View(groupDTO);
            }
            groupEntity = _mapper.Map<Groups>(groupDTO);
            _groupService.CreateGroup(groupEntity);
            return RedirectToAction("ShowGroups", "Groups", new { id = groupDTO.Course_ID });
        }


        private SelectList GetCourses()
        {
            var courses = _groupService.GetAllCourses();
            var courseListDTO = _mapper.Map<List<CourseDTO>>(courses);
            return new SelectList(courseListDTO, "Course_ID", "Name");
        }
    }
}
