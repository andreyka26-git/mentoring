using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskController
    {
        private readonly UserTaskService _taskService;

        public UserTaskController(UserTaskService taskService)
        {
            _taskService = taskService;
        }

        public bool AddTaskForUser(int userId, string description, IResponseModel model)
        {
            string message = GetMessageForModel(userId, description);
            if (message != null)
            {
                model.AddAttribute("action_result", message);
                return false;
            }

            return true;
        }

        private string GetMessageForModel(int userId, string description)
        {
            var task = new UserTask(description);
            int result = _taskService.AddTaskForUser(userId, task);
            if (result == -1)
                return "Invalid userId";

            if (result == -2)
                return "User not found";

            if (result == -3)
                return "The task already exists";

            return null;
        }
    }
}