using Microsoft.AspNetCore.Mvc;
using SimpleTodoApp.Models;
using SimpleTodoApp.Services;

namespace SimpleTodoApp.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<TaskEntity>> GetAllTasks()
        {
            var tasks = _taskService.GetAllTasks();
            return Ok(tasks);
        }
        [HttpGet("{id}")]
        public ActionResult<TaskEntity> GetTaskById(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        [HttpPost]
        public ActionResult<TaskEntity> CreateTask(CreateTaskViewModel taskViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }

            // gọi service để nhận về task vừa được tạo
            var createdTask = _taskService.CreatxeTask(taskViewModel);

            // Trả về mã 201 Created và thông tin của task mới
            // và một hearer Location trỏ đến API để lấy task này
            return CreatedAtAction("GetTaskById", new { id = createdTask.Id }, createdTask);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TaskEntity task)
        {
            if (id != task.Id)      
            {
                return BadRequest("ID in URL does not match ID in body");
            }

            // Kiểm tra xem task có tồn tại không
            var existingTask = _taskService.GetTaskById(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            _taskService.UpdateTask(task);
            return NoContent(); // Trả về 204 No Content nếu cập nhật thành công - báo hiệu thành công mà không cần gửi lại nội dung
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id) {
            
            var existingTask = _taskService.GetTaskById(id);
            if (existingTask == null)
            {
                return NotFound();
            }
            _taskService.DeleteTask(id);
            return NoContent(); // Trả về 204 No Content nếu xóa thành công - báo hiệu thành công mà không cần gửi lại nội dung
        }
    }
}