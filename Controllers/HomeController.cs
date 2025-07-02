using Microsoft.AspNetCore.Mvc;
using SimpleTodoApp.Models;
using System.Diagnostics;
using SimpleTodoApp.Data;
using SimpleTodoApp.Services;

namespace SimpleTodoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskService _taskService;
        public HomeController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        public IActionResult Index()
        {
            var tasks = _taskService.GetAllTasks();
            return View(tasks);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateTaskViewModel taskViewModel)
        {
            if (ModelState.IsValid)
            {
                _taskService.CreateTask(taskViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(taskViewModel);
        }
        public IActionResult Edit(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }
        [HttpPost]
        public IActionResult Edit(int id, TaskEntity task)
        {
            var existingTask = _taskService.GetTaskById(id);
            if (existingTask == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _taskService.UpdateTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }
        public IActionResult Delete(int id)
        {
            var exitingTask = _taskService.GetTaskById(id);
            if (exitingTask == null)
            {
                return NotFound();
            }
            return View(exitingTask);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _taskService.DeleteTask(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
