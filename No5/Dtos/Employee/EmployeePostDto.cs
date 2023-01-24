using No5.Models;

namespace No5.Dtos.Employee
{
    public class EmployeePostDto
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string About { get; set; }
        public IFormFile FormFile { get; set; }
        public List<Icon> Icons { get; set; }
    }
}
