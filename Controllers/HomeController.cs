using Microsoft.AspNetCore.Mvc;
using SimpleTodoApp.Models;
using System.Diagnostics;
using SimpleTodoApp.Data;

namespace SimpleTodoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Tạo 1 danh sách công việc đơn giản
            //var tasks = new List<string>
            //{
            //    "Học về Controller và View",
            //    "Tìm hiểu cách truyền dữ liệu",
            //    "Làm bài tập thực hành"
            //};
            // ViewData["TodoList"] = tasks;

            var tasks = _context.Tasks.OrderByDescending(t => t.CreatedDate).ToList();
            return View(tasks);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // [GET] /Home/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateTaskViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Chuyển đổi từ ViewModel sang Entity
                var taskEntity = new TaskEntity
                {
                    Title = viewModel.Title,
                    IsCompleted = false,
                    CreatedDate = DateTime.Now
                };

                // Dùng DbContext để thêm và lưu vào CSDL
                _context.Tasks.Add(taskEntity);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Tìm công việc theo ID
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            // Chuyển đổi sang ViewModel
            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(int id, TaskEntity task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                // Cập nhật công việc
                _context.Tasks.Update(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            // Tìm công việc theo ID
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Tìm công việc theo ID
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            // Xóa công việc
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
