using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.Entities;
using Todo.Domain.Queries;

namespace Todo.Test
{
    [TestClass]
    public class QueryTeste
    {
        private List<TodoItem> items;
        [TestMethod]
        public void RetonarTodasTarefasDeUmUser()
        {
            items = new List<TodoItem>();
            items.Add(new TodoItem("Tarefa 1", DateTime.Now, "raulSilva"));
            items.Add(new TodoItem("Tarefa 2", DateTime.Now, "raulSilva"));
            items.Add(new TodoItem("Tarefa 3", DateTime.Now, "MarcosBenjamim"));
            items.Add(new TodoItem("Tarefa 4", DateTime.Now, "DowtonMelos"));
            items.Add(new TodoItem("Tarefa 5", DateTime.Now, "raulSilva"));
            items.Add(new TodoItem("Tarefa 6", DateTime.Now, "DowtonMelos"));
            items.Add(new TodoItem("Tarefa 7", DateTime.Now, "MarcosBenjamim"));
            items.Add(new TodoItem("Tarefa 8", DateTime.Now, "MarcosBenjamim"));

            var result = items.AsQueryable().Where(TodoQuery.GetAll("raulSilva"));
            Assert.AreEqual(3, result.Count());
        }

    }
}