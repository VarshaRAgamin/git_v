using Microsoft.AspNetCore.Mvc;
using Vroom_Project.AppDbContext;
using Vroom_Project.Models;
using Vroom_Project.ViewModels;

namespace Vroom_Project.Controllers
{
    public class TestController : Controller
    {
        private readonly VroomDbContext _dbContext;
        public TestController(VroomDbContext dbContext)
        {
            _dbContext = dbContext;     
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(MakeModel model)
        {
            var newMake = new Make
            {
                Name = model.Name,
            };
            _dbContext.Makes.Add(newMake);
            await _dbContext.SaveChangesAsync();
            TempData["Message"] = $"{model.Name} has been added successfully!";
            return View("Index");    
        }
    }
}
