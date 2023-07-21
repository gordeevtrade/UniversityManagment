using University.DAL.Repositories.Interfaces;
namespace University.DAL.Repositories
{
    public class CoursRepository : ICourseRepositiy
    {
        private UniversityContext context;
        public CoursRepository(UniversityContext _context)
        {
            context = _context;
        }
        public List<Cours> GetAll()
        {
            return context.Courses.ToList();
        }

        public bool Create(Cours course)
        {
            var newCourse = course;
            context.Courses.Add(newCourse);
            return Save();
        }


        public bool Remove(Cours cours)
        {
            var newGroup = cours;
            context.Courses.Remove(newGroup);
            return Save();
        }

        public bool Update(Cours updateCourse)
        {
            var oldCourse = context.Courses.Where(x => x.Course_ID == updateCourse.Course_ID).FirstOrDefault();
            oldCourse.Name = updateCourse.Name;
            oldCourse.Description = updateCourse.Description;
            oldCourse.Course_ID = updateCourse.Course_ID;
            context.Courses.Update(oldCourse);
            return Save();
        }
        public Cours GetById(int id)
        {
            return context.Courses.Find(id);
        }
        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

