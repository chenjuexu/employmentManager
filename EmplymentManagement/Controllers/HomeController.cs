using EmplymentManagement.Models;
using EmplymentManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmplymentManagement.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        
        public ViewResult Index()
        {
            // retrieve all the employees
            var model = _employeeRepository.GetAllEmployees();
            // Pass the list of employees to the view
            return View(model);
        }
         
    // ? makes id method parameter nullable
    public ViewResult Details(int? id)
    {
        HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
        {
            // If "id" is null use 1, else use the value passed from the route
            Employee = _employeeRepository.GetEmployee(id ?? 1),
            PageTitle = "Employee Details"
        };

        return View(homeDetailsViewModel);
    }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee newEmployee = _employeeRepository.Add(employee);
               // return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();
        }
    }
}
