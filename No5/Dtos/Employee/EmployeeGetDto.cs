using No5.Models;

namespace No5.Dtos.Employee
{
    public class EmployeeGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string About { get; set; }
        public string Image { get; set; }
        public List<Icon> Icons { get; set; }
    }
}
