using Microsoft.AspNetCore.Mvc;
using SimpleTodoApp.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SimpleTodoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Tạo 1 danh sách công việc đơn giản
            var tasks = new List<string>
            {
                "Học về Controller và View",
                "Tìm hiểu cách truyền dữ liệu",
                "Làm bài tập thực hành"
            };
            // ViewData["TodoList"] = tasks;
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
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
    }
}
