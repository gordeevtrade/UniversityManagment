using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using University.BuisnessLogic.DTO;
using University.BuisnessLogic.Interface;
using University.DAL;

namespace University.Controllers
{
    public class CourseController : Controller
    {
        private ICourseService _couseService;
        private IMapper _mapper;
        public CourseController(ICourseService courseService, IMapper mapper)
        {
            _couseService = courseService;
            _mapper = mapper;
        }
        public IActionResult GetAllCourse()
        {
            var courses = _couseService.GetAllCourses();
            var courseDTO = _mapper.Map<List<Cours>, List<CourseDTO>>(courses);
            return View(courseDTO);
        }

        [HttpGet]
        public IActionResult CreateCourse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCourse(CourseDTO coursDto)
        {
            if (!ModelState.IsValid)
            {
                return View(coursDto);
            }
            var course = _mapper.Map<Cours>(coursDto);
            _couseService.CreateCourse(course);
            return RedirectToAction(nameof(GetAllCourse));
        }

        public IActionResult RemoveCourse(int id)
        {
            try
            {
                _couseService.RemoveCourse(id);
                return RedirectToAction("GetAllCourse", "Course");
            }
            catch (Exception ex)
            {
                return RedirectToAction("CourseError", "Course", new { errorMessage = ex.Message });
            }
        }

        public IActionResult CourseError(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }

        [HttpGet]
        public IActionResult EditCourse(int id)
        {
            var course = _couseService.GetById(id);
            var courseDTO = _mapper.Map<CourseDTO>(course);
            return View(courseDTO);
        }

        [HttpPost]
        public IActionResult EditCourse(CourseDTO coursDto)
        {
            if (!ModelState.IsValid)
            {
                return View(coursDto);
            }
            var course = _mapper.Map<Cours>(coursDto);
            _couseService.UpdateCourse(course);
            return RedirectToAction("GetAllCourse", "Course");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}