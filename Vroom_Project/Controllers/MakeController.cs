using Microsoft.AspNetCore.Mvc;
using Vroom_Project.AppDbContext;
using Vroom_Project.Models;
using Vroom_Project.ViewModels;

namespace Vroom_Project.Controllers
{
    public class MakeController : Controller
    {
        private readonly VroomDbContext _db;
        public MakeController(VroomDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var repoData = _db.Makes.ToList().Select(x=>new CreateModel
            {
                 Name = x.Name,
                 Id = x.Id,
            });
            return View(repoData);
        }
        //HTTP GET method
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddMake([FromBody] CreateModel make)
        {
            if(ModelState.IsValid)
            {
                var newMake = new Make
                {
                    Name = make.Name,
                };
                _db.Makes.Add(newMake);
                _db.SaveChanges();
               return RedirectToAction(nameof(Index));
            }
            return View(make);
        }
       
    }
}
