
using University.DAL.Repositories.Interfaces;
namespace University.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWOrk
    {
        private UniversityContext _context;
        private IGroupRepository _groupRepository;
        private ICourseRepositiy _coursRepository;
        private IStudentsRepository _studentsRepository;
        public UnitOfWork(UniversityContext context)
        {
            _context = context;
        }

        public ICourseRepositiy Cours
        {
            get
            {
                if (_coursRepository == null)

                    _coursRepository = new CoursRepository(_context);
                return _coursRepository;
            }
        }

        public IGroupRepository Groups
        {
            get
            {
                if (_groupRepository == null)
                    _groupRepository = new GroupRepository(_context);
                return _groupRepository;
            }
        }

        public IStudentsRepository Students
        {
            get
            {
                if (_studentsRepository == null)
                    _studentsRepository = new StudentsRepository(_context);
                return _studentsRepository;
            }
        }
    }
}
