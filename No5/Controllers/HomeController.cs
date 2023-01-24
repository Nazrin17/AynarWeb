using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using No5.Context;
using No5.Dtos.Employee;
using No5.Models;
using System.Diagnostics;

namespace No5.Controllers
{
    public class HomeController : Controller
    {
        private readonly AnyarDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(AnyarDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<Employee> employees = _context.Employees.Include(e => e.Icons).ToList();
            List<EmployeeGetDto> emps = _mapper.Map<List<EmployeeGetDto>>(employees);
            return View(emps);
        }

    }
}