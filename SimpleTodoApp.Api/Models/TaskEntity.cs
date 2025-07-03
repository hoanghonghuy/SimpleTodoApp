namespace SimpleTodoApp.Models
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        // public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        // Constructor mặc định
        //public TaskEntity()
        //{
        //    IsCompleted = false;
        //}
        // Constructor với tham số
        //public TaskEntity(int id, string title, string description)
        //{
        //    Id = id;
        //    Title = title;
        //    Description = description;
        //    IsCompleted = false;
        //}
    }
}
