using System.Collections.Generic;
using AspNetCoreWebApiHelloWorld.Models;

namespace AspNetCoreWebApiHelloWorld.Repositiories
{
    public interface ITodoRepository
    {
        void Add(TodoItem item);
        TodoItem Find(int id);
        IEnumerable<TodoItem> GetAll();
        void Remove(int id);
        void Update(TodoItem item);
    }
}