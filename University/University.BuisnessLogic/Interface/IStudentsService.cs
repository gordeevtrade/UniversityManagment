using University.DAL;

namespace University.BuisnessLogic.Interface
{
    public interface IStudentsService
    {
        public void StudentCreate(Students student);
        public List<Students> GetAllStudents(int id);
        public Students GetById(int id);
        public void Remove(int id);
        public void Update(Students student);
        public Groups GetGroups(int id);
        public List<Groups> GetAllGroups();

    }
}
