using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.BuisnessLogic.DTO;
using University.BuisnessLogic.Interface;
using University.DAL;

namespace University.Controllers
{
    public class StudentController : Controller
    {
        private IMapper _mapper;
        private IStudentsService _studentsService;
        public StudentController(IStudentsService studentsService, IMapper mapper)
        {
            _studentsService = studentsService;
            _mapper = mapper;
        }

        public IActionResult ShowStudents(int id)
        {
            var students = _studentsService.GetAllStudents(id);
            var studentsDTO = _mapper.Map<List<StudentDTO>>(students);
            var group = _studentsService.GetGroups(id);
            var groupDTO = _mapper.Map<Groups, GroupDTO>(group);
            ViewBag.GroupName = groupDTO.Name;
            ViewData["Group_Id"] = id;
            return View(studentsDTO);
        }


        [HttpGet]
        public IActionResult CreateStudent()
        {
            ViewData["Group_Id"] = GetGroups();
            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(StudentDTO studentDTO)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("CreateStudent", "Student");
            }
            var studentEntity = _mapper.Map<Students>(studentDTO);
            _studentsService.StudentCreate(studentEntity);
            return RedirectToAction("ShowStudents", "Student", new { id = studentDTO.Group_ID });
        }
        public IActionResult RemoveStudent(int id)
        {
            var group = _studentsService.GetById(id).Group_ID;
            _studentsService.Remove(id);
            return RedirectToAction("ShowStudents", "Student", new { id = group });
        }

        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            var student = _studentsService.GetById(id);
            var studentDTO = _mapper.Map<StudentDTO>(student);
            ViewData["Group_Id"] = GetGroups();
            return View(studentDTO);
        }

        [HttpPost]
        public IActionResult EditStudent(StudentDTO studentsDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Group_Id"] = GetGroups();
                return View(studentsDTO);
            }
            var studentEntity = _mapper.Map<Students>(studentsDTO);
            _studentsService.Update(studentEntity);
            return RedirectToAction("ShowStudents", "Student", new { id = studentsDTO.Group_ID });
        }

        private SelectList GetGroups()
        {
            var groups = _studentsService.GetAllGroups();
            var groupsListDTO = _mapper.Map<List<GroupDTO>>(groups);
            return new SelectList(groupsListDTO, "Group_ID", "Name");
        }
    }
}
