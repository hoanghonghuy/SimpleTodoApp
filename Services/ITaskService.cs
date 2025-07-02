using SimpleTodoApp.Models;

namespace SimpleTodoApp.Services
{
    public interface ITaskService
    {
        List<TaskEntity> GetAllTasks();
        TaskEntity GetTaskById(int id);
        void CreateTask(CreateTaskViewModel task);
        void UpdateTask(TaskEntity task);
        void DeleteTask(int id);

    }
}
