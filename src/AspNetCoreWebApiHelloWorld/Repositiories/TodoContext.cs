using System.Collections.Generic;
using AspNetCoreWebApiHelloWorld.Models;

namespace AspNetCoreWebApiHelloWorld.Repositiories
{
    public class TodoContext
    {
        public Dictionary<int, TodoItem> TodoItems { get; set; }
        public TodoContext()
        {
            TodoItems = new Dictionary<int, TodoItem>
            {
                {1, new TodoItem {Id = 1, Name = "Buy some eggs and fruits."}},
                {2, new TodoItem {Id = 2, Name = "Pay bills."}},
                {3, new TodoItem {Id = 3, Name = "Catch up meeting with the client on Monday!"}}
            };
        }
    }
}