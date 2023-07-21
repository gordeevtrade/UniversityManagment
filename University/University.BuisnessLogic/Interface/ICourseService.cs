
using University.DAL;

namespace University.BuisnessLogic.Interface
{
    public interface ICourseService
    {
        public void CreateCourse(Cours cours);
        public List<Cours> GetAllCourses();
        public Cours GetById(int id);
        public void UpdateCourse(Cours course);
        public void RemoveCourse(int id);
    }
}
