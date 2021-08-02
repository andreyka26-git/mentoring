using System;
using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskService
    {
        private readonly IUserDao _userDao;

        public UserTaskService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public void AddTaskForUser(int userId, UserTask task)
        {
            if (userId < 0)
                throw new ArgumentException("Invalid userId");

            var user = _userDao.GetUser(userId);
            if (user == null)
                throw new ArgumentNullException(message: "User not found", innerException: null);

            var tasks = user.Tasks;
            foreach (var t in tasks)
            {
                if (string.Equals(task.Description, t.Description, StringComparison.OrdinalIgnoreCase))
                    throw new Exception("User has already had task");
            }

            tasks.Add(task);
        }
    }
}