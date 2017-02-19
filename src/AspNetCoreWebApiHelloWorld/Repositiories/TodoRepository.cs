using System.Collections.Generic;
using System.Linq;
using AspNetCoreWebApiHelloWorld.Models;

namespace AspNetCoreWebApiHelloWorld.Repositiories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
            Add(new TodoItem { Name = "Item1" });
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems.Values.ToList();
        }

        public void Add(TodoItem item)
        {
            _context.TodoItems.Add(item.Id, item);
        }

        public TodoItem Find(int id)
        {
            return _context.TodoItems.Values.FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id)
        {
            _context.TodoItems.Remove(id);
        }

        public void Update(TodoItem item)
        {
            _context.TodoItems[item.Id] = item;
        }
    }
}