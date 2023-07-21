
using University.DAL.Repositories.Interfaces;

namespace University.DAL.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private UniversityContext context;
        public GroupRepository(UniversityContext _context)
        {
            context = _context;
        }
        public List<Groups> GetAll(int id)
        {
            return context.Groups.Where(x => x.courses.Course_ID == id).ToList();
        }
        public List<Groups> GetAllGroups()
        {
            return context.Groups.ToList();
        }
        public Groups RetGroup(int id)
        {
            Groups group = context.Groups.FirstOrDefault(x => x.Group_ID == id);
            return group;
        }

        public bool Create(Groups groups)
        {
            var newGroup = groups;
            context.Groups.Add(newGroup);
            return Save();
        }

        public bool Remove(Groups group)
        {
            context.Groups.Remove(group);
            return Save();
        }
        public bool Update(Groups updateGroup)
        {
            context.Groups.Update(updateGroup);
            return Save();
        }
        public Groups GetById(int id)
        {
            return context.Groups.Find(id);
        }
        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
