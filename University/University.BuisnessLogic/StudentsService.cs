using University.BuisnessLogic.Interface;
using University.DAL;
using University.DAL.Repositories.Interfaces;

namespace University.BuisnessLogic
{
    public class StudentsService : IStudentsService
    {
        private IUnitOfWOrk _unitOfWork;

        public StudentsService(IUnitOfWOrk unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Students> GetAllStudents(int id)
        {
            return _unitOfWork.Students.GetAll(id);

        }

        public void StudentCreate(Students student)
        {
            _unitOfWork.Students.Create(student);
        }

        public Students GetById(int id)
        {
            return _unitOfWork.Students.GetById(id);
        }

        public Groups GetGroups(int id)
        {
            return _unitOfWork.Groups.GetById(id);
        }

        public List<Groups> GetAllGroups()
        {
            return _unitOfWork.Groups.GetAllGroups();
        }

        public void Update(Students student)
        {
            _unitOfWork.Students.Update(student);
        }

        public void Remove(int id)
        {
            var student = GetById(id);
            _unitOfWork.Students.Remove(student);
        }

    }
}
