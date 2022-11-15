using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace TodoApi
{
    [Route("/stats")]
    public class StatisticsController : Controller
    {
        private TodoDbContext dbContext;

        public StatisticsController(TodoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("todo")]
        [OutputCache(PolicyName = "Expire30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetTodoStats()
        {
            var allTodos = dbContext.Todos;
            var allCompletedTodos = dbContext.Todos.Where(t => t.IsComplete);
            return Json(new { all = allTodos.Count(), completed = allCompletedTodos.Count() });
        }

        [HttpGet]
        [Route("owners")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetOwnerStats()
        {
            var allTodos = dbContext.Todos.GroupBy(t => t.OwnerId).Select(g => new { owner = g.Key, todos = g.Count() });
            return Json(allTodos);
        }

        [HttpGet]
        [Route("stream")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IResult GetStream()
        {
            return Results.Stream(async s => await s.WriteAsync("streamed"u8.ToArray()));
        }
    }
}
