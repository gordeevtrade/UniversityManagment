
namespace University.DAL.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Groups GetById(int id);
        List<Groups> GetAll(int id);
        bool Create(Groups group);
        bool Remove(Groups group);
        bool Update(Groups group);
        bool Save();
        Groups RetGroup(int id);
        List<Groups> GetAllGroups();
    }
}
