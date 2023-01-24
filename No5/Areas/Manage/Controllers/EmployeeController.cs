using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using No5.Context;
using No5.Dtos.Employee;
using No5.Models;

namespace No5.Areas.Area.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly AnyarDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(AnyarDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Employee> emps = _context.Employees.Include(e => e.Icons).ToList();
            if (emps == null)
            {
                return View();
            }
            List<EmployeeGetDto> getDtos = _mapper.Map<List<EmployeeGetDto>>(emps);
            return View(getDtos);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeePostDto postDto)
        {


            if (!ModelState.IsValid)
            {
                foreach (var item in postDto.Icons)
                {
                    if (item.Name == null || item.Url == null)
                    {
                        ModelState.AddModelError("Icons", "The Icon field is required.");
                    }
                }
                return View();
            }
            Employee employee = _mapper.Map<Employee>(postDto);
            string imagename = Guid.NewGuid() + postDto.FormFile.FileName;
            string path = Path.Combine(_env.WebRootPath, "assets/img", imagename);
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                postDto.FormFile.CopyTo(fileStream);
            }
            employee.Image = imagename;
            foreach (var item in postDto.Icons)
            {
                employee.Icons.Add(item);
            }
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            Employee employee = _context.Employees.Include(e => e.Icons).Where(e => e.Id == id).FirstOrDefault();
            if (employee == null)
            {
                return NotFound();
            }
            EmployeeGetDto getDto = _mapper.Map<EmployeeGetDto>(employee);
            EmployeeUpdateDto updateDto = new EmployeeUpdateDto { getDto = getDto };
            return View(updateDto);
        }
        [HttpPost]
        public IActionResult Update(EmployeeUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid format");
            }
            Employee employee = _context.Employees.Include(e => e.Icons).Where(e => e.Id == updateDto.getDto.Id).FirstOrDefault();
            employee.Position = updateDto.postDto.Position;
            employee.Name = updateDto.postDto.Name;
            employee.About = updateDto.postDto.About;
            if (updateDto.postDto.FormFile != null)
            {
                string imagename = Guid.NewGuid() + updateDto.postDto.FormFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "assets/img", imagename);
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    updateDto.postDto.FormFile.CopyTo(fileStream);
                }
                employee.Image = imagename;
            }
            for (int i = 0; i < updateDto.postDto.Icons.Count; i++)
            {
                employee.Icons[i] = updateDto.postDto.Icons[i];
            }
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Employee employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

    }
}
