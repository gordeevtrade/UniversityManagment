using University.BuisnessLogic.Interface;
using University.DAL;
using University.DAL.Repositories.Interfaces;

namespace University.BuisnessLogic
{
    public class GroupService : IGroupService
    {

        private IUnitOfWOrk _unitOfWork;

        public GroupService(IUnitOfWOrk unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public List<Groups> ShowGroups(int id)
        {
            return _unitOfWork.Groups.GetAll(id);
        }

        public List<Groups> GetAllGroups()
        {
            return _unitOfWork.Groups.GetAllGroups();
        }

        public Groups RetGroup(int id)
        {
            return _unitOfWork.Groups.RetGroup(id);
        }

        public Groups GetById(int id)
        {
            return _unitOfWork.Groups.GetById(id);
        }

        public string CourseName(int id)
        {
            return _unitOfWork.Cours.GetById(id).Name;

        }

        public List<Cours> GetAllCourses()
        {
            return _unitOfWork.Cours.GetAll();
        }

        public void UpdateGroup(Groups group)
        {
            _unitOfWork.Groups.Update(group);
        }

        public void RemoveGroup(int id)
        {
            var group = GetById(id);
            var students = _unitOfWork.Students.GetAll(id);
            if (students.Count > 0)
            {
                throw new Exception("В группе есть студенты");
            }
            _unitOfWork.Groups.Remove(group);
        }

        public void CreateGroup(Groups group)
        {
            _unitOfWork.Groups.Create(group);
        }

    }

}

