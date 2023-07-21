using University.DAL;

namespace University.BuisnessLogic.Interface
{
    public interface IGroupService
    {
        public List<Groups> ShowGroups(int id);
        public Groups RetGroup(int id);
        public Groups GetById(int id);
        public void UpdateGroup(Groups group);
        public void RemoveGroup(int id);
        public void CreateGroup(Groups group);
        public List<Groups> GetAllGroups();
        public string CourseName(int id);
        public List<Cours> GetAllCourses();


    }
}
