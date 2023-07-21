
namespace University.DAL.Repositories.Interfaces
{
    public interface IUnitOfWOrk
    {
        ICourseRepositiy Cours { get; }
        IGroupRepository Groups { get; }
        IStudentsRepository Students { get; }
    }
}
