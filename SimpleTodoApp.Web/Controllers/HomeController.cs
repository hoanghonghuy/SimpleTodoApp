using Microsoft.AspNetCore.Mvc;
using SimpleTodoApp.Models;

namespace SimpleTodoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl = "https://localhost:7042/api/task";
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<TaskEntity> tasks = new List<TaskEntity>();

            // Tạo một HttpClient 
            var client = _httpClientFactory.CreateClient();
            try
            {
                tasks = await client.GetFromJsonAsync<List<TaskEntity>>(_apiBaseUrl) ?? new List<TaskEntity>();
            }
            catch (HttpRequestException)
            {
                // Xử lý lỗi khi không thể kết nối đến API
                tasks = new List<TaskEntity>();
                ViewBag.ErrorMessage = "Không thể kết nối đến máy chủ API.";
            }
            return View(tasks);
        }
        // GET - Hiển thị form tạo mới task
        public IActionResult Create()
        {
            return View();
        }
        // POST - Nhận dữ liệu từ form và gửi đến API để tạo mới task
        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskViewModel viewModel)
        {
            // Nếu dữ liệu không hợp lệ, trả về view 
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var client = _httpClientFactory.CreateClient();
            // Gửi yêu cầu POST với dữ liệu viewModel dưới dạng JSON
            var response = await client.PostAsJsonAsync(_apiBaseUrl, viewModel);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            // Nếu không thành công, hiển thị lỗi
            ViewBag.ErrorMessage = "Không thể tạo task mới. Vui lòng thử lại.";
            return View(viewModel);
        }
        // GET - Hiển thị form chỉnh sửa task
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            try
            {
                var task = await client.GetFromJsonAsync<TaskEntity>(_apiBaseUrl + "/" + id);
                if (task == null)
                {
                    return NotFound();
                }
                return View(task);
            }
            catch (HttpRequestException)
            {

                return NotFound("Không thể kết nối đến máy chủ API.");
            }
        }
        // POST - Nhận dữ liệu từ form và gửi đến API để cập nhật task
        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskEntity task)
        {
            if (id != task.Id)
            {
                return BadRequest("ID trong URL không khớp với ID trong dữ liệu.");
            }
            // Nếu dữ liệu không hợp lệ, trả về view
            if (!ModelState.IsValid)
            {
                return View(task);
            }
            var client = _httpClientFactory.CreateClient();
            // Gửi yêu cầu PUT với dữ liệu task dưới dạng JSON
            var response = await client.PutAsJsonAsync(_apiBaseUrl + "/" + id, task);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ViewBag.ErrorMessage = "Không thể cập nhật task. Vui lòng thử lại.";
            return View(task);
        }
        // GET - Hiển thị xác nhận xóa task
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            try
            {
                var task = await client.GetFromJsonAsync<TaskEntity>($"{_apiBaseUrl}/{id}");
                if (task == null)
                {
                    return NotFound();
                }
                return View(task);
            }
            catch (HttpRequestException)
            {

                return NotFound("Không thể kết nối đến máy chủ API.");
            }

        }
        // POST - Xác nhận xóa task
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient();
            // Gửi yêu cầu DELETE đến API
            var response = await client.DeleteAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Delete", new {id = id, error = true});
        }
    }
}
