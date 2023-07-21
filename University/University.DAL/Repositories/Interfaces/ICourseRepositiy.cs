
namespace University.DAL.Repositories.Interfaces
{
    public interface ICourseRepositiy
    {
        List<Cours> GetAll();
        bool Create(Cours cours);
        public bool Remove(Cours cours);
        bool Update(Cours cours);
        Cours GetById(int id);
        bool Save();
    }
}
