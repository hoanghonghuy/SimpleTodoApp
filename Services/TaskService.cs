namespace SimpleTodoApp.Services;
using System.Collections.Generic;
using SimpleTodoApp.Data;

using SimpleTodoApp.Models;

public class TaskService : ITaskService
{
    private readonly ApplicationDbContext _context;
    public TaskService(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<TaskEntity> GetAllTasks()
    {
        return _context.Tasks.OrderByDescending(t => t.CreatedDate).ToList();
    }   
    public TaskEntity GetTaskById(int id)
    {
        return _context.Tasks.Find(id);
    }
    public void CreateTask(CreateTaskViewModel taskViewModel)
    {
        var taskEntity = new TaskEntity
        {
            Title = taskViewModel.Title,
            IsCompleted = false,
            CreatedDate = DateTime.UtcNow
        };
        _context.Tasks.Add(taskEntity);
        _context.SaveChanges();
    }
    public void UpdateTask(TaskEntity task)
    {
        var existingTask = _context.Tasks.Find(task.Id);
        if (existingTask != null)
        {
            existingTask.Title = task.Title;
            existingTask.IsCompleted = task.IsCompleted;
            _context.Tasks.Update(existingTask);
            _context.SaveChanges();
        }
    }
    public void DeleteTask(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
    }

}
