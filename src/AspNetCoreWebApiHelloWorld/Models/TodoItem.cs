using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApiHelloWorld.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsComplete { get; set; }
    }
}