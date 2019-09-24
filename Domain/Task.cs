using System;

namespace TODO.Domain
{
    class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? Completed { get; set; }
        
        //public bool IsCompleted
        //{
        //    get
        //    {
        //        return Completed != null;
        //    }
        //}

        public Task(int id, string title, DateTime dueDate)
        {
            Id = id;
            Title = title;
            DueDate = dueDate;
        }
    }
}
