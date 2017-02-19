using System;
using AspNetCoreWebApiHelloWorld.Models;
using AspNetCoreWebApiHelloWorld.Repositiories;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApiHelloWorld.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITodoRepository _todoRepository;

        public TodoController(IServiceProvider serviceProvider, ITodoRepository todoRepository)
        {
            _serviceProvider = serviceProvider;
            _todoRepository = todoRepository;
        }
        /// <summary>
        /// A landing page for browsers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return "Wellcome!";
        }

        /// <summary>
        /// Gets an item by id.
        /// </summary>
        /// <remarks>It is integer and must be greater than zero!</remarks>
        /// <param name="id">Id of the item</param>
        /// <returns>TodoItem</returns>
        [Produces(typeof(TodoItem))]  //adds model and schema to swagger very helpful for passing around objects. 
        [Consumes("application/json")]   //Tell this action to only accecpt json objects. Without it, it will accept anything<img width="16" height="16" class="wp-smiley emoji" draggable="false" alt=":)" src="https://s1.wp.com/wp-content/mu-plugins/wpcom-smileys/simple-smile.svg" style="height: 1em; max-height: 1em;">
        [HttpGet("{id}")] // test - deneme 123
        public TodoItem Get(int id)
        {
            return _todoRepository.Find(id);
        }

        [HttpPost]
        [Consumes("application/json")]
        public void Post([FromBody]TodoItem newTodoItem)
        {
            // resolve dependency  manually
            var repository = (ITodoRepository)_serviceProvider.GetService(typeof(ITodoRepository));
            repository.Add(newTodoItem);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TodoItem item)
        {
            //validation-1
            if (item == null || item.Id <= 0)
            {
                return BadRequest();
            }

            //validation-2
            var todo = _todoRepository.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _todoRepository.Update(todo);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _todoRepository.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _todoRepository.Remove(id);
            return new NoContentResult();
        }

        //[HttpPut("{id}", Name = "SetAsCompleted")]
        //public IActionResult CompleteTodo(int id)
        //{
        //    var item = _todoRepository.Find(id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }

        //    return new NoContentResult();
        //}

    }
}