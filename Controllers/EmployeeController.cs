using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCwithJQuery.Models;
using MVCwithJQuery.Models.Data;
using System.Linq;

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
			return View();
        }
        
        public IActionResult GetEmployees(int page = 0)
        {
			const int pageSize = 10;
			var employeeList = _context.Employees
				.Include(e => e.Department);
			//Pagination
			var count = employeeList.Count();
			//0*10 =0 skip from 0 rec,take 10
			var data = employeeList.Skip(page * pageSize).Take(pageSize).ToList();

			ViewBag.MaxPage = (count / pageSize) - (count % pageSize == 0 ? 1 : 0);
			ViewBag.Page = page;

			return Json(data);
        }
        public IActionResult Add()
        {
            ViewBag.depList = _context.Departments.ToList();
            Employee employee = new Employee();

			return View(employee);
        }
        [HttpPost]
        public IActionResult Save(Employee employee)
        {
            if(employee == null) return View();
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        { 
            var employee = _context.Employees
                .Include(e=>e.Department)
                .FirstOrDefault(e=>e.Id ==id);
            ViewBag.depList = _context.Departments.ToList();
            if(employee == null) return View(employee);
            return View(employee); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Employee employee)
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
