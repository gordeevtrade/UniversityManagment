
using University.DAL.Repositories.Interfaces;

namespace University.DAL.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        private UniversityContext context;
        public StudentsRepository(UniversityContext _context)
        {
            context = _context;
        }
        public List<Students> GetAll(int id)
        {
            return context.Students.Where(x => x.Group_ID == id).ToList();
        }
        public bool Create(Students student)
        {
            var stud = student;
            context.Students.Add(stud);
            return Save();
        }
        public Students GetById(int id)
        {
            return context.Students.Find(id);
        }
        public bool Remove(Students student)
        {
            context.Students.Remove(student);
            return Save();
        }
        public bool Update(Students student)
        {
            context.Students.Update(student);
            return Save();
        }
        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
