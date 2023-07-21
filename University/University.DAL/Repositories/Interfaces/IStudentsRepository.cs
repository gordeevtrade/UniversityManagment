
namespace University.DAL.Repositories.Interfaces
{
    public interface IStudentsRepository
    {
        List<Students> GetAll(int id);
        bool Create(Students student);
        bool Save();
        Students GetById(int id);
        bool Update(Students student);
        bool Remove(Students student);
    }
}
