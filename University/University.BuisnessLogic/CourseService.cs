
using University.BuisnessLogic.Interface;
using University.DAL;
using University.DAL.Repositories.Interfaces;

namespace University.BuisnessLogic
{
    public class CourseService : ICourseService
    {
        private IUnitOfWOrk _unitOfWork;

        public CourseService(IUnitOfWOrk unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateCourse(Cours cours)
        {
            _unitOfWork.Cours.Create(cours);
        }

        public List<Cours> GetAllCourses()
        {
            return _unitOfWork.Cours.GetAll();
        }

        public Cours GetById(int id)
        {
            return _unitOfWork.Cours.GetById(id);
        }

        public void UpdateCourse(Cours course)
        {
            _unitOfWork.Cours.Update(course);
        }

        public void RemoveCourse(int id)
        {
            var cours = GetById(id);
            var groups = _unitOfWork.Groups.GetAll(id);
            if (groups.Count > 0)
            {
                throw new Exception("В курсе есть групы");
            }
            _unitOfWork.Cours.Remove(cours);
        }

    }
}
