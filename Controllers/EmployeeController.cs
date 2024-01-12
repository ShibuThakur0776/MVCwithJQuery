using Microsoft.AspNetCore.Mvc;
using MVCwithJQuery.Models;
using MVCwithJQuery.Models.Data;

namespace MVCwithJQuery.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
           var list =  _context.Employees.ToList();
            return View(list);
        }
        public IActionResult Add()
        {
            Employee employee = new Employee();
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Employee employee)
        {
            if(employee == null) return View();
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        [HttpPut]
        public IActionResult Edit(Employee employee)
        {
            if (employee == null) return View();
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if(id == 0) return View();
            var employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
